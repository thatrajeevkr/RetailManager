using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using TRMDesktopUI.Library.API;

namespace Portal.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;
        private readonly IConfiguration _config;
        private readonly IAPIHelper _aPIHelper;

        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage, IConfiguration config, IAPIHelper aPIHelper)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            _config = config;
            _aPIHelper = aPIHelper;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string authTokenStorageKey = _config["authTokenStorageKey"];
            var token = await _localStorage.GetItemAsync<string>(authTokenStorageKey);

            if (string.IsNullOrWhiteSpace(token))
            {
                return _anonymous;
            }
            bool isAuthenticated = !await NotifyUserAuthentication(token);

            if (isAuthenticated == false)
            {
                return _anonymous;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token),
                    "jwtAuthType")));

        }

        public async Task<bool> NotifyUserAuthentication(string token)
        {
            bool isAuthenticatedOutput;
            try
            {
                await _aPIHelper.GetLoggedInUserInfo(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Task<AuthenticationState> authState;
            try
            {
                var authenticatedUser = new ClaimsPrincipal(
                                            new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token),
                                            "jwtAuthType"));
                authState = Task.FromResult(new AuthenticationState(authenticatedUser));
                NotifyAuthenticationStateChanged(authState);
                isAuthenticatedOutput = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //authState = Task.FromResult(_anonymous);
                await NotifyUserLogOut();
                isAuthenticatedOutput = false;
            }
            return isAuthenticatedOutput;
        }

        public async Task NotifyUserLogOut()
        {
            var authState = Task.FromResult(_anonymous);
            string authTokenStorageKey = _config["authTokenStorageKey"];
            await _localStorage.RemoveItemAsync(authTokenStorageKey);
            _aPIHelper.LogOfUser();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
