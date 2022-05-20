// See https://aka.ms/new-console-template for more information
using PongOnlineWorker.Service;
using System.Net.Http.Headers;

string url = "https://localhost:44336/";

Console.WriteLine("Pong online worker.");
HttpClient client = new HttpClient();
client.BaseAddress = new Uri(url);
Console.WriteLine("Press any key to start collecting and send data");
Console.ReadKey();
var mqttService = new MQTTService(client);
await mqttService.ConnectAndSubscribe();
