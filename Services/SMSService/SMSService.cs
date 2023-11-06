using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace mYSelfERPWeb.Services
{
    public class SMSService :ISMSService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "C20063365ee9c224005c37.89118261";

        public SMSService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<string> SendSmsAsync(string contacts, string message)
        {
            var url = "https://bangladeshsms.com/smsapi";
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"api_key", ApiKey},
                {"type", "text"},
                {"contacts", contacts},
                {"senderid", "HPLMIS"},
                {"msg", message}
            });

            var response = await _httpClient.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}