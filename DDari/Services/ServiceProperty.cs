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
    public class ServiceProperty
    {
      
            static HttpClient client = null;
            public ServiceProperty()
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8081");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            }



            public async Task<Uri> Create(Property prop, long userId)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(
                     $"/prop/addprop/{userId}", prop);
                response.EnsureSuccessStatusCode();

                // return URI of the created resource.
                return response.Headers.Location;

            }


            public async Task<HttpStatusCode> Delete(int id)
            {
                HttpResponseMessage response = await client.DeleteAsync($"/prop/propsupp/{id}");
                return response.StatusCode;

            }


            public async Task<Property> getOne(int id)
            {
                Property prop = null;
                HttpResponseMessage response = await client.GetAsync($"/prop/propget/{id}");
                if (response.IsSuccessStatusCode)
                {
                    prop = await response.Content.ReadAsAsync<Property>();
                }
                return prop;

            }

      


        public async Task<List<Property>> FindAll()
            {
                List<Property> properties = null;
                HttpResponseMessage response = await client.GetAsync($"/prop/properties");
                if (response.IsSuccessStatusCode)
                {
                    properties = response.Content.ReadAsAsync<IEnumerable<Property>>().Result.ToList();
                }
                return properties;

            }
           public async Task<List<Property>> search(string filter)
            {
                List<Property> properties = null;
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(client.BaseAddress + "prop/search"),
                Method = HttpMethod.Post,
            };
          
            request.Content = new StringContent(filter, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                {
                    properties = response.Content.ReadAsAsync<IEnumerable<Property>>().Result.ToList();
                }
                return properties;
          

        }

         

            public async Task<List<Property>> findByType(string type)
            {
                IEnumerable<Property> prop = null;
                HttpResponseMessage response = await client.GetAsync($"/prop/type?type={type}");
                if (response.IsSuccessStatusCode)
                {
                    prop = await response.Content.ReadAsAsync<IEnumerable<Property>>();
                }
                return prop.ToList();
            }

            public async Task<bool> Update( Property prop)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(
                   $"/prop/propmodif/", prop);
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
        public async Task<List<Property>> getPropsUser(int id)
        {
            List<Property> prop = null;
            HttpResponseMessage response = await client.GetAsync($"/prop/userprops/{id}");
            if (response.IsSuccessStatusCode)
            {
                prop = await response.Content.ReadAsAsync<List<Property>>();
            }
            return prop;

        }





    }
    }
