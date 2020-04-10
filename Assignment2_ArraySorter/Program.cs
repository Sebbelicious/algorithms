using System;
using System.Collections;
using System.Collections.Generic;

namespace Assignment2_ArraySorter
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Data[] test = new Data[10];
            
            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                int num  = rnd.Next(1, 100);
                int num2  = rnd.Next(1, 100);
                test[i] = new Data(num, num2, "Data priority1: " + num + ", priority2: " + num2);
            }
            
            ArraySorter<Data> arraySorter = new ArraySorter<Data>(test, test.Length);

            Console.WriteLine("Print random numbers");
            
            
            foreach (var obj in test)
            {
                Console.WriteLine(obj);
                
            }
            
            Console.WriteLine("----------------------");
            Console.WriteLine("Print ascending numbers");
            
            arraySorter.SortAscending();

            foreach (var item in arraySorter.Items)
            {
                Console.WriteLine(item);
            }
            /*
            Console.WriteLine("----------------------");
            Console.WriteLine("Print descending numbers");
            arraySorter.SortDescending();
            
            foreach (var item in arraySorter.Items)
            {
                Console.WriteLine(item);
            }
            */
            Console.WriteLine("----------------------");
            Console.WriteLine("Print ascending after enqueue numbers");
            
            Data data1 = new Data(70, 20, "Data priority1: 70, priority2: 20");
            Data data2 = new Data(50, 10, "Data priority1: 50, priority2: 10");
            Data data3 = new Data(80, 15, "Data priority1: 80, priority2: 15");
            
            arraySorter.Enqueue(data1);
            arraySorter.Enqueue(data2);
            arraySorter.Enqueue(data3);
            arraySorter.SortAscending();
            foreach (var item in arraySorter.Items)
            {
                Console.WriteLine(item);
            }
            
            Console.WriteLine("----------------------");
            Console.WriteLine("Print ascending after dequeue numbers");
            
            Console.WriteLine("dequeued " + arraySorter.Dequeue());
            Console.WriteLine("dequeued " + arraySorter.Dequeue());
            arraySorter.SortAscending();
            foreach (var item in arraySorter.Items)
            {
                Console.WriteLine(item);
            }
            
            Console.WriteLine("----------------------");
            Console.WriteLine("Print ascending numbers");
            
            arraySorter.SortAscending();

            foreach (var item in arraySorter.Items)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("----------------------");
            Console.WriteLine("Print ascending with comparator on priority2 numbers");
            
            IComparer<Data> comp = new Priority2Comparer();
            arraySorter.Sort(comp);
            foreach (var item in arraySorter.Items)
            {
                Console.WriteLine(item);
            }
            
            
        }
    }
}