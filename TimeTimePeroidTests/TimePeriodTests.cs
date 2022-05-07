using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTimePeroid;

namespace TimeTimePeroidTests
{
    [TestClass]
    public class TimePeriodTests
    {
        [TestMethod]
        public void TimePeriodCreate()
        {
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod(-1, 0, 0));
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod(0, -1, 0));
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod(0, 0, -1));

            Assert.IsInstanceOfType(new TimePeriod(25, 0, 0), typeof(TimePeriod));
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod(0, 61, 0));
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod(0, 0, 61));

            Assert.ThrowsException<ArgumentException>(() => new TimePeriod("-1:00:00"));
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod("00:-1:00"));
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod("00:00:-1"));

            Assert.IsInstanceOfType(new TimePeriod("25:00:00"), typeof(TimePeriod));
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod("00:61:00"));
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod("00:00:61"));

            Assert.ThrowsException<ArgumentException>(() => new TimePeriod("a"));
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod("1:a"));
            Assert.ThrowsException<ArgumentException>(() => new TimePeriod("1:1:a"));

            Assert.IsInstanceOfType(new TimePeriod(0, 0, 0), typeof(TimePeriod));
            Assert.IsInstanceOfType(new TimePeriod(0, 0), typeof(TimePeriod));
            Assert.IsInstanceOfType(new TimePeriod(0), typeof(TimePeriod));
            Assert.IsInstanceOfType(new TimePeriod("00:00:00"), typeof(TimePeriod));

            Assert.IsInstanceOfType(new TimePeriod(90, 0), typeof(TimePeriod));
            Assert.IsInstanceOfType(new TimePeriod(280), typeof(TimePeriod));
        }

        [TestMethod]
        public void TimePeriodToString()
        {
            Assert.AreEqual(new TimePeriod(0, 0, 0).ToString(), "00:00:00");
            Assert.AreEqual(new TimePeriod(5, 12, 24).ToString(), "05:12:24");
            Assert.AreEqual(new TimePeriod(12, 6, 3).ToString(), "12:06:03");

            Assert.AreEqual("120:30:15", new TimePeriod(120, 30, 15).ToString());
            Assert.AreEqual("01:30:15", new TimePeriod(90, 15).ToString());
            Assert.AreEqual("00:04:40", new TimePeriod(280).ToString());
        }

        [TestMethod]
        public void TimePeriodCompare()
        {
            Assert.IsTrue(new TimePeriod(0, 0, 0) < new TimePeriod(0, 0, 1));
            Assert.IsTrue(new TimePeriod(0, 0, 1) > new TimePeriod(0, 0, 0));
            Assert.IsTrue(new TimePeriod(0, 0, 0) <= new TimePeriod(0, 0, 1));
            Assert.IsTrue(new TimePeriod(0, 0, 1) >= new TimePeriod(0, 0, 0));
            Assert.IsTrue(new TimePeriod(0, 0, 0) >= new TimePeriod(0, 0, 0));
            Assert.IsTrue(new TimePeriod(0, 0, 0) <= new TimePeriod(0, 0, 0));
            Assert.IsTrue(new TimePeriod(0, 0, 0) == new TimePeriod(0, 0, 0));
            Assert.IsTrue(new TimePeriod(0, 1, 0) != new TimePeriod(0, 0, 0));
        }

        [TestMethod]
        public void TimePeriodCalculations()
        {
            Assert.AreEqual(new TimePeriod(12,0,0), new TimePeriod(10, 25, 30) + new TimePeriod(1, 34, 30));
            Assert.AreEqual(new TimePeriod(12,0,0), new TimePeriod(13, 25, 30) - new TimePeriod(1, 25, 30));
            Assert.AreEqual(new TimePeriod(12,0,0), new TimePeriod(1, 25, 30) - new TimePeriod(13, 25, 30));
        }
    }
}
