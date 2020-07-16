using System.Collections.Generic;
using Autozone.Website.Models;

namespace Autozone.Website.Data {
    public class CarDatabase {
        private readonly List<Car> cars = new List<Car>();

        public IEnumerable<Car> AllCars => cars;

        public CarDatabase() {
	        PopulateSampleData();
        }

        private void PopulateSampleData() {
	        var prius = new CarModel {
		        Make = "Toyota",
		        Name = "Prius"
	        };

            cars.Add(new Car {
                Model = prius,
                Registration = "AB17XYZ",
                Colour = "Beige",
                Year = 2017
            });
        }
    }
}