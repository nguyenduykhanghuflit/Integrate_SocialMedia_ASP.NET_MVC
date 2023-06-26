using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialMedia.Models;
using System.Configuration;

namespace SocialMedia.Controllers
{
    public class HomeController : Controller
    {
        readonly string sqlconnectStr = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatePost()
        {

            return View();
        }

        public ActionResult EditPost(string id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.Id = id;
                return View();

            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;

                return View();
            }
        }

        public ActionResult DetailPost(string id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                List<PostResponse> res = new List<PostResponse>();

                //sử dụng using để tự động đóng kết nối
                using (var connection = new SqlConnection(sqlconnectStr))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("khangGetBlogDemo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    _ = command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            PostResponse item = new PostResponse
                            {
                                Id = (int)reader.GetInt64(0),
                                Notes = (string)reader["Notes"],
                                Title = (string)reader["Title"],
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                UpdatedDate = (DateTime)reader["UpdatedDate"],

                            };
                            res.Add(item);
                        }
                    }

                }


                ViewBag.data = res.ToList()[0];

                return View();

            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;

                return View();
            }


        }

        public ActionResult DeletePost(string id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }



                //sử dụng using để tự động đóng kết nối
                using (var connection = new SqlConnection(sqlconnectStr))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("khangDeletePostDemo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    _ = command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();


                }


                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;

                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        public JsonResult NewPost(PostDto postDto)
        {
            try
            {
                if (postDto.Notes == null || postDto.Title == null)
                {

                    var err = new
                    {
                        error = -1,
                        data = "",
                        message = "Data invalid"
                    };
                    return Json(err, JsonRequestBehavior.AllowGet);
                }

                List<PostResponse> res = new List<PostResponse>();

                //sử dụng using để tự động đóng kết nối
                using (var connection = new SqlConnection(sqlconnectStr))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("khangCreatePostDemo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    _ = command.Parameters.AddWithValue("@Notes", postDto.Notes);
                    _ = command.Parameters.AddWithValue("@Title", postDto.Title);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            PostResponse item = new PostResponse
                            {
                                Id = (int)reader.GetInt64(0),
                                Title = (string)reader["Title"],
                                Notes = (string)reader["Notes"],
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                UpdatedDate = (DateTime)reader["UpdatedDate"],

                            };
                            res.Add(item);
                        }
                    }

                }

                var data = new
                {
                    error = 0,
                    data = res.ToList()[0],
                    message = "success"
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new
                {
                    error = -1,
                    data = "",
                    message = ex.Message
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpGet]
        public JsonResult GetPost(int? id)
        {

            try
            {
                if (id == null) id = 0;

                List<PostResponse> res = new List<PostResponse>();

                //sử dụng using để tự động đóng kết nối
                using (var connection = new SqlConnection(sqlconnectStr))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("khangGetBlogDemo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    _ = command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            PostResponse item = new PostResponse
                            {
                                Id = (int)reader.GetInt64(0),
                                Notes = (string)reader["Notes"],
                                Title = (string)reader["Title"],
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                UpdatedDate = (DateTime)reader["UpdatedDate"],

                            };
                            res.Add(item);
                        }
                    }

                }


                if (id > 0)
                {
                    var data = new
                    {
                        error = 0,
                        data = res.ToList()[0],
                        message = "success"
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new
                    {
                        error = 0,
                        data = res.ToList(),
                        message = "success"
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var data = new
                {
                    error = -1,
                    data = "",
                    message = ex.Message
                };
                return Json(data, JsonRequestBehavior.AllowGet);

            }

        }


        [HttpPost]
        public JsonResult UpdatePost(PostUpdateDto postDto)
        {
            try
            {
                if (postDto.Notes == null || postDto.Title == null)
                {

                    var err = new
                    {
                        error = -1,
                        data = "",
                        message = "Data invalid"
                    };
                    return Json(err, JsonRequestBehavior.AllowGet);
                }

                List<PostResponse> res = new List<PostResponse>();

                //sử dụng using để tự động đóng kết nối
                using (var connection = new SqlConnection(sqlconnectStr))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("khangUpdatePostDemo", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    _ = command.Parameters.AddWithValue("@Notes", postDto.Notes);
                    _ = command.Parameters.AddWithValue("@Title", postDto.Title);
                    _ = command.Parameters.AddWithValue("@Id", postDto.Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            PostResponse item = new PostResponse
                            {
                                Id = (int)reader.GetInt64(0),
                                Title = (string)reader["Title"],
                                Notes = (string)reader["Notes"],
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                UpdatedDate = (DateTime)reader["UpdatedDate"],

                            };
                            res.Add(item);
                        }
                    }

                }

                var data = new
                {
                    error = 0,
                    data = res.ToList()[0],
                    message = "success"
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new
                {
                    error = -1,
                    data = "",
                    message = ex.Message
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }




    }
}