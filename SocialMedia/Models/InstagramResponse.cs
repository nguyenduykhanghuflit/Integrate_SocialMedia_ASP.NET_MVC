using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{

    public class Business
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class PictureData
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class InstagramBusinessAccount
    {
        public string id { get; set; }
    }

    public class Picture
    {
        public PictureData data { get; set; }
    }

    public class InstagramInfo
    {
        public InstagramBusinessAccount instagram_business_account { get; set; }
        public string global_brand_page_name { get; set; }
        public string name { get; set; }
        public string about { get; set; }
        public int followers_count { get; set; }
        public Picture picture { get; set; }
        public Business business { get; set; }
        public int fan_count { get; set; }
        public string id { get; set; }
    }



    public class Datum
    {
        public string id { get; set; }
    }

    public class Media
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }



    public class InstagramProfile
    {
        public string username { get; set; }
        public int followers_count { get; set; }
        public int follows_count { get; set; }
        public string biography { get; set; }
        public int media_count { get; set; }
        public string name { get; set; }
        public string profile_picture_url { get; set; }
        public Media media { get; set; }
        public Media stories { get; set; }
        public long ig_id { get; set; }
        public string id { get; set; }
    }


}