using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class FacebookAuthController : Controller
    {
        static readonly HttpClient client = new HttpClient();


        public ActionResult Login()
        {
            return View();
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("LoginFacebookSucces");
                return uriBuilder.Uri;
            }
        }

        public ActionResult Logout()
        {

            Session.Clear();
            return RedirectToAction("Login", "FacebookAuth");

        }
        public ActionResult LoginFacebookSucces(string access_token)
        {
            var fb = new FacebookClient();
            fb.AccessToken = access_token;
            dynamic obj = fb.Get("me?fields=id,name,email,picture");

            Session["AccessToken"] = access_token;
            Session["FullName"] = obj.name;
            Session["Id"] = obj.id;
            Session["Picture"] = obj.picture.data.url;


            return RedirectToAction("Index", "Home");

        }
    }
    public class FbInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
        /*   public string Birthday { get; set; }
           public Hometown Hometown { get; set; }
           public string Gender { get; set; }*/
    }

    public class Hometown
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}