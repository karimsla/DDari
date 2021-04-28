using DDari.Models;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (isSpam(message))
            {
                return false;
            }
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
            HttpResponseMessage response = await client.PostAsJsonAsync("/message/chatbot",input);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                message = await response.Content.ReadAsStringAsync();
            }
            return message;

        }
       // public List<Utilisateur> getUsers(int id);


        public bool isSpam(string text)
        {
            // Create MLContext
            MLContext mlContext = new MLContext();
            //Define DataViewSchema for data preparation pipeline and trained model
            DataViewSchema modelSchema;
            string filePath = HttpContext.Current.Server.MapPath("~/Content/model.zip");
            // Load trained model
            var model = mlContext.Model.Load(filePath, out modelSchema);

            var predictor = mlContext.Model.CreatePredictionEngine<SpamInput, SpamPrediction>(model);
            var input = new SpamInput { Message = text };
            var prediction = predictor.Predict(input);
         

            if (prediction.isSpam=="spam")
            {
                return true;
            }
            return false;
        }



    }
}