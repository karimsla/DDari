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

        public async Task<List<DeliveryMan>> FindAllDm()
        {
            List<DeliveryMan> dms = null;
            HttpResponseMessage response = await client.GetAsync($"/deliveries/listdm");
            if (response.IsSuccessStatusCode)
            {
                dms = response.Content.ReadAsAsync<IEnumerable<DeliveryMan>>().Result.ToList();
            }
            return dms;

        }

        public async Task<bool> AddDeliveryAsync(Delivery delivery)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"/deliveries/add", delivery);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.StatusCode == HttpStatusCode.OK;
        }
        public async Task<bool> UpdateDSAsync(int id, int state)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"/Appointments/deliverystate/{id}?deliveryState={state}", "");

            response.EnsureSuccessStatusCode();


            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        public async Task<List<Delivery>> ListDeliveryPerDm(int id)
        {
            List<Delivery> dms = null;
            HttpResponseMessage response = await client.GetAsync($"/deliveries/listperdm/{id}");
            if (response.IsSuccessStatusCode)
            {
                dms = response.Content.ReadAsAsync<IEnumerable<Delivery>>().Result.ToList();
            }
            return dms;

        }
        public async Task<List<Delivery>> ListDeliveries()
        {
            List<Delivery> dms = null;
            HttpResponseMessage response = await client.GetAsync($"/deliveries/");
            if (response.IsSuccessStatusCode)
            {
                dms = response.Content.ReadAsAsync<IEnumerable<Delivery>>().Result.ToList();
            }
            return dms;

        }

        public async Task<bool> cancelDelivery(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"/deliveries/cancel/{id}");
            if (response.IsSuccessStatusCode)
            {
             return true;
            }
            return false;

        }
        public async Task<bool> deleteDM(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"/deliveries/deletedm/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }
        public async Task<bool> LocateAsync(int id, double latitude, string longitude)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"/deliveries/locate/{id}?latitude={latitude}&longitude={longitude}", "");

            response.EnsureSuccessStatusCode();


            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;

            }
            else
            {
                return false;
            }
        }


    }
}