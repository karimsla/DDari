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
using Cloudmersive.APIClient.NET.NLP.Api;
using Cloudmersive.APIClient.NET.NLP.Client;
using Cloudmersive.APIClient.NET.NLP.Model;
using System.Net.Mail;
using System.Text;
using RestSharp;

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
            if (isSpam(message) || profanityDetection(message))
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
        public async Task<List<Utilisateur>> getUsers(int id)
        {
            IEnumerable<Utilisateur> users = null;
            HttpResponseMessage response = await client.GetAsync($"/message/getUsers/{id}");
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadAsAsync<IEnumerable<Utilisateur>>();
            }
            return users.ToList();
        }


        public bool isSpam(string text)
        {
            // Create MLContext
            MLContext mlContext = new MLContext();
            //Define DataViewSchema for data preparation pipeline and trained model
            DataViewSchema modelSchema;
    
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Content", "model.zip");

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

        public bool profanityDetection(string text)
        {
            // Configure API key authorization: Apikey
            Configuration.Default.AddApiKey("Apikey", "128ffe67-a3e7-4ec4-b723-980fa639eaeb");

            var apiInstance = new AnalyticsApi();
      
            // Detect language of text
           
                var input = new ProfanityAnalysisRequest(text);
            try
            {
               
                ProfanityAnalysisResponse result = apiInstance.AnalyticsProfanity(input);
                //if the score is over 0.6 that means it s profanity other wise is not if it s less than 0.6 the return result is false
                return result.ProfanityScoreResult>0.6;
            }
            catch (Exception e)
            {
              
            }
            //false no profanity detected
            return false;
        }


        public String sendmail(string mails, string obj, string body)
        {
            try
            {
                string sendermail = System.Configuration.ConfigurationManager.AppSettings["SenderMail"].ToString();
                string senderpassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 1000000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                string from = sendermail;

                MailMessage mailMessage = new MailMessage(from, mails);



                mailMessage.Subject = obj;
                mailMessage.Body = body;

                client.Credentials = new NetworkCredential(sendermail, senderpassword);

                mailMessage.IsBodyHtml = true;

                mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                client.Send(mailMessage);

            }
            catch (Exception e)
            {

                return "error occured";

            }
            return "success";
        }

        public void sendSMS(string body, string phone)
        {


            try
            {




                var client = new RestClient("https://rest.nexmo.com/sms/json?api_key=e5668206&api_secret=P4VhyGeOyadg8AVr&from=Dari&to=216" + phone + "&text=" + Uri.EscapeUriString(body) + "");
                var request = new RestRequest();


                request.Method = Method.POST;
                request.AddHeader("content-type", "application/x-www-form-urlencoded");

                IRestResponse response = client.Execute(request);
                Console.WriteLine(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }





        }

    }
}