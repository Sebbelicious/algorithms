using System;
using System.Collections.Generic;

namespace Mini_Project_2_Airport_Queuing_System.Queues
    {
        public class PriorityQueue<T> : IPriorityQueue<T> where T : Passenger
        {
        private T[] _items;
        private int _size;
        private Dictionary<Enum, int> categoryCount = new Dictionary<Enum, int>();

        public PriorityQueue(int size)
        {
            _items = new T[size];
            _size = 0;
            
            foreach (Enum category in Enum.GetValues(typeof(Category)))
            {
                categoryCount.Add(category, 0);
            }
        }
        
        public void Enqueue(T item)
        {
            if (_size == _items.Length)
                throw new Exception("Cannot add to full queue");
            _items[_size] = item;
            _size++;
            categoryCount[item.Category]++;
            Swim(_size-1);
        }
        
        private void Swim(int i)
        {
            var parent = (i - 1) / 2;

            if (Less(_items[i], _items[parent]))
            { 
                Swap(i, parent);
                Swim(parent);
            }
            
        }

        public T Dequeue()
        {
            if (_size == 0)
                throw new InvalidOperationException("Cannot remove from empty queue");
            var pop = _items[0];
            _items[0] = _items[_size - 1];
            _items[_size - 1] = default;
            _size--;
            categoryCount[pop.Category]--;
            Sink();
            
            return pop;
        }
        
        private void Sink(int i = 0)
        {
            var topPrio = i;

            var left = i * 2 + 1;
            var right = i * 2 + 2;

            if (left < _size && Less(_items[left], _items[topPrio]))
            {
                topPrio = left;
            }

            if (right < _size && Less(_items[right], _items[topPrio]))
            {
                topPrio = right;
            }

            if (i != topPrio)
            {
                Swap(i, topPrio);
                Sink(topPrio);
            }
        }

        public T Peek()
        {
            if (_size == 0)
                throw new InvalidOperationException("Cannot peek into empty queue");
            return _items[0];
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }
        
        private void Swap(int idxA, int idxB)
        {
            var tmp = _items[idxA];
            _items[idxA] = _items[idxB];
            _items[idxB] = tmp;
        }

        private bool Less(T first, T second)
        {
            return first.CompareTo(second) < 0;
        }

        public override string ToString()
        {
            var res = "Count of each category in queue \n";

            foreach (var KV in categoryCount)
            {
                res += $"{KV.Key} {KV.Value} \n";
            }

            return res;
        }
        }
    }