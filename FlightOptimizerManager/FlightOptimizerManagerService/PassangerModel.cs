using System;
using System.Collections.Generic;
using System.Text;

namespace FlightOptimizerManagerService
{
    public class PassangerModel
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string FamilyCode { get; set; }
        public bool RequiresTwoSeats { get; set; }

    }
}
