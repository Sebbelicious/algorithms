using System;

namespace Mini_Project_2_Airport_Queuing_System
{
    public class Passenger : IComparable<Passenger>
    {
        private int Id { get; }
        private Time ArrivalTime { get; }
        public  Plane Plane { get; }
        public Category Category { get; }
        public Status Status = Status.Waiting;

        public Passenger(int id, Time arrivalTime, Category category, Plane plane)
        {
            Id = id;
            ArrivalTime = arrivalTime;
            Plane = plane;
            Category = category;
        }

        public override string ToString()
        {
            return $"{Id}) arrived {ArrivalTime} as {Category} and is {Status}";
        }

        public int CompareTo(Passenger other)
        {
            if (Category.CompareTo(other.Category) != 0)
                return Category.CompareTo(other.Category);
            return ArrivalTime.CompareTo(other.ArrivalTime);
        }
    }
}