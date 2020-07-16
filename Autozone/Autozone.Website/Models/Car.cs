
namespace Autozone.Website.Models {
	public class Car {
		public string Registration { get; set; }
		public string Colour { get; set; }
		public int Year { get; set; }
		public CarModel Model { get; set; }
	}

	public class CarModel {
		public string Make { get; set; }
		public string Name { get; set; }
	}
}
