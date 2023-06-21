using SocialMedia.Models;
using SocialMedia.Services;
using SocialMedia.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class InstagramController : Controller
    {

        readonly FacebookRequest facebookRequest = new FacebookRequest();

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
                return View();
            }
            else if (profile is ErrorResponse errorResponse)
            {
                ViewBag.Error = errorResponse.error.message;
                return View();
            }
            else
            {
                var insProfileData = (InstagramProfile)profile;
                Session["Name"] = insProfileData.username;
                Session["Avt"] = insProfileData.profile_picture_url;
                ViewBag.Data = insProfileData;

                //todo: use api to get comments
                List<InstagramMedia> instagramMedias = new List<InstagramMedia>();
                var media = insProfileData.media.data;

                for (int i = 0; i < media.Count; i++)
                {
                    var insMediaRes = await instagramService.GetMediaDetail(media[i].id);

                    if (insMediaRes is InstagramMedia insMediaData)
                    {

                        instagramMedias.Add(insMediaData);
                    }
                }

                ViewBag.Media = instagramMedias;



                return View();
            }


        }


        public async Task<ActionResult> Media(string id)
        {
            var accessToken = Session["AccessToken"];
            if (accessToken == null || await facebookRequest.AccessTokenValid(accessToken.ToString()) == false)
            {
                return RedirectToAction("Login", "FacebookAuth");
            }
            InstagramService instagramService = new InstagramService(accessToken.ToString());

            if (id == null)
            {
                return RedirectToAction("Index", "Instagram");
            }
            var insMediaRes = await instagramService.GetMediaDetail(id);

            if (insMediaRes is FacebookError facebookErr)
            {
                ViewBag.Error = facebookErr.error.message;
                return View();
            }
            else if (insMediaRes is ErrorResponse errorResponse)
            {
                ViewBag.Error = errorResponse.error.message;
                return View();
            }
            else
            {
                var insMediaData = (InstagramMedia)insMediaRes;
                ViewBag.Data = insMediaData;
                return View();
            }


        }

        [HttpGet]
        public async Task<JsonResult> GetMediaComment(string ig_media_id)
        {
            var accessToken = Session["AccessToken"];
            if (accessToken == null || await facebookRequest.AccessTokenValid(accessToken.ToString()) == false)
            {
                ErrorResponse error = new ErrorResponse(-1, "Unauthorized");
                return Json(error, JsonRequestBehavior.AllowGet);
            }

            if (ig_media_id == null)
            {
                ErrorResponse error = new ErrorResponse(-1, "ig_media_id is require");
                return Json(error, JsonRequestBehavior.AllowGet);
            }

            InstagramService instagramService = new InstagramService(accessToken.ToString());
            var data = await instagramService.GetMediaComment(ig_media_id);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async Task<JsonResult> GetCommentReply(string ig_comment_id)
        {
            var accessToken = Session["AccessToken"];
            if (accessToken == null || await facebookRequest.AccessTokenValid(accessToken.ToString()) == false)
            {
                ErrorResponse error = new ErrorResponse(-1, "Unauthorized");
                return Json(error, JsonRequestBehavior.AllowGet);
            }

            if (ig_comment_id == null)
            {
                ErrorResponse error = new ErrorResponse(-1, "ig_media_id is require");
                return Json(error, JsonRequestBehavior.AllowGet);
            }

            InstagramService instagramService = new InstagramService(accessToken.ToString());
            var data = await instagramService.GetCommentReply(ig_comment_id);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<JsonResult> CreateComment(string id, string message, CommentType commentType)
        {

            var accessToken = Session["AccessToken"];
            if (accessToken == null || await facebookRequest.AccessTokenValid(accessToken.ToString()) == false)
            {
                ErrorResponse error = new ErrorResponse(-1, "Unauthorized");
                return Json(error, JsonRequestBehavior.AllowGet);
            }

            if (id == null || message == null)
            {
                ErrorResponse error = new ErrorResponse(-1, "missing data");
                return Json(error, JsonRequestBehavior.AllowGet);
            }

            InstagramService instagramService = new InstagramService(accessToken.ToString());
            var data = await instagramService.CreateComment(id, message, commentType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> Test()
        {
            var accessToken = Session["AccessToken"];
            if (accessToken == null || await facebookRequest.AccessTokenValid(accessToken.ToString()) == false)
            {
                return Json("Unauthorized", JsonRequestBehavior.AllowGet);
            }
            InstagramService instagramService = new InstagramService(accessToken.ToString());

            object me = await instagramService.GetMediaDetail("17996583520821414");
            return Json(me, JsonRequestBehavior.AllowGet);

        }
    }
}