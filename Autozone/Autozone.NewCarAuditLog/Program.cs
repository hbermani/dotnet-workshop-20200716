using System;
using EasyNetQ;

namespace Autozone.NewCarAuditLog {
	class Program {

		private const string AMQP =
			"amqp://uzvpuvak:j1ulefFopg5uip8lNxKFXtwN7J03Dgqa@rattlesnake.rmq.cloudamqp.com/uzvpuvak";

		private static IBus bus = RabbitHutch.CreateBus(AMQP);
		static void Main(string[] args) {
			bus.Subscribe<string>("newcarauditlog", s => Console.WriteLine(s));
			Console.WriteLine("Hello World!");
		}
	}
}
