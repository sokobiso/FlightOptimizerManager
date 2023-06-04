using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightOptimizerManagerService
{
    public class Family
    {
        public string FamilyCode { get; set; }

        public List<Adult> Adults { get; set; }
        public List<Children> Childrens { get; set; }

        public Family()
        {
            Adults = new List<Adult>();
            Childrens = new List<Children>();
        }

        public void AddMember(PassangerModel passenger)
        {
            if (passenger.Age > 12)
            {
                if (Adults.Count == 2)
                {
                    Console.WriteLine($"Passenger {passenger.Id} not added, each family consists of a maximum of 2 adults");
                }
                else
                {
                    Adults.Add(new Adult
                    {
                        Age = passenger.Age,
                        ID = passenger.Id,
                        RequiresTwoSeats = passenger.RequiresTwoSeats
                    });
                }
            }
            else
            {
                if (Childrens.Count == 3)
                {
                    Console.WriteLine($"Passenger {passenger.Id} not added, each family consists of a maximum of 3 childrens");
                }
                else
                {
                    Childrens.Add(new Children
                    {
                        ID = passenger.Id,
                        Age = passenger.Age,
                    });
                }
            }
        }

        public decimal CalculateTotalPrice()
        {
            return Adults.Sum(member => member.TicketPrice()) + Childrens.Sum(member => member.TicketPrice());
        }
    }
}
