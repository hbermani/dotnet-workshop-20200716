using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Autozone.PricingService.Services {
	public class PricerService : Pricer.PricerBase {
		private readonly ILogger<PricerService> _logger;
		public PricerService(ILogger<PricerService> logger) {
			_logger = logger;
		}

		public override Task<CarPriceResponse> CalculatePrice(CarPriceRequest request, ServerCallContext context) {
			var price = 1000;
			if (request.Make.Equals("tesla", StringComparison.InvariantCultureIgnoreCase)) price = 50000;
			if (request.Year < 1980) price = 10;

			return Task.FromResult(new CarPriceResponse {
				Price = price,
				Currency = "GBP"
			});
		}
	}
}
