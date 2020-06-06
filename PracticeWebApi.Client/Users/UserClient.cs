using Newtonsoft.Json;
using PracticeWebApi.CommonClasses.Users;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWebApi.Client.Users
{
    public class UserClient : IUserClient
    {
        private static readonly string _baseUrl = "https://localhost:10000";

        public Task DeleteUser(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> FindUserById(string id)
        {
            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/{id}");

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(getRequest);
                var stringResponse = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(stringResponse);

                return user;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/user");
            
            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(getRequest);
                var stringResponse = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<IEnumerable<User>>(stringResponse);

                return users;
            }
        }

        public async Task RegisterNewUser(User user)
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}");
            var stringRequest = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            postRequest.Content = stringRequest;

            using (var client = new HttpClient())
            {
                await client.SendAsync(postRequest);
            }
        }

        public Task UpdateUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
