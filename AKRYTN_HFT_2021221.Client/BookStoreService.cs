using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Client
{
    class BookStoreService
    {
        HttpClient client;

        public BookStoreService(string baseurl)
        {
            Init(baseurl);
        }

        //Connect to server
        private void Init(string baseurl)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
                ("application/json"));
            try
            {
                client.GetAsync("").GetAwaiter().GetResult();
            }
            catch (HttpRequestException)
            {
                throw new ArgumentException("Endpoint is not available!");
            }

        }

        //------------------------------------------------------------
        //CRUD METHODS:
        //------------------------------------------------------------

        //Get all from collection
        public List<T> Get<T>(string endpoint)
        {
            List<T> items = new List<T>();
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                items = response.Content.ReadAsAsync<List<T>>().GetAwaiter().GetResult();
            }
            return items;
        }

        //Get all from collection into one collection
        public T GetSingle<T>(string endpoint)
        {
            T item = default(T);
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            return item;
        }

        //Get one by ID
        public T Get<T>(int id, string endpoint)
        {
            T item = default(T);
            HttpResponseMessage response = client.GetAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            return item;
        }

        //Add new
        public void Post<T>(T item, string endpoint)
        {
            HttpResponseMessage response =
                client.PostAsJsonAsync(endpoint, item).GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();
        }

        //Delete by ID
        public void Delete(int id, string endpoint)
        {
            HttpResponseMessage response =
                client.DeleteAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();
        }

        //Update data
        public void Put<T>(T item, string endpoint)
        {
            HttpResponseMessage response =
                client.PutAsJsonAsync(endpoint, item).GetAwaiter().GetResult();


            response.EnsureSuccessStatusCode();
        }


        //------------------------------------------------------------
        //NON-CRUD:
        //------------------------------------------------------------

            //PublisherLogic methods:

        //Get the books that were released by the given publisher
        public IEnumerable<Book> GetPublisherBooks(int id)
        {
            IEnumerable<Book> item = default(IEnumerable<Book>);
            HttpResponseMessage response = client.GetAsync("publisher" + "/" + id.ToString() + "/books").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<IEnumerable<Book>>().GetAwaiter().GetResult();
            }
            return item;
        }

            //BookLogic methods:

        //Get Publisher
        public Publisher BookGetPublisher(int id)
        {
            Publisher item = default(Publisher);
            HttpResponseMessage response = client.GetAsync("book" + "/" + id.ToString() + "/publisher").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<Publisher>().GetAwaiter().GetResult();
            }
            return item;
        }

            //CartLogic methods:

        //Get cartItems that belong to this cart
        public IEnumerable<CartItem> GetCartItemsInThisCart(int id)
        {
            IEnumerable<CartItem> item = default(IEnumerable<CartItem>);
            HttpResponseMessage response = client.GetAsync("cart" + "/" + id.ToString() + "/Cartitems").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<IEnumerable<CartItem>>().GetAwaiter().GetResult();
            }
            return item;
        }

        //Get the amount of money that is needed to pay for the cart content
        public double GetCartPrice(int id)
        {
            double item = default(double);
            HttpResponseMessage response = client.GetAsync("cart" + "/" + id.ToString() + "/Price").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<double>().GetAwaiter().GetResult();
            }
            return item;
        }

        //Get books that belong to this cart
        public IEnumerable<Book> GetBooksInThisCart(int id)
        {
            IEnumerable<Book> item = default(IEnumerable<Book>);
            HttpResponseMessage response = client.GetAsync("cart" + "/" + id.ToString() + "/Books").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<IEnumerable<Book>>().GetAwaiter().GetResult();
            }
            return item;
        }

            //UserLogic methods:

        //Get the cart of the user
        public Cart GetUserCart(int id)
        {
            Cart item = default(Cart);
            HttpResponseMessage response = client.GetAsync("user" + "/" + id.ToString() + "/cart").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<Cart>().GetAwaiter().GetResult();
            }
            return item;
        }

        //Get user's cart items
        public IEnumerable<CartItem> GetUserCartItems(int id)
        {
            IEnumerable<CartItem> item = default(IEnumerable<CartItem>);
            HttpResponseMessage response = client.GetAsync("user" + "/" + id.ToString() + "/cart/cartItems").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<IEnumerable<CartItem>>().GetAwaiter().GetResult();
            }
            return item;
        }

        //Get users with book older than x year
        public IEnumerable<User> UserWithBookOlderThanXyear(int year)
        {
            IEnumerable<User> item = default(IEnumerable<User>);
            HttpResponseMessage response = client.GetAsync("user" + "/" + "olderthan/" + year.ToString() + "year").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<IEnumerable<User>>().GetAwaiter().GetResult();
            }
            return item;
        }

    }
}
