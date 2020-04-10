using System;

namespace Assignment2_ArraySorter
{
    public class Data : IComparable<Data>
    {
        private readonly int _priority;
        public int Priority2 { get; private set; }
        private readonly string _data;
        

        public Data(int priority, int priority2, string data)
        {
            _priority = priority;
            _data = data;
            Priority2 = priority2;
        }

        public int CompareTo(Data other)
        {
         
            if (_priority - other._priority < 0)
            {
                return -1;
            }

            return _priority - other._priority > 0 ? 1 : 0;
        }

        public override string ToString()
        {
            return _data;
        }
    }
}