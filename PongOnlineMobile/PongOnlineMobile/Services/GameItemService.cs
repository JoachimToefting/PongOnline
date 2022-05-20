using MonkeyCache.SQLite;
using Polly;
using PongOnlineMobile.Models;
using PongOnlineMobile.Repository;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PongOnlineMobile.Services
{
	public class GameItemService : IGameItemService
	{
		private readonly IGenericRepository _genericRepository;
		public GameItemService(IGenericRepository genericRepository)
		{
			_genericRepository = genericRepository;
		}

		public async Task<IEnumerable<Game>> GetGameItems()
		{
			UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
			{
				Path = ApiConstants.ItemsEndpoint
			};

			string url = builder.Path;

			if (Connectivity.NetworkAccess == NetworkAccess.None)
			{
				return Barrel.Current.Get<IEnumerable<Game>>(url);
			}
			if (!Barrel.Current.IsExpired(url))
			{
				return Barrel.Current.Get<IEnumerable<Game>>(url);
			}

			var todoItems = await Policy
				.Handle<ApiException>(ex => ex.StatusCode != HttpStatusCode.NotFound)
				.WaitAndRetryAsync(retryCount: 3, sleepDurationProvider:
				retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
				onRetry: (ex, time) =>
				{
					Console.WriteLine($"Something went wrong: {ex.Message},retrying...");
				})
				.ExecuteAsync(async () =>
				{
					Console.WriteLine($"Trying to fetch remote data...");
					return await _genericRepository.GetAsync<IEnumerable<Game>>(builder.ToString(), await SecureStorage.GetAsync("accessToken"));
				});

			Barrel.Current.Add(url, todoItems, TimeSpan.FromSeconds(20));
			return todoItems;

			//return await _genericRepository.GetAsync<IEnumerable<Game>>(builder.ToString(), await SecureStorage.GetAsync("accessToken"));
		}
		public async Task<Game> GetGameItemByIdAsync(int id)
		{
			UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
			{
				Path = $"{ApiConstants.ItemsEndpoint}/{id}"
			};
			return await _genericRepository.GetAsync<Game>(builder.ToString(), await SecureStorage.GetAsync("accessToken"));
		}
	}
}
