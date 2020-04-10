using System;
using System.Collections.Generic;
using System.Threading;
using Mini_Project_2_Airport_Queuing_System.Queues;

namespace Mini_Project_2_Airport_Queuing_System
{
    class Program
    {
        private static readonly List<Plane> Planes = new List<Plane>();
        private static PassengerProducer _producer;
        private static List<PassengerConsumer> _consumers = new List<PassengerConsumer>();
        private static IPriorityQueue<Passenger> _queue;
        private static Clock _clock;
        private const int NoPassengers = 10000;
        private const int ConsumerAmount = 4;
  
        private static void Setup() {
            for (var hour = 7; hour <= 22; hour++) {
                Planes.Add(new Plane(new Time(hour, 00, 00)));
            }

            // _queue = new NotPrioritisingPassengerArrayQueue(noPassengers);
            _queue = new PriorityQueue<Passenger>(NoPassengers);
            _producer = new PassengerProducer(Planes, _queue);
            for (var i = 0; i < ConsumerAmount; i++)
            {
                _consumers.Add(new PassengerConsumer(Planes, _queue));
            }
            _clock = new Clock(_producer, _consumers, new Time(05, 00, 00));
        }
  
        public static void Main(String[] args) {
            Setup();
            Console.WriteLine("Hello Airport");
            var newThread = new Thread(new ThreadStart(_clock.Run));
            newThread.Start(); 
            
    
        }
    }
}