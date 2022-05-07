using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTimePeroid
{
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        public long Period { get; init; }

        public TimePeriod(int hours, int minutes, int seconds)
        {
            if (hours < 0 || minutes < 0 || seconds < 0 || seconds >= 60 || minutes >= 60)
                throw new ArgumentException("Invalid arguments");

            Period = hours*3600 + minutes*60 + seconds;
        }

        public TimePeriod(int minutes, int seconds)
        {
            if (minutes < 0 || seconds < 0 || seconds >= 60)
                throw new ArgumentException("Invalid arguments");

            Period = minutes * 60 + seconds;
        }

        public TimePeriod(int seconds)
        {
            if (seconds < 0)
                throw new ArgumentException("Invalid arguments");

            Period = seconds;
        }

        public TimePeriod(long seconds)
        {
            if (seconds < 0)
                throw new ArgumentException("Invalid arguments");

            Period = seconds;
        }

        public TimePeriod(string timePeriod)
        {
            if (timePeriod is null || string.IsNullOrEmpty(timePeriod))
                throw new ArgumentNullException("Invalid timePeriod string");

            string[] timeParts = timePeriod.Split(':');

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
                throw new ArgumentException("Invalid timePeriod string");
            }

            if (hours < 0 || minutes < 0 || seconds < 0 || seconds >= 60 || minutes >= 60)
                throw new ArgumentException("Invalid arguments");

            Period = hours * 3600 + minutes * 60 + seconds;
        }

        public override string ToString()
        {
            long hours = Period / 3600;
            long minutes = Period / 60 % 60;
            long seconds = Period % 60;

            return $"{hours.ToString().PadLeft(2, '0')}:{minutes.ToString().PadLeft(2, '0')}:{seconds.ToString().PadLeft(2, '0')}";
        }

        public int CompareTo(TimePeriod other)
        {
            return Period.CompareTo(other.Period);
        }

        public bool Equals(TimePeriod other)
        {
            return Period == other.Period;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            TimePeriod timePeriod = (TimePeriod)obj;
            return Equals(timePeriod);
        }

        public override int GetHashCode()
        {
            return Period.GetHashCode();
        }

        public static bool operator ==(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return timePeriod1.Equals(timePeriod2);
        }

        public static bool operator !=(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return !(timePeriod1.Equals(timePeriod2));
        }

        public static bool operator <(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return timePeriod1.CompareTo(timePeriod2) < 0;
        }
        public static bool operator >(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return timePeriod1.CompareTo(timePeriod2) > 0;
        }

        public static bool operator <=(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return timePeriod1.CompareTo(timePeriod2) <= 0;
        }

        public static bool operator >=(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return timePeriod1.CompareTo(timePeriod2) >= 0;
        }

        public static TimePeriod operator +(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return new TimePeriod(timePeriod1.Period + timePeriod2.Period);
        }

        public static TimePeriod operator -(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            if(timePeriod1 >= timePeriod2)
                return new TimePeriod(timePeriod1.Period - timePeriod2.Period);
            return new TimePeriod(timePeriod2.Period - timePeriod1.Period);
        }



    }
}
