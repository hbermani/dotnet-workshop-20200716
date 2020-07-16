using System;
using System.IO;
using Autozone.Messages;
using EasyNetQ;

namespace Autozone.NewCarAuditLog {
	class Program {

		private const string AMQP =
			"amqp://uzvpuvak:j1ulefFopg5uip8lNxKFXtwN7J03Dgqa@rattlesnake.rmq.cloudamqp.com/uzvpuvak";

		private static IBus bus = RabbitHutch.CreateBus(AMQP);
		static void Main(string[] args) {
			bus.Subscribe<NewCarListingMessage>($"newcarauditlog_{Environment.MachineName}", HandleNewCarListing);
		}

		private static void HandleNewCarListing(NewCarListingMessage message) {
			File.AppendAllText("D:\\car_log.csv",
				$"{DateTime.Now:O},{message.Registration},{message.Make},{message.Year}\n"
				);
		}
	}
}
