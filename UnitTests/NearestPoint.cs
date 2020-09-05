using GetData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtoBuf;
using System;
using Utils;

namespace UnitTests
{
    [TestClass]
    public class NearestPoint
    {
        public class MyLocation : ITransport
        {
            public float Longitude { get; set; }
            public float Latitude { get; set; }

            public MyLocation(float lg, float lt)
            {
                Longitude = lg;
                Latitude = lt;
            }

            public static implicit operator MyLocation((float x, float y) d)
                => new MyLocation(d.x, d.y);

            public static bool operator ==(MyLocation a, MyLocation b)
                => a.Latitude == b.Latitude && a.Longitude == b.Longitude;
            public static bool operator !=(MyLocation a, MyLocation b)
                => !(a == b);

            public override bool Equals(object c)
                => c is MyLocation ml ? ml == this : false;
        }

        NearestPointerContainer<MyLocation> ct;

        public NearestPoint()
        {
            var locs = new MyLocation[] { 
                (1, 1),
                (2, 2),
                (-1, -1),
                (30, 50),
                (25, 25)
                };
            ct = new NearestPointerContainer<MyLocation>(locs, 1, 1);
        }

        private static Func<MyLocation, MyLocation, float> measure = (ml, loc) => (float)Math.Sqrt(
                    Math.Pow(ml.Longitude - loc.Longitude, 2) + Math.Pow(ml.Latitude - ml.Latitude, 2));
        public void Test(MyLocation loc, MyLocation expected)
        {
            Assert.AreEqual(expected,
                ct.GetNearest(loc, ml => measure(ml, loc))
                );
        }

        [TestMethod]
        public void Test1() => Test((2, 2), (2, 2));
        [TestMethod]
        public void Test2() => Test((2.001f, 1.999f), (2, 2));
        [TestMethod]
        public void Test3() => Test((25, 26), (25, 25));
        [TestMethod]
        public void Test4() => Test((-1.5f, -1.5f), (-1, -1));
        [TestMethod]
        public void Test5() => Assert.ThrowsException<TooFarException>(() => ct.GetNearest((-4.5f, -4.5f), ml => measure(ml, (-4.5f, -4.5f))));
    }
}
