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
    public class ServiceFurniture
    {

        static HttpClient client = null;
        public ServiceFurniture()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<Uri> Create(Furniture fur, int userId)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"/fur/addfur/{userId}", fur);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;

        }


        public async Task<HttpStatusCode> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"/fur/fursupp/{id}");
            return response.StatusCode;

        }
        public async Task<List<Furniture>> search(string filter)
        {
            List<Furniture> furniture = null;
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(client.BaseAddress+"fur/search"),
                Method = HttpMethod.Post,
            };
            request.Content = new StringContent(filter, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                furniture = response.Content.ReadAsAsync<IEnumerable<Furniture>>().Result.ToList();
            }
            return furniture;
            
        }

        public async Task<Furniture> getOne(int id)
        {
            Furniture fur = null;
            HttpResponseMessage response = await client.GetAsync($"/fur/furget/{id}");
            if (response.IsSuccessStatusCode)
            {
                fur = await response.Content.ReadAsAsync<Furniture>();
            }
            return fur;

        }

        public async Task<List<Furniture>> FindAll()
        {
            dynamic furnitures = null;
            HttpResponseMessage response = await client.GetAsync($"/fur/furniturs");
            if (response.IsSuccessStatusCode)
            {
                furnitures = response.Content.ReadAsAsync<IEnumerable<Furniture>>().Result;
            }
            return furnitures;

        }

        internal Task<Uri> Create(object Furniture, int v)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Furniture>> findByType(string type)
        {
            IEnumerable<Furniture> fur = null;
            HttpResponseMessage response = await client.GetAsync($"/fur/type?type={type}");
            if (response.IsSuccessStatusCode)
            {
                fur = await response.Content.ReadAsAsync<IEnumerable<Furniture>>();
            }
            return fur.ToList();
        }

        public async Task<bool> Update( Furniture fur)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
               $"/fur/furmodif", fur);
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




        public async Task<List<Furniture>> getMine(int id)
        {
            List<Furniture> furnitures = null;
            HttpResponseMessage response = await client.GetAsync($"/fur/myfurn/{id}");
            if (response.IsSuccessStatusCode)
            {
                furnitures =await response.Content.ReadAsAsync<List<Furniture>>();
            }
            return furnitures;

        }



    }












}
