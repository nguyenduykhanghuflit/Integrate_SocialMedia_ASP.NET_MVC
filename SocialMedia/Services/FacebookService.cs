using Newtonsoft.Json;
using SocialMedia.Models;
using SocialMedia.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SocialMedia.Services
{
    public class FacebookService
    {
        private readonly string AccessToken;
        private static readonly string fbUrl = "https://graph.facebook.com/v17.0/";
        public FacebookService(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public async Task<object> GetPageInfo()
        {
            string url = fbUrl + "me/accounts?access_token=" + AccessToken;

            FacebookRequest facebookRequest = new FacebookRequest();
            object data = await facebookRequest.Get<PageInfo>(url);
            return data;
        }

    }
}