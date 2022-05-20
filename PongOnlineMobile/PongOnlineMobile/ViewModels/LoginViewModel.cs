using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using PongOnlineMobile.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PongOnlineMobile.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public Command LoginCommand { get; }
		OidcClient _client;
		LoginResult _result;

		//TODO: ioc or depencyinjection
		Lazy<HttpClient> _apiClient = new Lazy<HttpClient>(() => new HttpClient());

		public LoginViewModel()
		{
			LoginCommand = new Command(OnLoginClicked);

			var browser = DependencyService.Get<IBrowser>();

			var options = new OidcClientOptions
			{
				Authority = AuthConfig.Domain,
				ClientId = AuthConfig.ClientId,
				Scope = AuthConfig.Scopes,
				RedirectUri = $"{AuthConfig.PackageName}://{AuthConfig.Domain}/android/{AuthConfig.PackageName}/callback",
				Browser = browser
			};

			_client = new OidcClient(options);
			_apiClient.Value.BaseAddress = new Uri(AuthConfig.WebApi);
		}

		private async void OnLoginClicked(object obj)
		{
			// Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

			var audience = new Dictionary<string, string>
			{
				{ "audience", AuthConfig.Audience }
			};

			var loginRequest = new LoginRequest() { FrontChannelExtraParameters = new Parameters(audience) };

			try
			{
				_result = await _client.LoginAsync(loginRequest);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				throw;
			}

			if (_result.IsError)
			{
				//Messagecenter
				Debug.WriteLine(_result.Error);
				return;
			}
			else
			{
				await SecureStorage.SetAsync("accessToken", _result.AccessToken);

				IsLoggedIn = true;
			}
			await Shell.Current.GoToAsync("///GameItemPage");
		}
		Command logoutCommand;
		public ICommand LogoutCommand => logoutCommand ??= new Command(async () =>
		{
			SecureStorage.Remove("accessToken");
			IsLoggedIn = false;
			System.Console.WriteLine("Du er nu logget ud og AccessToken er slettet!");
			await Shell.Current.GoToAsync($"//AboutPage");
		});
	}
}
