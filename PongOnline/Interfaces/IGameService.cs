using PongOnline.Models;

namespace PongOnline.Service
{
	public interface IGameService
	{
		Task MessageHandling(MqttMessageDTO mqttMessage);
		Task<int> NewGame(string message);
		Task<int?> UpdateBall(string message);
		Task<int?> UpdateScore(string message);
	}
}