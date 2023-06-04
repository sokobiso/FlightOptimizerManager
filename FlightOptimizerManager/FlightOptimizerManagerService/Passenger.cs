using System;
using System.Collections.Generic;
using System.Text;

namespace FlightOptimizerManagerService
{
    abstract public class Passenger
    {
        public abstract decimal TicketPrice();
        public abstract string Type();

        public int ID { get; set; }
        public int Age { get; set; }
    }
}
