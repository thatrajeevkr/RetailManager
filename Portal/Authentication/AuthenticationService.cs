using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Portal.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly IConfiguration _config;
        private string authTokenStorageKey;

        public AuthenticationService(ILocalStorageService localStorage, HttpClient client, AuthenticationStateProvider authStateProvider, IConfiguration config)
        {
            _localStorage = localStorage;
            _client = client;
            _authStateProvider = authStateProvider;
            _config = config;
            authTokenStorageKey = _config["authTokenStorageKey"];
        }

        public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", userForAuthentication.Email),
                new KeyValuePair<string, string>("password", userForAuthentication.Password)
            });
            string api = _config["api"] + _config["tokenEndpoint"];
            var authResult = await _client.PostAsync(api, data);
            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }

            var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(
                authContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            await _localStorage.SetItemAsync(authTokenStorageKey, result.Access_Token);

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Access_Token);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Access_Token);

            return result;
        }

        public async Task LogOut()
        {
            await _localStorage.RemoveItemAsync(authTokenStorageKey);
            ((AuthStateProvider)_authStateProvider).NotifyUserLogOut();
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
