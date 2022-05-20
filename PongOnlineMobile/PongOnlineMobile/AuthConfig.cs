using System;
using System.Collections.Generic;
using System.Text;

namespace PongOnlineMobile
{
	public static class AuthConfig
	{
		public const string Domain = "https://dev-n3qh9yho.us.auth0.com";    // Her indsættes Domain Name fra Auth0
		public const string ClientId = "DYWGljHrcSJmtz0wJiRmYFzRJE5fClwG";  // Her indsættes Client ID fra Auth0
		public const string Audience = "https://localhost:5001/game";  // Her indsættes Audience fra Auth0
		public const string Scopes = "openid profile read:Game";
		//public const string RedirectUri = "com.companyname.pongonlinemobile://callback";
		public const string PackageName = "com.companyname.pongonlinemobile";

		public const string WebApi = "https://10.0.2.2:5001";
	}
}
