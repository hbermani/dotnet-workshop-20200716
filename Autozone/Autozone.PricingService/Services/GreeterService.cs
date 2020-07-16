using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Autozone.PricingService {
	public class PricerService : Pricer.PricerBase {
		private readonly ILogger<PricerService> _logger;
		public PricerService(ILogger<PricerService> logger) {
			_logger = logger;
		}

		public override Task<CarPriceResponse> CalculatePrice(CarPriceRequest request, ServerCallContext context) {
			return Task.FromResult(new CarPriceResponse {
				Price = 1000,
				Currency = "GBP"
			});
		}
	}
}
