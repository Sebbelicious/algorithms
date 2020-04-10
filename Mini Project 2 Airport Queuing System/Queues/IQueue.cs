namespace Mini_Project_2_Airport_Queuing_System.Queues
{
    public interface IQueue<T>
    {
        void Enqueue(T item);

        T Dequeue();

        T Peek();

        bool IsEmpty();

    }
}