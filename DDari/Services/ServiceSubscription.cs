using DDari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace DDari.Services
{
    public class ServiceSubscription
    {
        static HttpClient client = null;

        public ServiceSubscription(){
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<Uri> Create(Models.Subscription subscription)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"/Subscription/addsub", subscription);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;

        }


        public async Task<Uri> CreateSubscribe(Models.Subscribe subscribe)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"/Subscription/addsubscribe", subscribe);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;

        }
        public async Task<System.Net.HttpStatusCode> Delete(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"/Subscription/delete/{id}");
            return response.StatusCode;

        }

        public async Task<Subscription> getOne(int id)
        {
            Subscription sub= null;
            HttpResponseMessage response = await client.GetAsync($"/Subscription/getOne/{id}");
            if (response.IsSuccessStatusCode)
            {
                sub = await response.Content.ReadAsAsync<Subscription>();
            }
            return sub;
        }

        public async Task<dynamic> FindAll()
        {
            dynamic subs = null;
            HttpResponseMessage response = await client.GetAsync($"/Subscription/GetAll");
            if (response.IsSuccessStatusCode)
            {
                subs = response.Content.ReadAsAsync < IEnumerable<Subscription>>().Result;
            }
            return subs;

        }



        public async Task<Subscription> Update(int id , Subscription subscription)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"/Subscription/Modify",subscription);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            subscription = await response.Content.ReadAsAsync<Subscription>();
            return subscription;
        }


    }
}