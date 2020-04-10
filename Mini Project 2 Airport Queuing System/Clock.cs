using System;
using System.Collections.Generic;
using System.Threading;

namespace Mini_Project_2_Airport_Queuing_System
{
    public class Clock
    {
        private const long SleepingTime = 10;
        private bool _running = true;
        private PassengerProducer _producer;
        private List<PassengerConsumer> _consumers;
        private long _millis;

        public Time Time => new Time(_millis);

        public Clock(PassengerProducer producer, List<PassengerConsumer> consumers, Time startTime)
        {
            _producer = producer;
            _consumers = consumers;
            _millis = startTime.Millis;
        }

        public void Stop()
        {
            _running = false;
        }

        public void Run()
        {
            try
            {
                while (_running)
                {
                    Thread.Sleep((int) SleepingTime);
                    _producer.Tick(this);

                    foreach (var consumer in _consumers)
                    {
                        consumer.Tick(this);
                    }
                    _millis += 1000;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}