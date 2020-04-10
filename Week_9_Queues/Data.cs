using System;
using System.Collections.Generic;

namespace Queues
{
    public class Data : IComparable<Data>
    {
        public int Priority;
        public string data;


        public int CompareTo(Data other)
        {
            return Priority-other.Priority;
        }
    }
}