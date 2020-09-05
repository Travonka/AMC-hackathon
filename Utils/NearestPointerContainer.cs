using System;
using System.Collections.Generic;
using System.Linq;
using GetData;

namespace Utils
{
    public sealed class TooFarException : ArgumentOutOfRangeException
    {
        public TooFarException(float longitude, float latitude) 
            : base($"Invalid coordinates of parking: {longitude} {latitude}")
        {
        }
    }

    public sealed class NearestPointerContainer<TLocatable> where TLocatable : IParking
    {
        private sealed class Cell
        {
            internal readonly List<TLocatable> Parkings = new List<TLocatable>();
            public void Add(TLocatable parking)
                => Parkings.Add(parking);
        }

        public readonly float MaxDistanceLatitude;
        public readonly float MaxDistanceLongtitude;
        private readonly Cell[,] container;
        private readonly int xCount;
        private readonly int yCount;
        private readonly float offsetLatitude;
        private readonly float offsetLongitude;

        private bool InCellBounds(int x, int y)
        {
            return !(x < 0 || x >= xCount || y < 0 || y >= yCount);
        }

        private bool InCellBoundsOrNeighbour(int x, int y)
        {
            return !(x < -1 || x > xCount || y < -1 || y > yCount);
        }

        private (int x, int y) GetXY(TLocatable parking)
        {
            var xDegree = parking.Longitude - offsetLongitude;
            var yDegree = parking.Latitude - offsetLatitude;

            var x = (int)(xDegree / MaxDistanceLatitude);
            var y = (int)(yDegree / MaxDistanceLongtitude);
            return (x, y);
        }

        private bool TryGetCoord(TLocatable parking, out (int x, int y) res)
        {
            var (x, y) = GetXY(parking);

            if (!InCellBounds(x, y))
            {
                res = (-1, -1);
                return false;
            }

            res = (x, y);
            return true;
        }

        private bool TryGetCoordNeighbour(TLocatable parking, out (int x, int y) res)
        {
            var (x, y) = GetXY(parking);

            if (!InCellBoundsOrNeighbour(x, y))
            {
                res = (-1, -1);
                return false;
            }

            res = (x, y);
            return true;
        }

        private (int x, int y) GetCoordWithCheck(TLocatable parking)
        {
            if (!TryGetCoord(parking, out var res))
                throw new TooFarException(parking.Longitude, parking.Latitude);

            return res;
        }

        private (int x, int y) GetCoordWithCheckNeighbour(TLocatable parking)
        {
            if (!TryGetCoordNeighbour(parking, out var res))
                throw new TooFarException(parking.Longitude, parking.Latitude);

            return res;
        }

        private void AddParking(TLocatable parking)
        {
            var (x, y) = GetCoordWithCheck(parking);
            container[x, y].Add(parking);
        }

        public TLocatable GetNearest(TLocatable parking, Func<TLocatable, float> distanceTo)
        {
            var (x, y) = GetCoordWithCheckNeighbour(parking);
            var candidates = new List<TLocatable>();
            for (int xOff = -1; xOff <= 1; xOff++)
                for (int yOff = -1; yOff <= 1; yOff++)
                    if (InCellBounds(x + xOff, y + yOff))
                        candidates.AddRange(container[x + xOff, y + yOff].Parkings);

            var bestDistance = 1e+40;
            var bestCandidate = default(TLocatable);

            foreach (var c in candidates)
            {

                    var dist = distanceTo(c);
                    if (dist < bestDistance && dist!=0)
                    {
                        bestDistance = dist;
                        bestCandidate = c;
                    }
                
            }

            return bestCandidate;
        }

        public NearestPointerContainer(
            IEnumerable<TLocatable> parkings, float maxDistanceLongtitude , float maxDistanceLatitude)
        {
            MaxDistanceLongtitude = maxDistanceLongtitude;
            MaxDistanceLatitude = maxDistanceLatitude;

            var leftPoint = parkings.Select(c => c.Longitude).Min();
            var rightPoint = parkings.Select(c => c.Longitude).Max();
            var bottomPoint = parkings.Select(c => c.Latitude).Min();
            var topPoint = parkings.Select(c => c.Latitude).Max();

            offsetLongitude = leftPoint;
            offsetLatitude = bottomPoint;

            var width = rightPoint - leftPoint;
            var height = topPoint - bottomPoint;

            xCount = (int)(width / (maxDistanceLatitude) + 1);
            yCount = (int)(height / (maxDistanceLongtitude) + 1);

            container = new Cell[xCount, yCount];

            for (int x = 0; x < xCount; x++)
                for (int y = 0; y < yCount; y++)
                    container[x, y] = new Cell();

            foreach (var parking in parkings)
                AddParking(parking);
        }
    }
}
