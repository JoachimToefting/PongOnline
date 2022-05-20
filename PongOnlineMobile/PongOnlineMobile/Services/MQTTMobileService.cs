using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PongOnlineMobile.Services
{
	public static class MQTTMobileService
	{
		private const string topicCommand = "gamedata/command";
		private const string hostname = "hazeltail958.cloud.shiftr.io";
		private const string username = "hazeltail958";
		private const string password = "FIcSUcWaLwow5JcA";
		private static MqttFactory mqttFactory;

		public static async Task ConnectAndSendReset()
		{
			mqttFactory = new MqttFactory();
			using (IMqttClient? mqttclient = mqttFactory.CreateMqttClient())
			{
				IMqttClientOptions? mqttClientOption = new MqttClientOptionsBuilder()
					.WithTcpServer(hostname)
					.WithCredentials(username, password)
					.WithClientId("Sender" + Guid.NewGuid().ToString())
					.Build();

				_ = await mqttclient.ConnectAsync(mqttClientOption);

				MqttApplicationMessageBuilder msgbuilder = new MqttApplicationMessageBuilder()
					.WithTopic(topicCommand)
					.WithPayload("r");

				await mqttclient.PublishAsync(msgbuilder.Build());

				await mqttclient.DisconnectAsync();
			}
		}
	}
}
