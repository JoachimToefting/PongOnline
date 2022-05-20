using PongOnline.Models;

namespace PongOnline.Service
{
	public class GameService : IGameService
	{
		private const string topicBall = "gamedata/ball";
		private const string topicCommand = "gamedata/command";
		private const string topicScore = "gamedata/score";
		private readonly IStorageService _storageService;
		public GameService(IStorageService storageService)
		{
			_storageService = storageService;
		}

		public async Task MessageHandling(MqttMessageDTO mqttMessage)
		{
			switch (mqttMessage.Topic)
			{
				case topicCommand:
					if (mqttMessage.Message.Substring(0, 1) == "i")
					{
						await NewGame(mqttMessage.Message);
					}
					break;
				case topicScore:
					await UpdateScore(mqttMessage.Message);
					break;
				case topicBall:
					await UpdateBall(mqttMessage.Message);
					break;
				default:
					break;
			}
		}
		public async Task<int> NewGame(string message)
		{
			var splitmsg = message.Split(':');
			var game = new Game()
			{
				Player1 = new Player()
				{
					PlayerNumber = Convert.ToInt32(splitmsg[1].Substring(0, splitmsg[1].Length - 1)),
				},
				Player2 = new Player()
				{
					PlayerNumber = Convert.ToInt32(splitmsg[2])
				}
			};
			return await _storageService.CreateGame(game);
		}
		public async Task<int?> UpdateBall(string message)
		{
			Game? game = await _storageService.GetNewestGame();
			if (game == null)
			{
				throw new Exception("No Games");
			}
			game.BallStats.Add(await GetBallStatsAsync(message));
			return (await _storageService.UpdateGame(game))?.Id;
		}
		public async Task<int?> UpdateScore(string message)
		{
			Game? game = await _storageService.GetNewestGame();
			var msgsplit = message.Split(':');
			var player = Convert.ToInt32(msgsplit[1].Substring(0, msgsplit[1].Length - 1));
			if (game.Player1.PlayerNumber == player)
			{
				game.Player1.Score += Convert.ToInt32(msgsplit[2]);
			}
			else if(game.Player2.PlayerNumber == player)
			{
				game.Player2.Score += Convert.ToInt32(msgsplit[2]);
			}
			else
			{
				return -1;
			}
			return (await _storageService.UpdateGame(game))?.Id;
		}
		private async Task<BallStats> GetBallStatsAsync(string message)
		{
			var msgsplit = message.Split(':');
			var ballStats = new BallStats
			{
				Player = new Player
				{
					PlayerNumber = Convert.ToInt32(msgsplit[1].Substring(0, msgsplit[1].Length - 1))
				},
				Ypos = Convert.ToInt32(msgsplit[2].Substring(0, msgsplit[2].Length - 1)),
				Dir = msgsplit[3] == "0" ? false : true
			};
			return ballStats;
		}

	}
}
