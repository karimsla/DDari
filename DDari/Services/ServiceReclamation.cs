using DDari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DDari.Services
{
    public class ServiceReclamation
    {
        static HttpClient client = null;
        public ServiceReclamation()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<Uri> Create(Reclamation reclamation, long userId)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"/reclamation/add/{userId}", reclamation);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;

        }
        public async Task<HttpStatusCode> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"/reclamation/delete/{id}");
            return response.StatusCode;

        }
        public async Task<bool> treat(int id, string treatement)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
               $"/reclamation/treat/{id}", treatement);
            response.EnsureSuccessStatusCode();

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;

            }
            else
            {
                return false;
            }
     
        }
        public async Task<Reclamation> getOne(int id)
        {
            Reclamation reclamation = null;
            HttpResponseMessage response = await client.GetAsync($"/reclamation/{id}");
            if (response.IsSuccessStatusCode)
            {
                reclamation = await response.Content.ReadAsAsync<Reclamation>();
            }
            return reclamation;
        }

        public async Task<dynamic> FindAll()
        {
           dynamic reclamations=null ;
            HttpResponseMessage response = await client.GetAsync($"/reclamation/all");
            if (response.IsSuccessStatusCode)
            {
                reclamations =  response.Content.ReadAsAsync<IEnumerable<Reclamation>>().Result;
            }
            return reclamations;
             
        }
        public async Task<List<Reclamation>> findNotTreated()
        {
            IEnumerable<Reclamation> reclamations = null;
            HttpResponseMessage response = await client.GetAsync($"/reclamation/notTreated");
            if (response.IsSuccessStatusCode)
            {
                reclamations = await response.Content.ReadAsAsync<IEnumerable<Reclamation>>();
            }
            return reclamations.ToList();
        }


        public async Task<List<Reclamation>> findMyReclam(int id)
        {
            IEnumerable<Reclamation> reclamations = null;
            HttpResponseMessage response = await client.GetAsync($"/reclamation/myReclam/{id}");
            if (response.IsSuccessStatusCode)
            {
                reclamations = await response.Content.ReadAsAsync<IEnumerable<Reclamation>>();
            }
            return reclamations.ToList();
        }


        public async Task<List<Reclamation>> filter(string filter)
        {
            IEnumerable<Reclamation> reclamations = null;
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("/reclamation/search"),
                Method = HttpMethod.Get,
            };
            request.Content = new StringContent(filter, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                reclamations = await response.Content.ReadAsAsync<IEnumerable<Reclamation>>();
            }
            return reclamations.ToList();
        }
        public async Task<List<Reclamation>> findBetweenDate(DateTime start, DateTime end)
        {
            IEnumerable<Reclamation> reclamations = null;
            HttpResponseMessage response = await client.GetAsync($"/reclamation/between?start={start}&end={end}");
            if (response.IsSuccessStatusCode)
            {
                reclamations = await response.Content.ReadAsAsync<IEnumerable<Reclamation>>();
            }
            return reclamations.ToList();
        }
        public async Task<List<Reclamation>> findByType(string type)
        {
            IEnumerable<Reclamation> reclamations = null;
            HttpResponseMessage response = await client.GetAsync($"/reclamation/type?type={type}");
            if (response.IsSuccessStatusCode)
            {
                reclamations = await response.Content.ReadAsAsync<IEnumerable<Reclamation>>();
            }
            return reclamations.ToList();
        }
        public async Task<List<Reclamation>> searchMultiCriteria(string filter, string type, bool mine, DateTime start, DateTime end, bool treated, int id)
        {
            IEnumerable<Reclamation> reclamations = null;
            HttpResponseMessage response = await client.GetAsync($"/reclamation/searchmulti?filter={filter}&type={type}&mine={mine}&id={id}&start={start}&end={end}&treated={treated}");
            if (response.IsSuccessStatusCode)
            {
                reclamations = await response.Content.ReadAsAsync<IEnumerable<Reclamation>>();
            }
            return reclamations.ToList();
        }
    }
}