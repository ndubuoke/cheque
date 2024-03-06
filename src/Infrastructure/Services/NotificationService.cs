
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ChequeMicroservice.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChequeMicroservice.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendNotification(string deviceID, string message)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string firebaseOptionsServerId = _configuration["Firebase:ServerKey"];
                    string firebaseOptionsSenderId = _configuration["Firebase:SenderId"];

                    client.BaseAddress = new Uri("https://fcm.googleapis.com");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                        $"key={firebaseOptionsServerId}");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={firebaseOptionsSenderId}");

                    var data = new
                    {
                        to = deviceID,
                        notification = new
                        {
                            body = message,
                            title = "Onyx",
                        },
                        priority = "high"
                    };

                    string json = JsonConvert.SerializeObject(data);
                    StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage result = await client.PostAsync("/fcm/send", httpContent);
                    string resp = await result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
                //Logger.LogInfoAsnyc(ex?.Message ?? ex.InnerException?.Message, "Notification", "Onyx");
            }

        }
    }
}
