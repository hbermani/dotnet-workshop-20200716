using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autozone.Messages;
using Autozone.Website.Data;
using Autozone.Website.Models;
using EasyNetQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autozone.Website.Controllers.Api {
	[Route("api/[controller]")]
	[ApiController]
	public class CarsController : ControllerBase {
		
		private readonly CarDatabase _db;
		private readonly IBus _bus;

		public CarsController(CarDatabase db, IBus bus) {
			_db = db;
			_bus = bus;
		}

		// GET: api/Cars
		[HttpGet]
		public object Get() {

			var cars = _db.AllCars.ToList();
			var items = cars.Select(car => car.ToDynamic()).ToList();
			foreach (var item in items) {
				item._links = new {
					model = new {
						href = $"/api/cars/models/{item.Model.Code}"
					}
				};
				item.foo = "bar";
			}
			return new {
				_actions = new {
					add_car = new {
						name = "add_car",
						title = "Add a new car to the database",
						href = "/api/cars",
						method = "post",
						schema = "https://autozone.com/data/schema/new_car.json"
					},
				},
				count = cars.Count,
				index = 0,
				items = items
			};
		}


		// POST: api/Cars
		[HttpPost]
		public ActionResult Post([FromBody] Car car) {
			// add the new car to the database
			try {
				_db.AddCar(car);
				PublishNotification(car);
				var result = new CreatedResult($"/api/cars/{car.Registration}", car);
				return result;
			} catch (DuplicateCarException ex) {
				return Conflict(ex.Message);
			}
		}

		private void PublishNotification(Car car) {
			var message = new NewCarListingMessage {
				Registration = car.Registration,
				Make = car.Model.Make,
				Model = car.Model.Name,
				Colour = car.Colour,
				Year = car.Year
			};
			_bus.Publish(message);
		}

		// GET: api/Cars/AB12XYZ
		[HttpGet("{id}", Name = "GetCarByRegistration")]
		public ActionResult Get(string id) {
			var car = _db.FindCar(id);
			if (car == null) return NotFound($"There is no car with registration {id} in our database.");
			return Ok(car);
		}


		//// PUT: api/Cars/5
		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody] string value) {
		//}

		//// DELETE: api/ApiWithActions/5
		//[HttpDelete("{id}")]
		//public void Delete(int id) {
		//}
	}
}
