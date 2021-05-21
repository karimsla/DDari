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
    public class ServiceDetailPanier
    {
        static HttpClient client = null;
        public ServiceDetailPanier()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<Uri> addToBasket(int id)
        {

            int idc = 8;
                HttpResponseMessage response = await client.PostAsJsonAsync(
                     $"/pan/addfurp/{id}/{idc}","");
                response.EnsureSuccessStatusCode();

                // return URI of the created resource.
                return response.Headers.Location;

            }


        public async Task<List<Furniture>> getBasket()
        {
            int idc = 8;
            IEnumerable<Furniture> furnitures = null;
            HttpResponseMessage response = await client.GetAsync($"/pan/getPanier/{idc}");
            if (response.IsSuccessStatusCode)
            {
                furnitures = await response.Content.ReadAsAsync<IEnumerable<Furniture>>();
            }
            return furnitures.ToList();
        }


        public async Task<HttpStatusCode> Delete(int id)
        {
            int idc = 8;
            HttpResponseMessage response = await client.DeleteAsync($"/pan/fursupp/{idc}/{id}");
            return response.StatusCode;

        }










    }
}