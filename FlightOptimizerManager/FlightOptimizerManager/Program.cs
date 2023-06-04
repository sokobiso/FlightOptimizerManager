using FlightOptimizerManagerService;
using System;
using System.Collections.Generic;

namespace FlightOptimizerManager
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PassangerModel> passengers = new List<PassangerModel>
        {
            new PassangerModel { Id = 1,  Age = 35, FamilyCode = "A", RequiresTwoSeats = true },
            new PassangerModel { Id = 2,  Age = 32, FamilyCode = "A", RequiresTwoSeats = true },
            new PassangerModel { Id = 9,  Age = 32, FamilyCode = "A", RequiresTwoSeats = true },
            new PassangerModel { Id = 3,  Age = 7, FamilyCode = "A", RequiresTwoSeats = false },
            new PassangerModel { Id = 4,  Age = 4, FamilyCode = "A", RequiresTwoSeats = false },
            new PassangerModel { Id = 7,  Age = 4, FamilyCode = "A" },
            new PassangerModel { Id = 8,  Age = 4, FamilyCode = "A" },
            new PassangerModel { Id = 5,  Age = 35, RequiresTwoSeats = true },
            new PassangerModel { Id = 6,  Age = 4 },
            // ... add more passengers

        };

            FlightOptimizer optimizer = new FlightOptimizer(passengers);
            optimizer.OptimizeFlight();

            Console.ReadLine();

        }
    }
}
