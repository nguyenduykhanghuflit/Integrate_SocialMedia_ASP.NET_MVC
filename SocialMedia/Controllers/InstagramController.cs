using SocialMedia.Models;
using SocialMedia.Services;
using SocialMedia.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class InstagramController : Controller
    {
        // GET: Instagram

        FacebookRequest facebookRequest = new FacebookRequest();

        public async Task<ActionResult> Index()
        {
            var accessToken = Session["AccessToken"];
            if (accessToken == null || await facebookRequest.AccessTokenValid(accessToken.ToString()) == false)
            {
                return RedirectToAction("Login", "FacebookAuth");
            }
            InstagramService instagramService = new InstagramService(accessToken.ToString());

            var profile = await instagramService.GetInstagramProfile();

            if (profile is FacebookError facebookErr)
            {
                ViewBag.Error = facebookErr.error.message;
            }
            else if (profile is ErrorResponse errorResponse)
            {
                ViewBag.Error = errorResponse.Message;
            }
            else
            {
                ViewBag.Data = (InstagramProfile)profile;
            }

            return View();
        }

        public async Task<JsonResult> Test()
        {
            var accessToken = Session["AccessToken"];
            if (accessToken == null || await facebookRequest.AccessTokenValid(accessToken.ToString()) == false)
            {
                return Json("Unauthorized", JsonRequestBehavior.AllowGet);
            }
            InstagramService instagramService = new InstagramService(accessToken.ToString());

            var me = await instagramService.GetMediaDetail("17996583520821414");
            return Json(me, JsonRequestBehavior.AllowGet);

        }
    }
}