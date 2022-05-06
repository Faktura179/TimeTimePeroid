using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTimePeroid
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        public byte Hours { get; init; }
        public byte Minutes { get; init; }
        public byte Seconds { get; init; }

        public Time(int hours, int minutes, int seconds)
        {
            ValidateTime(hours, minutes, seconds);
            Hours = (byte)hours;
            Minutes = (byte)minutes;
            Seconds = (byte)seconds;
        }

        public Time(int hours, int minutes)
        {
            Seconds = 0;
            ValidateTime(hours, minutes, Seconds);
            Hours = (byte)hours;
            Minutes = (byte)minutes;
        }

        public Time(int hours)
        {
            Minutes = 0;
            Seconds = 0;
            ValidateTime(hours, Minutes, Seconds);
            Hours = (byte)hours;
        }

        public Time(string time)
        {
            if(time is null || string.IsNullOrEmpty(time))
                throw new ArgumentNullException("Invalid time string");
            
            string[] timeParts = time.Split(':');

            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            try
            {
                hours = Convert.ToInt32(timeParts[0]);
                if (timeParts.Length > 1)
                    minutes = Convert.ToInt32(timeParts[1]);
                if (timeParts.Length > 2)
                    seconds = Convert.ToInt32(timeParts[2]);
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid time string");
            }

            ValidateTime(hours, minutes, seconds);
            Hours = (byte)hours;
            Minutes = (byte)minutes;
            Seconds = (byte)seconds;
        }


        private static void ValidateTime(int hours, int minutes, int seconds)
        {
            if (hours >= 24 || minutes >= 60 || seconds >= 60)
                throw new ArgumentException("Invalid value");
            if(hours < 0 || minutes < 0 || seconds < 0)
                throw new ArgumentException("Invalid value");
        }

        public override string ToString()
        {
            return $"{Hours.ToString().PadLeft(2, '0')}:{Minutes.ToString().PadLeft(2, '0')}:{Seconds.ToString().PadLeft(2, '0')}";
        }

        public bool Equals(Time other)
        {
            if(Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds)
                return true;
            return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Time time = (Time)obj;
            return Equals(time);
        }

        public override int GetHashCode()
        {
            return (Seconds + (byte.MaxValue*(Minutes+1)+1) + (byte.MaxValue*byte.MaxValue* (Hours+1)+1));
        }

        public static bool operator ==(Time time1, Time time2)
        {
            return time1.Equals(time2);
        }

        public static bool operator !=(Time time1, Time time2)
        {
            return !(time1.Equals(time2));
        }

        public int CompareTo(Time other)
        {
            return GetHashCode().CompareTo(other.GetHashCode());
        }

        public static bool operator <(Time time1, Time time2)
        {
            return time1.CompareTo(time2) < 0;
        }
        public static bool operator >(Time time1, Time time2)
        {
            return time1.CompareTo(time2) > 0;
        }

        public static bool operator <=(Time time1, Time time2)
        {
            return time1.CompareTo(time2) <= 0;
        }

        public static bool operator >=(Time time1, Time time2)
        {
            return time1.CompareTo(time2) >= 0;
        }

        public static Time operator +(Time time1, Time time2)
        {
            int newSeconds = (time1.Seconds + time2.Seconds) % 60;
            int carryMinutes = (time1.Seconds + time2.Seconds) / 60;
            int newMinutes = (time1.Minutes + time2.Minutes + carryMinutes) % 60;
            int carryHours = (time1.Minutes + time2.Minutes + carryMinutes) / 60;
            int newHours = (time1.Hours + time2.Hours + carryHours) % 24;

            return new Time(newHours, newMinutes, newSeconds);
        }

        public static Time operator -(Time time1, Time time2)
        {
            int newSeconds = MathHelper.Mod((time1.Seconds - time2.Seconds), 60);
            int carryMinutes = (time1.Seconds - time2.Seconds - 59) / 60;
            int newMinutes = MathHelper.Mod((time1.Minutes - time2.Minutes + carryMinutes), 60);
            int carryHours = (time1.Minutes - time2.Minutes + carryMinutes - 59) / 60;
            int newHours = MathHelper.Mod((time1.Hours - time2.Hours + carryHours), 24);

            return new Time(newHours, newMinutes, newSeconds);
        }
    }
}
