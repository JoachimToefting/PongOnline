using PongOnline.Models;

namespace PongOnline.Service
{
	public class StorageService : IStorageService
	{
		private List<Game> games;

		public StorageService()
		{
			games = new List<Game>();
			var player1 = new Player
			{
				Id = 0,
				PlayerNumber = 2,
				Score = 5,
				Win = true
			};
			var player2 = new Player
			{
				Id = 0,
				PlayerNumber = 3,
				Score = 2,
				Win = false
			};
			games.Add(new Game
			{
				Id = 0,
				Player1 = player1,
				Player2 = player2,
				BallStats = new List<BallStats>()
				{
					new BallStats()
					{
						Id = 0,
						Dir = true,
						Player = player1,
						Ypos = 24
					},
					new BallStats()
					{
						Id = 0,
						Dir = true,
						Player = player2,
						Ypos = 57
					}
				}
			});
		}
		public async Task<Game?> GetGameById(int id)
		{
			return games.Find(x => x.Id == id);
		}
		public async Task<List<Game>> GetGames()
		{
			return games;
		}
		public async Task<Game> GetNewestGame()
		{
			return games.Last();
		}
		public async Task<int> CreateGame(Game game)
		{
			game.Id = games.Count();
			games.Add(game);
			return game.Id;
		}
		public async Task<Game?> UpdateGame(Game game)
		{
			games.RemoveAll(x => x.Id == game.Id);
			games.Add(game);
			return game;
		}

	}
}
