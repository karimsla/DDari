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
    public class ServiceAppointment
    {
        static HttpClient client = null;

        public ServiceAppointment()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> RequestAppAsync(Appointment appointment)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"/Appointments/add", appointment);

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
        public async Task<bool> AcceptAppAsync(int id,string date,string address,int at)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                 $"/Appointments/accept/{id}?date={date}&address={address}&apptype={at}","");

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
        public async Task<List<Appointment>> ownerAppAsync(int id)
        {
            List<Appointment> applist = null;
            HttpResponseMessage response = await client.GetAsync(
                 $"/Appointments/listAppOwner/{id}");
            if (response.IsSuccessStatusCode)
            {
                applist = response.Content.ReadAsAsync<IEnumerable<Appointment>>().Result.ToList();
            }
            return applist;
        }
        public async Task<List<Appointment>> CustAppAsync(int id)
        {
            List<Appointment> applist = null;
            HttpResponseMessage response = await client.GetAsync(
                 $"/Appointments/listAppCustomer/{id}");
            if (response.IsSuccessStatusCode)
            {
                applist = response.Content.ReadAsAsync<IEnumerable<Appointment>>().Result.ToList();
            }
            return applist;
        }
        public async Task<bool> cancelApp(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"/Appointments/cancelApp/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }
        public async Task<Customer> getCustAsync(int id)
        {
            Customer applist = null;
            HttpResponseMessage response = await client.GetAsync(
                 $"/UserCrud/getUserById/{id}");
            if (response.IsSuccessStatusCode)
            {
                applist = response.Content.ReadAsAsync<Customer>().Result;
            }
            return applist;
        }


    }
}