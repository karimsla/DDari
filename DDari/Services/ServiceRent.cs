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
    public class ServiceRent
    {
        static HttpClient client = null;
        public ServiceRent()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<List<Rent>> searchMultiCriteria(string filter, string adress, string state, string city, int id)
        {
            IEnumerable<Rent> rents = null;
            HttpResponseMessage response = await client.GetAsync($"/rent/searchmulti?filter={filter}&adress={adress}&state={state}&city={city}&id={id}");
            if (response.IsSuccessStatusCode)
            {
                rents = await response.Content.ReadAsAsync<IEnumerable<Rent>>();
            }
            return rents.ToList();
        }
    }
}