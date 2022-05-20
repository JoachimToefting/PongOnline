using PongOnline.Models;

namespace PongOnline.Service
{
    public interface IStorageService
    {
        Task<int> CreateGame(Game game);
        Task<Game?> GetGameById(int id);
        Task<List<Game>> GetGames();
        Task<Game?> UpdateGame(Game game);
        Task<Game> GetNewestGame();
    }
}