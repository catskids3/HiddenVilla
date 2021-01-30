using Blazored.LocalStorage;
using HiddenVilla.Client.Helper;
using HiddenVilla.Common;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiddenVilla.Client.Service
{
	public class AuthStateProvider : AuthenticationStateProvider
	{
		private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;

		public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
		{
			_httpClient = httpClient;
			_localStorage = localStorage;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var token = await _localStorage.GetItemAsync<string>(SD.Local_Token);

			if(token == null)
			{
				return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

				// fake return of authenticated user
				//return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[] {
				//	new Claim(ClaimTypes.Name, "ben@gmail.com")
				//}, "jwtAuthType")));
			}

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
			return new AuthenticationState(
				new ClaimsPrincipal(
				new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
		}

		public void NotifyUserLoggedIn(string token)
		{
			var authenticatedUser = 
				new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
			var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
			NotifyAuthenticationStateChanged(authState);
		}

		public void NotifyUserLogout()
		{
			var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
			NotifyAuthenticationStateChanged(authState);
		}
	}
}
