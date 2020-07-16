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
		public IEnumerable<Car> Get() {
			return _db.AllCars;
		}


		// POST: api/Cars
		[HttpPost]
		public ActionResult Post([FromBody] Car car) {
			// add the new car to the database
			try {
				_db.AddCar(car);
				return Ok();
			} catch (DuplicateCarException ex) {
				return Conflict(ex.Message);
			}

		}

		//// GET: api/Cars/5
		//[HttpGet("{id}", Name = "Get")]
		//public string Get(int id) {
		//	return "value";
		//}


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
