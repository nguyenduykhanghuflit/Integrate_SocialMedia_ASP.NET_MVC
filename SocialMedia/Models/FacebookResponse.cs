using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Me
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class CategoryList
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Data
    {
        public string access_token { get; set; }
        public string category { get; set; }
        public List<CategoryList> category_list { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public List<string> tasks { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
    }

    public class PageInfo
    {
        public List<Data> data { get; set; }
        public Paging paging { get; set; }
    }

    public class Error
    {
        public string message { get; set; }
        public string type { get; set; }
        public int code { get; set; }
        public int error_subcode { get; set; }
        public string fbtrace_id { get; set; }
    }

    public class FacebookError
    {
        public Error error { get; set; }
    }
    public class AppToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
    }

    public class Valid
    {
        public bool is_valid { get; set; }
    }
    public class TokenValid
    {
        public Valid data { get; set; }
    }
}