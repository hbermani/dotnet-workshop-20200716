using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeter;

namespace GrpcGreeterClient {
	class Program {
		static async Task Main(string[] args) {
            using var grpc = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(grpc);
            var request = new HelloRequest { Name = "Everybody!" };
            var response = await client.SayHelloAsync(request);
            Console.WriteLine(response.Message);
            Console.WriteLine("Press a key to exit...");
            Console.ReadKey();
		}
	}
}
