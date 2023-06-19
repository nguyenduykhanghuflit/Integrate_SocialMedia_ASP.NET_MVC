using Newtonsoft.Json;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;

namespace SocialMedia.Utils
{
    public class Request
    {
        /*        public async Task<object> Get<T, V>(string url)
                {
                    HttpClient client = new HttpClient();

                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<T>(res);
                        return data;
                    }
                    else
                    {
                        //chưa ném ra lỗi cụ thể
                        var res = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<V>(res);
                        return data;
                    }
                }*/
    }
}