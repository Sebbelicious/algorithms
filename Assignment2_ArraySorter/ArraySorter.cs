using System;
using System.Collections;
using System.Collections.Generic;

namespace Assignment2_ArraySorter
{
    public class ArraySorter<T>  where T: IComparable<T>
    {
        public T[] Items {get; private set; }
        public int Size { get; private set; }
        private IComparer<T> _comparer;
        private bool _isAscending = true;
        private bool _isSorted = false;
        
        public ArraySorter(T[] items, int size, IComparer<T> comparer = null)
        {
            Items = items;
            Size = size;
            BuildHeap();
            _comparer = comparer;
        }

        private void BuildHeap()
        {
            int length = Size;
            for (int i = length/2-1; i >= 0; i--)
            {
                Heapify(length, i);
            }
        }
        private void Swap(int idxA, int idxB)
        {
            var tmp = Items[idxA];
            Items[idxA] = Items[idxB];
            Items[idxB] = tmp;
        }
        
        private bool Less(T first, T second)
        {
            if (_comparer == null)
            {
                if (_isAscending)
                {
                    return first.CompareTo(second) < 0;
                }

                return first.CompareTo(second) > 0;
            }
            return _comparer.Compare(first, second) < 0;
        }
        
        private void Heapify(int length, int i)
        {
            int largest = i;

            int left = i * 2 + 1;
            int right = i * 2 + 2;

            if (left < length && Less(Items[largest], Items[left]))
            {
                largest = left;
            }
            if (right < length && Less(Items[largest], Items[right]))
            {
                largest = right;
            }

            if (i != largest)
            {
                Swap(i, largest);
                Heapify(length, largest);
            }
        }
        
        public void Enqueue(T item)
        {
            IncreaseSize();
            Items[Size] = item;
            Size++;
            BuildHeap();
            _isSorted = false;
        }

        private void IncreaseSize()
        {
            if (Size+1 > Items.Length*0.75)
            {
                T[] newArr = new T[Items.Length* 2];
                for (int i = 0; i < Size; i++)
                {
                    newArr[i] = Items[i];
                }

                Items = newArr;
            }
        }

        public T Dequeue()
        {
            if (_isSorted)
            {
                BuildHeap();
                _isSorted = false;
            }
            T pop = Items[0];
            Items[0] = Items[Size - 1];
            Items[Size - 1] = default(T);
            Size--;
            Heapify(Size-1, 0);
            return pop;
        }

        public void SortAscending()
        {
            if (!_isAscending)
            {
                _isAscending = true;
                _isSorted = false;
                BuildHeap();
            }

            if (!_isSorted)
            {
                int length = Size;
                for (int i = length-1; i >= 0; i--)
                {
                    Swap(i, 0);
                    Heapify(i, 0);
                }

                _isSorted = true;
            }
        }

        public void SortDescending()
        {
            if (_isAscending)
            {
                _isAscending = false;
                _isSorted = false;
                BuildHeap();
            }

            if (!_isSorted)
            {
                int length = Size;
                for (int i = length-1; i >= 0; i--)
                {
                    Swap(i, 0);
                    Heapify(i, 0);
                }
                _isSorted = true;
            }
        }

        public void Sort(IComparer<T> comparer)
        {
            IComparer<T> tmp = _comparer;
            _comparer = comparer;
            if (!_isAscending)
            {
                _isAscending = true;
            }

            _isSorted = false;
            BuildHeap();
            SortAscending();
            _comparer = tmp;
            _isSorted = true;
        }
    }
}