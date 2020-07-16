using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autozone.Website.Data;
using Autozone.Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autozone.Website.Controllers.Api {
	[Route("api/[controller]")]
	[ApiController]
	public class CarsController : ControllerBase {
		private readonly CarDatabase _db;

		public CarsController(CarDatabase db) {
			_db = db;
		}

		// GET: api/Cars
		[HttpGet]
		public object Get() {

			var cars = _db.AllCars.ToList();
			return new {
				_actions = new {
					add_car = new {
						name = "add_car",
						title = "Add a new car to the database",
						href = "/api/cars",
						method = "post",
						schema = "https://autozone.com/data/schema/new_car.json"
					}
				},
				count = cars.Count,
				index = 0,
				items = cars
			};
		}


		// POST: api/Cars
		[HttpPost]
		public ActionResult Post([FromBody] Car car) {
			// add the new car to the database
			try {
				_db.AddCar(car);
				var result = new CreatedResult($"/api/cars/{car.Registration}", car);
				return result;
			} catch (DuplicateCarException ex) {
				return Conflict(ex.Message);
			}

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
