using System;

namespace Mini_Project_2_Airport_Queuing_System.Queues
{
    public interface IPriorityQueue<T> : IQueue<T> where T : IComparable<T> 
    {
        
    }
}