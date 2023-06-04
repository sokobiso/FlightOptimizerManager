using System;
using System.Collections.Generic;
using System.Text;

namespace FlightOptimizerManagerService
{
    public class Children : Passenger
    {
        public override decimal TicketPrice()
        {
            return 150;
        }

        public override string Type()
        {
            return "Children";
        }
    }
}
