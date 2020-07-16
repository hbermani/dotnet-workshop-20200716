using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autozone.Website.Data;
using Microsoft.AspNetCore.Mvc;

namespace Autozone.Website.Controllers {
	public class CarsController : Controller {
		private readonly CarDatabase _db;

		public CarsController(CarDatabase DB) {
			_db = DB;
		}

		public IActionResult Index() {
			var allCars = _db.AllCars;
			return View(allCars);
		}
	}
}