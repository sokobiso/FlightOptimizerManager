using FlightOptimizerManagerService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FlightOptimizerManagerTest
{
    [TestClass]
    public class FlightOptimizerManagerServiceTests
    {
        [TestMethod]
        public void OptimizeFlight_AssignsFamiliesToSeats()
        {
            // Arrange
            var passangerModels = new List<PassangerModel>
            {
                new PassangerModel { Id = 1,  Age = 35, FamilyCode = "A", RequiresTwoSeats = true },
                new PassangerModel { Id = 2,  Age = 32, FamilyCode = "A", RequiresTwoSeats = true },
                new PassangerModel { Id = 3,  Age = 7, FamilyCode = "A", RequiresTwoSeats = false },
                new PassangerModel { Id = 4,  Age = 4, FamilyCode = "A", RequiresTwoSeats = false },
            };

            var flightOptimizer = new FlightOptimizer(passangerModels);

            // Act
            flightOptimizer.OptimizeFlight();

            // Assert
            Assert.AreEqual(flightOptimizer.seatingPlan.Count, 6);
        }

        [TestMethod]
        public void OptimizeFlight_AssignsRemainingPassengersToSeats()
        {
            // Arrange
            var passangerModels = new List<PassangerModel>
            {
                new PassangerModel { Id = 1,  Age = 35, RequiresTwoSeats = true },
                new PassangerModel { Id = 2,  Age = 32, RequiresTwoSeats = true },
                new PassangerModel { Id = 5,  Age = 32, RequiresTwoSeats = false }
            };

            var flightOptimizer = new FlightOptimizer(passangerModels);

            // Act
            flightOptimizer.OptimizeFlight();

            // Assert
            // Add your assertions to verify that single passengers are assigned to seats correctly
            // See output
            Assert.AreEqual(flightOptimizer.seatingPlan.Count, 5);
        }

        [TestMethod]
        public void OptimizeFlight_ReturnIstrueIFfamilyComposedFromTooAdult()
        {
            //Try to add 3 adults ids (1,2,3)
            // Arrange
            var passangerModels = new List<PassangerModel>
            {
                new PassangerModel { Id = 1,  Age = 35, FamilyCode = "A", RequiresTwoSeats = true },
                new PassangerModel { Id = 2,  Age = 32, FamilyCode = "A", RequiresTwoSeats = true },
                new PassangerModel { Id = 3,  Age = 32, FamilyCode = "A", RequiresTwoSeats = true },
                new PassangerModel { Id = 4,  Age = 7, FamilyCode = "A" },
                new PassangerModel { Id = 5,  Age = 4, FamilyCode = "A" },
            };

            var flightOptimizer = new FlightOptimizer(passangerModels);

            // Act
            flightOptimizer.OptimizeFlight();

            // Assert
            // Verify if 3 adults are added to family 
            // See output
            var family = flightOptimizer.Families.FirstOrDefault();
            Assert.IsTrue(family.Adults.Count <= 2);
        }

        [TestMethod]
        public void OptimizeFlight_ReturnIstrueIFfamilyComposedFromTreeChildrens()
        {
            //Try to add 3 adults ids (5,6,7)
            // Arrange
            var passangerModels = new List<PassangerModel>
            {
                new PassangerModel { Id = 1,  Age = 35, FamilyCode = "A", RequiresTwoSeats = true },
                new PassangerModel { Id = 2,  Age = 32, FamilyCode = "A", RequiresTwoSeats = true },
                new PassangerModel { Id = 4,  Age = 7, FamilyCode = "A" },
                new PassangerModel { Id = 5,  Age = 4, FamilyCode = "A" },
                new PassangerModel { Id = 6,  Age = 5, FamilyCode = "A" },
                new PassangerModel { Id = 7,  Age = 10, FamilyCode = "A" },

            };

            var flightOptimizer = new FlightOptimizer(passangerModels);

            // Act
            flightOptimizer.OptimizeFlight();

            // Assert
            // Verify if 4 childrens are added to family 
            // See output
            var family = flightOptimizer.Families.FirstOrDefault();
            Assert.IsTrue(family.Childrens.Count <= 3);
        }

        [TestMethod]
        public void OptimizeFlight_ReturnIsNullWhenTryToAddChildWithoutParents()
        {
            //Try to add 1 child without parent
            // Arrange
            var passangerModels = new List<PassangerModel>
            {
                new PassangerModel { Id = 7,  Age = 10, FamilyCode = "A" },
            };

            var flightOptimizer = new FlightOptimizer(passangerModels);

            // Act
            flightOptimizer.OptimizeFlight();

            // Assert
            // Verify if the family is not created
            // See output
            var family = flightOptimizer.Families.FirstOrDefault();
            Assert.IsNull(family);
        }

        [TestMethod]
        public void FindConsecutiveSeats_ReturnsCorrectIndex()
        {
            // Arrange
            var flightOptimizer = new FlightOptimizer();

            var seats = new List<int> { 1, 2, 4, 5, 6, 8 };

            // Act
            var index = flightOptimizer.FindConsecutiveSeats(seats, 3);

            // Assert
            Assert.AreEqual(2, index, "Incorrect index of consecutive seats");
        }

        // Add more test methods for other functionalities of the FlightOptimizer class
    }
}

