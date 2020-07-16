using Microsoft.AspNetCore.Mvc;

namespace Autozone.Website.Controllers.Api {
	[Route("api")]
	[ApiController]
	public class ApiRootController : ControllerBase {
		public object Get() {
			return new {
				_links = new {
					cars = new {
						name = "list cars",
						href = "/api/cars/",
						method = "GET"
					}
				},
				title = "Autozone Cars API"
			};
		}
	}
}