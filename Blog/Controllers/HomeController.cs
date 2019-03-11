using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Post> model = new List<Post>();
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB; Initial Catalog=Blog;Integrated Security=SSPI");

            SqlDataReader rdr = null;

            // 2. Open the connection
            conn.Open();

            // 3. Pass the connection to a command object
            SqlCommand cmd = new SqlCommand("SELECT PostId, Title, Body = SUBSTRING(Body, 1, 100), Link, DatePosted FROM Post ORDER BY DatePosted DESC", conn);

            // get query results
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var post = new Post();

                post.PostId = (int)rdr["PostId"];
                post.Title = rdr["Title"].ToString();
                post.Body = rdr["Body"].ToString();
                post.Link = rdr["Link"].ToString();
                post.DatePosted = (DateTime)rdr["DatePosted"];

                model.Add(post);
            }

            // close the reader
            if (rdr != null)
            {
                rdr.Close();
            }

            // 5. Close the connection
            if (conn != null)
            {
                conn.Close();
            }

            return View(model);
        }
    }
}