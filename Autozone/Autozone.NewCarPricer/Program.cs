using System;
using Autozone.Messages;
using Autozone.PricingService;
using EasyNetQ;
using Grpc.Net.Client;

namespace Autozone.NewCarPricer {
	class Program {
		private const string AMQP =
			"amqp://uzvpuvak:j1ulefFopg5uip8lNxKFXtwN7J03Dgqa@rattlesnake.rmq.cloudamqp.com/uzvpuvak";

		private static IBus bus = RabbitHutch.CreateBus(AMQP);
		private static Pricer.PricerClient pricerClient;

		static void Main(string[] args) {
			var grpc = GrpcChannel.ForAddress("https://workshop.ursatile.com:5003");
			pricerClient = new Pricer.PricerClient(grpc);
			Console.Write("Subscribing to messages!");
			bus.Subscribe<NewCarListingMessage>($"newcarpricer", HandleNewCarListing);
		}

		private static void HandleNewCarListing(NewCarListingMessage message) {
			Console.WriteLine("New car listing received");
			var pricerRequest = new CarPriceRequest {
				Colour = message.Colour,
				Make = message.Make,
				Model = message.Model,
				Year = message.Year
			};
			var response = pricerClient.CalculatePrice(pricerRequest);
			Console.WriteLine($"Car costs {response.Price} {response.Currency}");
		}
	}
}
