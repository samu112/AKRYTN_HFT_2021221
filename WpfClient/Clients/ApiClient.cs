using AKRYTN_HFT_2021221.Models;
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


        //------------------------------------------------------------
        //CRUD METHODS:
        //------------------------------------------------------------

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

        public async Task PostAsync<T>(T item, string endpoint)
        {
            var content =
                new StringContent(JsonConvert.SerializeObject(item),
                Encoding.UTF8,
                "application/json");

            var response = await HttpClient.PostAsync(endpoint, content);

            response.EnsureSuccessStatusCode();
        }

        //------------------------------------------------------------
        //NON-CRUD:
        //------------------------------------------------------------

            //PublisherLogic methods:

        //Get the books that were released by the given publisher
        public List<Book> GetPublisherBooks(string endpoint, int id)
        {
            List<Book> items = new List<Book>();
            string url = endpoint + "publisher" + "/" + id.ToString() + "/books";
            var response = HttpClient.GetAsync(url).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                var entities = JsonConvert.DeserializeObject<List<Book>>(responseString);

                return entities;
            }

            throw new Exception("The request was not successfull");
        }

            //BookLogic methods:

        //Get Publisher
        public Publisher BookGetPublisher(string endpoint, int id)
        {
            Publisher item = default(Publisher);
            var response = HttpClient.GetAsync(endpoint + "/" + id.ToString() + "/publisher").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                item = JsonConvert.DeserializeObject<Publisher>(responseString);

                return item;
            }

            throw new Exception("The request was not successfull");
        }

            //CartLogic methods:

        //Get the amount of money that is needed to pay for the cart content
        public double GetCartPrice(string endpoint, int id)
        {
            double item = default(double);
            var response = HttpClient.GetAsync(endpoint + "/" + id.ToString() + "/Price").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                item = JsonConvert.DeserializeObject<double>(responseString);

                return item;
            }

            throw new Exception("The request was not successfull");
        }


    }
}
