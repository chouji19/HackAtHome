using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HackAtHome.Entities;
using Newtonsoft.Json;
using Org.Apache.Http.Client.Params;


namespace HAckAtHome.SAL
{
    public class AuthenticateService
    {
        public async Task<ResultInfo> AutenticateAsync(string studentEmail,string studentPassword)
        {
            ResultInfo Restult = null;
            string WebAppiBaseAddress = "https://ticapacitacion.com/hackathome/";
            string EventId = "Xamarin30";
            string RequestUri = "api/evidence/Authenticate";

            UserInfo User = new UserInfo
            {
                Email = studentEmail,
                Password =  studentPassword,
                EventID = EventId
            };
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(WebAppiBaseAddress);
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var JSONUserInfo = JsonConvert.SerializeObject(User);
                    HttpResponseMessage Response =
                        await
                            Client.PostAsync(RequestUri,
                                new StringContent(JSONUserInfo, Encoding.UTF8, 
                                "application/json"));
                    var ResultWebApi = await Response.Content.ReadAsStringAsync();
                    Restult = JsonConvert.DeserializeObject<ResultInfo>(ResultWebApi);
                }
                catch (Exception)
                {
                    return new ResultInfo() {Status = Status.Error};
                }
                return Restult;

            }
        }
    }
}