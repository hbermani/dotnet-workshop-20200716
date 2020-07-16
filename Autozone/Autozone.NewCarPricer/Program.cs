using System;
using System.Threading.Tasks;
using Autozone.Messages;
using Autozone.PricingService;
using EasyNetQ;
using Grpc.Net.Client;
using Microsoft.AspNetCore.SignalR.Client;

namespace Autozone.NewCarPricer {
	class Program {
		private const string AMQP =
			"amqp://uzvpuvak:j1ulefFopg5uip8lNxKFXtwN7J03Dgqa@rattlesnake.rmq.cloudamqp.com/uzvpuvak";

		private static IBus bus = RabbitHutch.CreateBus(AMQP);
		private static Pricer.PricerClient pricerClient;

		private const string SIGNALR_URL = "https://workshop.ursatile.com:5001/newcarhub";

		private static readonly HubConnection hub = new HubConnectionBuilder()
			.WithUrl(SIGNALR_URL)
			.Build();

		static async Task Main(string[] args) {
			var grpc = GrpcChannel.ForAddress("https://workshop.ursatile.com:5003");
			pricerClient = new Pricer.PricerClient(grpc);
			Console.Write("Subscribing to messages!");

			await hub.StartAsync();

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
			var thing =
				$"Car {message.Make} {message.Model} ({message.Colour}, {message.Year}) costs {response.Price} {response.Currency}";
			hub.SendAsync("SendMessage", "autozone", thing);
			Console.WriteLine(thing);
		}
	}
}
