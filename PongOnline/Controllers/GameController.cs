using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PongOnline.Models;
using PongOnline.Service;

namespace PongOnline.Controllers
{
	[Authorize("ReadPolicy")]
	[Route("api/[controller]")]
	[ApiController]
	public class GameController : ControllerBase
	{
		private readonly IGameService _gameService;
		private readonly IStorageService _storageService;
		public GameController(IGameService gameService, IStorageService storageService)
		{
			_gameService = gameService;
			_storageService = storageService;
		}
		//POST api/<GameController>
		[HttpPost]
		public async Task PostGameAsync([FromBody] MqttMessageDTO mqttMessage)
		{
			await _gameService.MessageHandling(mqttMessage);
		}
		[HttpGet]
		public async Task<List<Game>> GetAllGamesAsync()
		{
			return await _storageService.GetGames();
		}
		[HttpGet("{id}")]
		public async Task<Game?> GetGameByIdAsync(int id)
		{
			return await _storageService.GetGameById(id);
		}
	}
}
