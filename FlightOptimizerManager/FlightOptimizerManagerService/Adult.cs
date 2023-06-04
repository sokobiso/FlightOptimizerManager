using System;
using System.Collections.Generic;
using System.Text;

namespace FlightOptimizerManagerService
{
    public class Adult : Passenger
    {
        public bool RequiresTwoSeats { get; set; }

        public override decimal TicketPrice()
        {
            if (RequiresTwoSeats)
            {
                return 500;
            };
            return 250;
        }

        public override string Type()
        {
            return "Adult";
        }
    }
}
