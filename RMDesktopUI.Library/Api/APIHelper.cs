using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Configuration;
using RMDesktopUI.Models;

namespace RMDesktopUI.Library.Api
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient apiClient { get; set; }
        private ILoggedInUserModel loggedInUser;

        public APIHelper(ILoggedInUserModel _loggedInUser)
        {
            loggedInUser = _loggedInUser;
            InitializeClient();
        }

        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(api);
            apiClient.DefaultRequestHeaders.Accept.Clear(); // clear the request header
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // we're looking for json data back
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            using (HttpResponseMessage response = await apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task GetLoggedInUserInfo(string token)
        {
            // clear the request header
            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            // we're looking for json data back
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));
            // We put the authorization token in the request header
            apiClient.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");

            using (HttpResponseMessage response = await apiClient.GetAsync("/api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                    loggedInUser.CreatedDate = result.CreatedDate;
                    loggedInUser.EmailAddress = result.EmailAddress;
                    loggedInUser.FirstName = result.FirstName;
                    loggedInUser.LastName = result.LastName;
                    loggedInUser.Id = result.Id;
                    loggedInUser.Token = token;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
