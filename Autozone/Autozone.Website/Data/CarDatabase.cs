using System;
using System.Collections.Generic;
using System.Linq;
using Autozone.Website.Models;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace Autozone.Website.Data {
	public class CarDatabase {

		private readonly List<Car> cars = new List<Car>();

		public CarDatabase() {
			PopulateSampleData();
		}
		public IEnumerable<Car> AllCars => cars;

		public void AddCar(Car car) {
			var existingCar = FindCar(car.Registration);
			if (existingCar == null) {
				cars.Add(car);
			} else {
				throw new DuplicateCarException(
					$"There is already a car with registration {car.Registration} in the system.");
			}
		}

		public Car FindCar(string reg) {
			return cars.FirstOrDefault(car =>
				car.Registration.Equals(reg, StringComparison.InvariantCultureIgnoreCase));
		}

		private void PopulateSampleData() {
			var prius = new CarModel {
				Code = "prius",
				Make = "Toyota",
				Name = "Prius"
			};

			var bmw5 = new CarModel { Code = "bmw5", Make = "BMW", Name = "5 Series" };
			var golf = new CarModel { Code = "golf", Make = "Volkswagen", Name = "Golf" };

			cars.Add(new Car {
				Registration = "AB16XYZ",
				Model = prius,
				Colour = "Blue",
				Year = 2016
			});

			cars.Add(new Car {
				Registration = "DF09JKL",
				Model = bmw5,
				Colour = "Black",
				Year = 2009
			});


			cars.Add(new Car {
				Model = prius,
				Registration = "AB17XYZ",
				Colour = "Beige",
				Year = 2017
			});

			cars.Add(new Car {
				Model = golf,
				Registration = "E332ABC",
				Colour = "Red",
				Year = 1988
			});
		}
	}
}