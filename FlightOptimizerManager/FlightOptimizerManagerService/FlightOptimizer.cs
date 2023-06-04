using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightOptimizerManagerService
{
    public class FlightOptimizer
    {
        public Dictionary<int, Passenger> seatingPlan;

        private List<Family> families;

        private List<Adult> singlePassangers;
        public List<Adult> SinglePassangers { get => singlePassangers; set => singlePassangers = value; }
        public List<Family> Families { get => families; set => families = value; }
        public FlightOptimizer()
        {

        }
        public FlightOptimizer(List<PassangerModel> passangerModels)
        {
            Families = new List<Family>();
            SinglePassangers = new List<Adult>();
            seatingPlan = new Dictionary<int, Passenger>();

            Dictionary<string, Family> familyMap = new Dictionary<string, Family>();
            foreach (var passenger in passangerModels)
            {
                if (!string.IsNullOrEmpty(passenger.FamilyCode) && passenger.FamilyCode != "-")
                {
                    if (!familyMap.ContainsKey(passenger.FamilyCode))
                    {
                        Family newFamily = new Family();
                        // verify the existence of a child's parents
                        if (passenger.Age < 12)
                        {
                            var adultsExistForChild = passangerModels.Any(p => p.Age > 12 && p.FamilyCode == passenger.FamilyCode);
                            if (adultsExistForChild)
                            {
                                AddNewMemberToFamily(familyMap, passenger, newFamily);
                            }
                            else
                            {
                                Console.WriteLine($"cannot add child {passenger.Id} without parent");
                            }
                        }
                        else
                        {
                            AddNewMemberToFamily(familyMap, passenger, newFamily);
                        }
                    }
                    else
                    {
                        familyMap[passenger.FamilyCode].AddMember(passenger);
                    }
                }
                else
                {
                    if (passenger.Age > 12)
                    {
                        Adult adult = new Adult { Age = passenger.Age, ID = passenger.Id, RequiresTwoSeats = passenger.RequiresTwoSeats };
                        SinglePassangers.Add(adult);
                    }
                    else
                    {
                        Console.WriteLine($"The age of the member {passenger.Id} must be over 12 years old");
                        //throw new Exception("The age of the member must be over 12 years old");
                    }
                }
            }
        }

        private void AddNewMemberToFamily(Dictionary<string, Family> familyMap, PassangerModel passenger, Family newFamily)
        {
            newFamily.AddMember(passenger);
            familyMap.Add(passenger.FamilyCode, newFamily);
            Families.Add(newFamily);
        }

        public void OptimizeFlight()
        {
            AssignFamiliesToSeats(Families);
            AssignRemainingPassengersToSeats();
            DisplaySeatingPlan();
            CalculateTotalRevenue();
        }

        private void AssignFamiliesToSeats(List<Family> families)
        {
            foreach (Family family in families)
            {
                List<int> availableSeats = GetAvailableSeats();

                // Assign seats for families with normal seating requirements
                foreach (var member in family.Adults)
                {
                    if (member.RequiresTwoSeats)
                    {
                        SetTooSeats(availableSeats, member);
                    }
                    else
                    {
                        int seat = availableSeats.FirstOrDefault();
                        seatingPlan.Add(seat, member);
                        availableSeats.Remove(seat);
                    }
                }

                foreach (var member in family.Childrens)
                {
                    SetOneSeat(availableSeats, member);
                }
            }
        }

        private void SetTooSeats(List<int> availableSeats, Adult member)
        {
            var requiredSeats = 2;
            int startIndex = FindConsecutiveSeats(availableSeats, requiredSeats);
            if (startIndex != -1)
            {
                var seat1 = availableSeats[startIndex];
                seatingPlan.Add(seat1, member);
                var seat2 = availableSeats[startIndex + 1];
                seatingPlan.Add(seat2, member);
                availableSeats.Remove(seat1);
                availableSeats.Remove(seat2);
            }
        }

        private void AssignRemainingPassengersToSeats()
        {
            List<int> availableSeats = GetAvailableSeats();
            // Assign seats for singlePassangers

            foreach (Adult passenger in SinglePassangers)
            {
                if (!seatingPlan.ContainsValue(passenger))
                {
                    if (passenger.RequiresTwoSeats)
                    {
                        SetTooSeats(availableSeats, passenger);
                    }
                    else
                    {
                        SetOneSeat(availableSeats, passenger);
                    }
                }
            }
        }

        private void SetOneSeat(List<int> availableSeats, Passenger passenger)
        {
            int seat = availableSeats.FirstOrDefault();
            seatingPlan.Add(seat, passenger);
            availableSeats.Remove(seat);
        }

        private List<int> GetAvailableSeats()
        {
            List<int> allSeats = Enumerable.Range(1, 200).ToList();
            List<int> assignedSeats = seatingPlan.Keys.ToList();
            List<int> availableSeats = allSeats.Except(assignedSeats).ToList();
            return availableSeats;
        }

        public int FindConsecutiveSeats(List<int> seats, int count)
        {
            for (int i = 0; i < seats.Count - count + 1; i++)
            {
                bool isConsecutive = true;
                for (int j = 0; j < count - 1; j++)
                {
                    if (seats[i + j] + 1 != seats[i + j + 1])
                    {
                        isConsecutive = false;
                        break;
                    }
                }
                if (isConsecutive)
                {
                    return i;
                }
            }
            return -1;
        }

        private void DisplaySeatingPlan()
        {
            foreach (KeyValuePair<int, Passenger> kvp in seatingPlan.OrderBy(x => x.Key))
            {
                Console.WriteLine($"Seat {kvp.Key}: {kvp.Value.Type()} (ID: {kvp.Value.ID})");
            }
        }

        private void CalculateTotalRevenue()
        {
            decimal totalRevenue = seatingPlan.Values.Sum(passenger => passenger.TicketPrice());
            Console.WriteLine($"Total Revenue: {totalRevenue} €");
        }
    }
}
