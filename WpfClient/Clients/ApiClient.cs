using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.Clients
{
    public class ApiClient
    {
        private static HttpClient HttpClient = new HttpClient();

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await HttpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                var entities = JsonConvert.DeserializeObject<T>(responseString);

                return entities;
            }

            throw new Exception("The request was not successfull");
        }

        public async Task DeleteAsync(int id, string endpoint)
        {
            var response = await HttpClient.DeleteAsync($"{endpoint}/{id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task PutAsync<T>(T entityToUpdate, string endpoint)
        {
            var content =
                new StringContent(JsonConvert.SerializeObject(entityToUpdate),
                Encoding.UTF8,
                "application/json");

            var response = await HttpClient.PutAsync(endpoint, content);

            response.EnsureSuccessStatusCode();
        }
    }
}
