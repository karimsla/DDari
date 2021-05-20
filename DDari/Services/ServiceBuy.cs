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
    public class ServiceBuy
    {
        static HttpClient client = null;
        public ServiceBuy()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<List<Buy>> searchMultiCriteria(string filter,string adress,string  state, string city, int id)
        {
            IEnumerable<Buy> buys = null;
            HttpResponseMessage response = await client.GetAsync($"/buy/searchmulti?filter={filter}&adress={adress}&state={state}&city={city}&id={id}");
            if (response.IsSuccessStatusCode)
            {
                buys = await response.Content.ReadAsAsync<IEnumerable<Buy>>();
            }
            return buys.ToList();
        }
    }
}