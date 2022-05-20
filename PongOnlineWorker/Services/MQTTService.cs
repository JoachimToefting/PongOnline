using System.Diagnostics;
using System.Text;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using PongOnlineWorker.Models;

namespace PongOnlineWorker.Service
{
	public class MQTTService
	{
		private const string topicBall = "gamedata/ball";
		private const string topicCommand = "gamedata/command";
		private const string topicScore = "gamedata/score";
		private readonly string hostname = "hazeltail958.cloud.shiftr.io";
		private readonly string username = "hazeltail958";
		private readonly string password = "FIcSUcWaLwow5JcA";
		private MqttFactory mqttFactory;
		private HttpClient httpClient;
		public MQTTService(HttpClient httpClient)
		{
			mqttFactory = new MqttFactory();
			Debug.WriteLine("Hello is me you are looking for");
			this.httpClient = httpClient;
		}

		public async Task ConnectAndSubscribe()
		{
			using (IMqttClient? mqttclient = mqttFactory.CreateMqttClient())
			{
				IMqttClientOptions? mqttClientOption = new MqttClientOptionsBuilder()
					.WithTcpServer(hostname)
					.WithCredentials(username, password)
					.WithClientId("Worker" + Guid.NewGuid().ToString())
					.Build();
				mqttclient.UseConnectedHandler(async message =>
				{
					Console.WriteLine("Successfully connected");
					var topicfilterBall = new MqttTopicFilterBuilder().WithTopic(topicBall).Build();
					var topicfilterCommand = new MqttTopicFilterBuilder().WithTopic(topicCommand).Build();
					var topicfilterScore = new MqttTopicFilterBuilder().WithTopic(topicScore).Build();
					await mqttclient.SubscribeAsync(topicfilterBall);
					await mqttclient.SubscribeAsync(topicfilterCommand);
					await mqttclient.SubscribeAsync(topicfilterScore);
				});
				mqttclient.UseDisconnectedHandler(message =>
				{
					Console.WriteLine("Disconnected");
				});
				mqttclient.UseApplicationMessageReceivedHandler(async message =>
				{
					var content = new StringContent(
						JsonConvert.SerializeObject(
							new MqttMessageDTO
							{
								Topic = message.ApplicationMessage.Topic,
								Message = Encoding.UTF8.GetString(message.ApplicationMessage.Payload)
							}),
						Encoding.UTF8,
						"application/json");
					var response = await httpClient.PostAsync("api/game", content);
					switch (message.ApplicationMessage.Topic)
					{
						case topicCommand:
							Console.WriteLine("Command: " + Encoding.UTF8.GetString(message.ApplicationMessage.Payload));
							break;
						case topicBall:
							Console.WriteLine("Ball: " + Encoding.UTF8.GetString(message.ApplicationMessage.Payload));
							break;
						case topicScore:
							Console.WriteLine("Score: " + Encoding.UTF8.GetString(message.ApplicationMessage.Payload));
							break;
						default:
							Console.WriteLine("Error bad topic" + message.ApplicationMessage.Topic);
							break;
					}
					Console.WriteLine("Response: " + response.StatusCode + " " + await response.Content.ReadAsStringAsync());

				});

				_ = await mqttclient.ConnectAsync(mqttClientOption);

				Console.WriteLine("Press anykey to close...");
				Console.ReadKey();
			}
		}
	}
}
