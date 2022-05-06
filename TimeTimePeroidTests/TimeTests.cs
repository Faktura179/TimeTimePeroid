using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TimeTimePeroid;

namespace TimeTimePeroidTests
{
    [TestClass]
    public class TimeTests
    {
        [TestMethod]
        public void TimeCreate()
        {
            Assert.ThrowsException<ArgumentException>(() => new Time(-1, 0, 0));
            Assert.ThrowsException<ArgumentException>(() => new Time(0, -1, 0));
            Assert.ThrowsException<ArgumentException>(() => new Time(0, 0, -1));

            Assert.ThrowsException<ArgumentException>(() => new Time(25, 0, 0));
            Assert.ThrowsException<ArgumentException>(() => new Time(0, 61, 0));
            Assert.ThrowsException<ArgumentException>(() => new Time(0, 0, 61));

            Assert.ThrowsException<ArgumentException>(() => new Time("-1:00:00"));
            Assert.ThrowsException<ArgumentException>(() => new Time("00:-1:00"));
            Assert.ThrowsException<ArgumentException>(() => new Time("00:00:-1"));

            Assert.ThrowsException<ArgumentException>(() => new Time("25:00:00"));
            Assert.ThrowsException<ArgumentException>(() => new Time("00:61:00"));
            Assert.ThrowsException<ArgumentException>(() => new Time("00:00:61"));

            Assert.ThrowsException<ArgumentException>(() => new Time("a"));
            Assert.ThrowsException<ArgumentException>(() => new Time("1:a"));
            Assert.ThrowsException<ArgumentException>(() => new Time("1:1:a"));

            Assert.IsInstanceOfType(new Time(0, 0, 0), typeof(Time));
            Assert.IsInstanceOfType(new Time(0, 0), typeof(Time));
            Assert.IsInstanceOfType(new Time(0), typeof(Time));
            Assert.IsInstanceOfType(new Time("00:00:00"), typeof(Time));
        }

        [TestMethod]
        public void TimeToString()
        {
            Assert.AreEqual(new Time(0, 0, 0).ToString(), "00:00:00");
            Assert.AreEqual(new Time(5, 12, 24).ToString(), "05:12:24");
            Assert.AreEqual(new Time(12, 6, 3).ToString(), "12:06:03");
        }

        [TestMethod]
        public void TimeCompare()
        {
            Assert.IsTrue(new Time(0, 0, 0) < new Time(0, 0, 1));
            Assert.IsTrue(new Time(0, 0, 1) > new Time(0, 0, 0));
            Assert.IsTrue(new Time(0, 0, 0) <= new Time(0, 0, 1));
            Assert.IsTrue(new Time(0, 0, 1) >= new Time(0, 0, 0));
            Assert.IsTrue(new Time(0, 0, 0) >= new Time(0, 0, 0));
            Assert.IsTrue(new Time(0, 0, 0) <= new Time(0, 0, 0));
            Assert.IsTrue(new Time(0, 0, 0) == new Time(0, 0, 0));
            Assert.IsTrue(new Time(0, 1, 0) != new Time(0, 0, 0));
        }

        [TestMethod]
        public void TimeCalculations()
        {
            Assert.AreEqual(new Time(0, 0, 0) + new Time(0,0,1), new Time(0, 0, 1));
            Assert.AreEqual(new Time(0, 0, 59) + new Time(0,0,2), new Time(0, 1, 1));
            Assert.AreEqual(new Time(0, 59, 59) + new Time(0,0,2), new Time(1, 0, 1));
        }
    }
}