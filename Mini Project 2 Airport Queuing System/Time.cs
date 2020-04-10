using System;

namespace Mini_Project_2_Airport_Queuing_System
{
    public class Time : IComparable<Time>
    {
        public long Millis { get; }

        public Time(long millis)
        {
            Millis = millis;
        }

        public Time(int hour, int minute, int second)
        {
            Millis = ((((long) hour) * 60 + minute) * 60 + second) * 1000;
        }


        private static string Two(long number)
        {
            return number >= 10 ? $"{number}" : $"0{number}";
        }

        public int CompareTo(Time other)
        {
            if (Millis < other.Millis) return -1;
            if (Millis > other.Millis) return 1;
            return 0;
        }

        public override string ToString()
        {
            var s = Millis / 1000;
            var m = s / 60;
            var h = m / 60;
            s %= 60;
            m %= 60;
            return $"{Two(h)}:{Two(m)}:{Two(s)}";
        }
    }
}