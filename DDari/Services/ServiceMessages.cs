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
    public class ServiceMessages
    {
        static HttpClient client = null;
        public ServiceMessages()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }


        public async Task<bool> AddMessageAsync(string message, long by, long to)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"/message/add?by={by}&to={to}&message={message}",new Message());

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
        public async Task<List<Message>> GetMessage(int sentBy, int sentTo)
        {
            IEnumerable<Message> messages = null;
            HttpResponseMessage response = await client.GetAsync($"/message/getmessages?by={sentBy}&to={sentTo}");
            if (response.IsSuccessStatusCode)
            {
                messages = await response.Content.ReadAsAsync<IEnumerable<Message>>();
            }
            return messages.ToList();
        }

        public async Task<string> chatBot(string input)
        {
            string message = "";
            HttpResponseMessage response = await client.GetAsync("/message/chatbot");

            if (response.IsSuccessStatusCode)
            {
                message = await response.Content.ReadAsAsync<string>();
            }
            return message;

        }
       // public List<Utilisateur> getUsers(int id);
    }
}