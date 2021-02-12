using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
//using Microsoft.AspNet.SignalR.Client;

namespace BigDataTechnology.Monitoring
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wellcome BigDataTechnology Monitoring Tool ");
            Connect();
            Console.ReadKey();
        }
        static async Task Connect()
        {
            try
            {
                string url = "http://localhost:58023/chathub";

                HubConnection connection = new HubConnectionBuilder().WithUrl(url).Build();
                await connection.StartAsync();

                Console.WriteLine("Connected to the Hub on " + url);

                connection.On("ReceiveMessage", (string server, string message) =>
                {
                    Console.WriteLine($"{server}: {message}\r\n");
                });

            }
            catch (Exception ex)
            {
                Exception sd = ex;
            }
            Console.ReadKey();
        }
    }
}
