using DDari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public bool treat(int id, string treatement)
        {

            return true;
        }
        public Reclamation getOne(int id)
        {
            return new Reclamation();
        }
        public List<Reclamation> findAll()
        {
            return null;
        }
        public List<Reclamation> findNotTreated()
        {
            return null;
        }
        public List<Reclamation> findMyReclam(int id)
        {
            return null;
        }
        public List<Reclamation> filter(string filter)
        {
            return null;
        }
        public List<Reclamation> findBetweenDate(DateTime start, DateTime end)
        {
            return null;
        }
        public List<Reclamation> findByType(string type)
        {
            return null;
        }
        public List<Reclamation> searchMultiCriteria(string filter, string type, bool mine, DateTime start, DateTime end, bool treated, int id)
        {
            return null;
        }
    }
}