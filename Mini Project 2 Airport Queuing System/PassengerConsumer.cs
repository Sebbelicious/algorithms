using System;
using System.Collections.Generic;
using Mini_Project_2_Airport_Queuing_System.Queues;

namespace Mini_Project_2_Airport_Queuing_System
{
    public class PassengerConsumer
    {
        public List<Plane> Planes;
        public IPriorityQueue<Passenger> Queue;
        public int ProcessingTicksLeft = 0;
        private Passenger _passenger;

        public PassengerConsumer(List<Plane> planes, IPriorityQueue<Passenger> queue)
        {
            Planes = planes;
            Queue = queue;
        }

        public void Tick(Clock clock)
        {
            if (ProcessingTicksLeft > 0)
            {
                ProcessingTicksLeft--;
                return;
            }


            if (Queue.IsEmpty()) return;
            Console.WriteLine("Before " + Queue);
            _passenger = Queue.Dequeue();

            var now = clock.Time;
            if (_passenger.Plane.DepartureTime.CompareTo(now) < 0)
            {
                _passenger.Status = Status.MissedPlane;
                Console.WriteLine($"Passenger {_passenger} missed the plane");
            }
            else
            {
                _passenger.Status = Status.Boarded;
                Console.WriteLine($"Passenger {_passenger} has boarded");
            }

            ProcessingTicksLeft = _passenger.Category switch
            {
                Category.LateToFlight => 60,
                Category.BusinessClass => 60,
                Category.Disabled => 180,
                Category.Family => 180,
                Category.Monkey => 60,
                _ => throw new ArgumentOutOfRangeException()
            };
            Console.WriteLine("After " + Queue);
            
        }
    }
}