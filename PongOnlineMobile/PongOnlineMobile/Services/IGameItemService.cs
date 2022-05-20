using PongOnlineMobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PongOnlineMobile.Services
{
	public interface IGameItemService
	{
		Task<Game> GetGameItemByIdAsync(int id);
		Task<IEnumerable<Game>> GetGameItems();
	}
}