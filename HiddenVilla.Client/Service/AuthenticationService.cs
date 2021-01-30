﻿using Blazored.LocalStorage;
using HiddenVilla.Client.Service.IService;
using HiddenVilla.Common;
using HiddenVilla.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVilla.Client.Service
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly HttpClient _client;
		private readonly ILocalStorageService _localStorage;
		private readonly AuthenticationStateProvider _authStateProvider;

		public AuthenticationService(HttpClient client, ILocalStorageService localStorage,
			AuthenticationStateProvider authStateProvider)
		{
			_client = client;
			_localStorage = localStorage;
			_authStateProvider = authStateProvider;
		}
		public async Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication)
		{
			var content = JsonConvert.SerializeObject(userFromAuthentication);
			var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/account/signin", bodyContent);
			var contentTemp = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(contentTemp);

			if(response.IsSuccessStatusCode)
			{
				await _localStorage.SetItemAsync(SD.Local_Token, result.Token);
				await _localStorage.SetItemAsync(SD.Local_UserDetails, result.userDTO);
				((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
				return new AuthenticationResponseDTO { IsAuthSuccessful = true };
			}
			else
			{
				return result;
			}
		}

		public async Task Logout()
		{
			await _localStorage.RemoveItemAsync(SD.Local_Token);
			await _localStorage.RemoveItemAsync(SD.Local_UserDetails);
			((AuthStateProvider)_authStateProvider).NotifyUserLogout();
			_client.DefaultRequestHeaders.Authorization = null;
		}

		public async Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegistration)
		{
			var content = JsonConvert.SerializeObject(userForRegistration);
			var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/account/signup", bodyContent);
			var contentTemp = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<RegistrationResponseDTO>(contentTemp);

			if (response.IsSuccessStatusCode)
			{
				return new RegistrationResponseDTO { IsRegistrationSuccessful = true };
			}
			else
			{
				return result;
			}
		}
	}
}
