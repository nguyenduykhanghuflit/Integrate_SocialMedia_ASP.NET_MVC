using SocialMedia.Models;
using SocialMedia.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Services
{

    public class InstagramService
    {

        private readonly string AccessToken;
        private static readonly string fbUrl = "https://graph.facebook.com/v17.0/";



        public InstagramService(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public async Task<object> GetInstagramProfile()
        {
            FacebookRequest facebookRequest = new FacebookRequest();
            FacebookService facebookService = new FacebookService(AccessToken);

            var pageInfo = await facebookService.GetPageInfo();
            if (pageInfo is FacebookError || pageInfo is ErrorResponse)
            {
                return pageInfo;
            }
            else
            {
                var pageInfoData = (PageInfo)pageInfo;
                string pageId = pageInfoData.data[0].id;
                string pageAccessToken = pageInfoData.data[0].access_token;

                string url = fbUrl + pageId
                  + "?fields=instagram_business_account,global_brand_page_name,name,about,followers_count,app_id,picture{url,width,height},business,fan_count&access_token="
                  + pageAccessToken;

                var insInfo = await facebookRequest.Get<InstagramInfo>(url);

                if (insInfo is FacebookError || insInfo is ErrorResponse)
                {
                    return insInfo;
                }
                else
                {
                    var insInfoData = (InstagramInfo)insInfo;
                    string insId = insInfoData.instagram_business_account.id;
                    var urlProfile = fbUrl + insId +
                       "?fields=username,followers_count,follows_count,biography,media_count,name,profile_picture_url,media,stories,ig_id,id&access_token=" + pageAccessToken;
                    object instagramProfile = await facebookRequest.Get<InstagramProfile>(urlProfile);

                    return instagramProfile;
                }

            }




        }

        public async Task<object> GetMediaDetail(string id)
        {
            FacebookRequest facebookRequest = new FacebookRequest();
            string url = fbUrl + id +
               "?fields=caption,comments_count,id,is_comment_enabled,is_shared_to_feed,like_count,media_product_type,media_type,media_url,thumbnail_url,timestamp,shortcode,username,children,owner&access_token="
               + AccessToken;
            return await facebookRequest.Get<InstagramMedia>(url);

        }


        public async Task<object> GetMediaComment(string ig_media_id)
        {
            FacebookRequest facebookRequest = new FacebookRequest();
            string url = fbUrl + ig_media_id + "/comments";
            string fields = "?fields=from,id,like_count,replies,timestamp,text,user,username&access_token=" + AccessToken;
            return await facebookRequest.Get<Comments>(url + fields);
        }

        public async Task<object> GetCommentReply(string ig_comment_id)
        {
            FacebookRequest facebookRequest = new FacebookRequest();
            string url = fbUrl + ig_comment_id + "/replies";
            string fields = "?fields=from,id,like_count,timestamp,text,user,username&access_token=" + AccessToken;
            return await facebookRequest.Get<Comments>(url + fields);
        }

        public async Task<object> CreateComment(string id, string message, CommentType commentType)
        {

            FacebookRequest facebookRequest = new FacebookRequest();
            string endpoint = commentType == CommentType.Comments ? "/comments" : "/replies";
            string url = fbUrl + id + endpoint + $"?message={message}&access_token={AccessToken}";
            return await facebookRequest.Post<CommentItem>(url);
        }

    }
}