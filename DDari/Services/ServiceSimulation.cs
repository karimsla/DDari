using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace DDari.Services
{
    public class ServiceSimulation
    {


        static HttpClient client = null;

        public ServiceSimulation()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<string> mensualite(double montantCredit, float taux_period, long duree)
        {
            double message = 0;
            HttpResponseMessage response = await client.GetAsync($"/Simulation/mensualiteSA/{montantCredit}/{taux_period}/{duree}");

            if (response.IsSuccessStatusCode)
            {
                message = await response.Content.ReadAsAsync<double>();
            }
            return message + "";

        }

        public async Task<string> total(double montantCredit, long duree, float interet, double assurance, double frais)
        {
            double message = 0;
            HttpResponseMessage response = await client.GetAsync($"/Simulation/Total/{montantCredit}/{duree}/{interet}/{assurance}/{frais}");

            if (response.IsSuccessStatusCode)
            {
                message = await response.Content.ReadAsAsync<double>();
            }
            return message + "";

        }




    }
}
