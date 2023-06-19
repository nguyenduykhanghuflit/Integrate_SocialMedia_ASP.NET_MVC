using Newtonsoft.Json;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SocialMedia.Utils
{
    public class FacebookRequest
    {
        public async Task<object> Get<T>(string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(res);
                }
                else
                {
                    var res = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<FacebookError>(res);

                }
            }
            catch (Exception ex)
            {
                return new ErrorResponse(-1, ex.Message);
            }
        }

        public async Task<bool> AccessTokenValid(string input_token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://graph.facebook.com/oauth/access_token?client_id=280910524463703&client_secret=00d69d8a2d25388191c0d84385cf30fe&grant_type=client_credentials");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                var appToken = JsonConvert.DeserializeObject<AppToken>(res);
                var access_token = appToken.access_token;

                var requestToken = new HttpRequestMessage(HttpMethod.Get, $"https://graph.facebook.com/v17.0/debug_token?input_token={input_token}&access_token={access_token}&fields=is_valid");
                var responseToken = await client.SendAsync(requestToken);
                if (responseToken.IsSuccessStatusCode)
                {
                    var res2 = await responseToken.Content.ReadAsStringAsync();
                    var tokenValid = JsonConvert.DeserializeObject<TokenValid>(res2);
                    return tokenValid.data.is_valid;
                }
                else
                {

                    var res2 = await responseToken.Content.ReadAsStringAsync();
                    var tokenValid = JsonConvert.DeserializeObject<TokenValid>(res2);
                    throw new Exception("token invalid");
                }
            }
            else
            {
                throw new Exception("get access_token");
            }

        }
    }
}