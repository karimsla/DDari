using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DDari.Models;
using System.Net;

namespace DDari.Services
{
    public class ServiceDelivery
    {
        static HttpClient client = null;

        public ServiceDelivery()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> AddDmAsync(DeliveryMan dm)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"/deliveries/createdm", dm);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.StatusCode==HttpStatusCode.OK;
        }

        public async Task<dynamic> FindAllDm()
        {
            List<DeliveryMan> dms = null;
            HttpResponseMessage response = await client.GetAsync($"/deliveries/listdm");
            if (response.IsSuccessStatusCode)
            {
                dms = response.Content.ReadAsAsync<IEnumerable<DeliveryMan>>().Result.ToList();
            }
            return dms;

        }
    }
}