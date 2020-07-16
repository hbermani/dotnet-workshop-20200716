using System;

namespace Autozone.Messages {
	public class NewCarListingMessage {
		public string Make { get; set; }
		public string Model { get; set; }
		public string Colour { get; set; }
		public string Registration { get; set; }
		public int Year { get; set; }
	}
}