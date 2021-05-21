using DDari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace DDari.Services
{
    public class ServiceOrders
    {
        static HttpClient client = null;
        public ServiceOrders()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }


        public async Task<Uri> addOrder()
        {
            int idc = 1;
            HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"/orders/commander/{idc}", "");
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;

        }

        public async Task<List<Orders>> FindAll()
        {
            List<Orders> orders = null;
            int idc = 1;
            HttpResponseMessage response = await client.GetAsync($"/orders/allorders/{idc}");
            if (response.IsSuccessStatusCode)
            {
                orders = response.Content.ReadAsAsync<IEnumerable<Orders>>().Result.ToList();
            }
            return orders;

        }

        public async Task<List<Furniture>> FurnOrd(int id)
        {
            List<Furniture> furns = null;
        
            HttpResponseMessage response = await client.GetAsync($"/orders/FurnOrd/{id}");
            if (response.IsSuccessStatusCode)
            {
                furns = response.Content.ReadAsAsync<IEnumerable<Furniture>>().Result.ToList();
            }
            return furns;
        }


        public async Task<Orders> getOne(int id)
        {
            Orders ord = null;
            HttpResponseMessage response = await client.GetAsync($"/orders/ordget/{id}");
            if (response.IsSuccessStatusCode)
            {
                ord = await response.Content.ReadAsAsync<Orders>();
            }
            return ord;

        }



        public async Task<HttpStatusCode> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"/orders/ordsupp/{id}");
            return response.StatusCode;

        }






    }
}