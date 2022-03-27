using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Dynamic;
using TechTalk.SpecFlow.CommonModels;

namespace host_app.Controllers
{
    class user
    {
        public string id { get; set; }
        public user()
        {

        }
    }
    class film
    {
        public film(string id, string name, string country, string description, string url_img, string url_film, string year_public, string age)
        {
            this.id = id;
            this.name = name;
            this.country = country;
            this.description = description;
            this.url_img = url_img;
            this.url_film = url_film;
            this.year_public = year_public;
            this.age = age;
        }

        public string id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string description { get; set; }
        public string url_img { get; set; }
        public string url_film { get; set; }
        public string year_public { get; set; }
        public string age { get; set; }
      
    }
    public class User : Controller
    {
       static dynamic  success=new ExpandoObject();
       dynamic error=new ExpandoObject();
        
       
        SqlConnection conn = new SqlConnection("Server=DESKTOP-FLFGLMO\\SQLEXPRESS;Database=webnangcao;Trusted_Connection=True;");

        [HttpPost("Login")]
        public IActionResult login([FromForm] string Email,[FromForm] string password)
        {
            string id = "";
            SqlCommand cmd = new SqlCommand("select ID_USER from USER_SECURITY where EMAIL='" + Email + "' and PASSWORD='" + password + "'", conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                id = (string)dr["ID_USER"];

            }
            dr.Close();
            conn.Close();
            if (id=="")
            {
                SqlCommand cmd2 = new SqlCommand("select ID_ADMIN from admin where name='" + Email + "' and PASSWORD='" + password + "'", conn);
                conn.Open();
                SqlDataReader dr2 = cmd2.ExecuteReader();
             
                while (dr2.Read())
                {
                    id = (string)dr2["ID_ADMIN"];

                }
                conn.Close();
            }
            user User = new user();
            User.id = id;
            return Json(User);
        }

        [HttpPut("user")]
        public IActionResult Postuser([FromForm] string name, [FromForm] int age, [FromForm] string url)
        {
            SqlCommand cmd = new SqlCommand("insert into [dbo].[USER] (ID_USER, NAME, AGE, URL_IMG) values('USER06', '"+name+"',"+age+", '"+url+"'); ", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            var myData = new
            {
                status = "success"
            };
            return Json(myData);
        }

        //actor

        [HttpPut("actor")]
        public IActionResult Postactor([FromForm] string name, [FromForm] string description, [FromForm] string url)
        {
            SqlCommand cmd = new SqlCommand("insert into ACTOR (ID_ACTOR,NAME_ACTOR,DESCRIPTION,URL_IMG) values ('ACTOR15','"+name+"','"+description+"','"+url+"'); ", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            var myData = new
            {
                status = "success"
            };
            return Json(myData);
        }
        [HttpGet("actor")]
        public IActionResult getactor()
        {
            SqlCommand cmd = new SqlCommand("select * from actor", conn);
            conn.Open();
            List<dynamic> actors = new List<dynamic>();
           
            SqlDataReader dr =  cmd.ExecuteReader();

            while (dr.Read())
            {
                dynamic actor = new ExpandoObject();
                actor.Name = (string)dr["name_actor"];
                actor.description=(string)dr["description"];
                actor.url=(string)dr["url_img"];
                actors.Add(actor);
            }
            conn.Close();
            
            return Json(actors);
        }
        [HttpGet("actor/{id}")]
        public IActionResult getactor_id(string id)
        {
            SqlCommand cmd = new SqlCommand("select* from [dbo].[ACTOR] where ID_ACTOR='"+id+"' and status=1", conn);
            conn.Open();
            dynamic actor = new ExpandoObject();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                actor.Name = (string)dr["name_actor"];
                actor.description = (string)dr["description"];
                actor.url = (string)dr["url_img"];

            }
            else
            {
                actor.status = "failed";
            }
            conn.Close();
            return Json(actor);
        }
        [HttpDelete("actor/{id}")]
        public IActionResult getactor(string id)
        {

            SqlCommand cmd = new SqlCommand("update ACTOR set [dbo].[ACTOR].status=0 where ID_ACTOR='"+id+"'", conn);
            conn.Open(); 
            cmd.ExecuteNonQuery();
            conn.Close();
            var myData = new
            {
                status = "success"
            };
            return Json(myData);
        }

        [HttpPost("actor/{id}")]
        public IActionResult UPDATEaCTOR(string id,[FromForm] string name,string description,string url)
        {

            SqlCommand cmd = new SqlCommand("update ACTOR set  NAME_ACTOR='"+name+"',DESCRIPTION='"+description+"',URL_IMG='"+url+"' where ID_ACTOR='"+id+"'", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            var myData = new
            {
                status = "success"
            };
            return Json(myData);
        }

        //admin

        [HttpPut("admin")]
        public IActionResult Postaddmin([FromForm] string name, [FromForm] string password)
        {
            SqlCommand cmd = new SqlCommand("insert into ADMIN (ID_ADMIN , NAME , PASSWORD) values ('AD05','" + name + "','" + password + "');", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            var myData = new
            {
                status = "success"
            };
            return Json(myData);
        }
        [HttpGet("admin")]
        public IActionResult getadmin()
        {
            SqlCommand cmd = new SqlCommand("select * from actor", conn);
            conn.Open();
            List<dynamic> admins = new List<dynamic>();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dynamic admin = new ExpandoObject();
                admin.id_adin = (string)dr["ID_ADMIN"];
                admin.name = (string)dr["NAME"];
                admin.password = (string)dr["PASSWORD"];
                admins.Add(admin);
            }
            conn.Close();

            return Json(admins);
        }
        //[HttpDelete("actor/{id}")]
        //public IActionResult getactor(string id)
        //{

        //    SqlCommand cmd = new SqlCommand("update ACTOR set [dbo].[ACTOR].status=0 where ID_ACTOR='" + id + "'", conn);
        //    conn.Open();
        //    cmd.ExecuteNonQuery();
        //    conn.Close();
        //    var myData = new
        //    {
        //        status = "success"
        //    };
        //    return Json(myData);
        //}

        //[HttpPost("actor/{id}")]
        //public IActionResult UPDATEaCTOR(string id, [FromForm] string name, string description, string url)
        //{

        //    SqlCommand cmd = new SqlCommand("update ACTOR set  NAME_ACTOR='" + name + "',DESCRIPTION='" + description + "',URL_IMG='" + url + "' where ID_ACTOR='" + id + "'", conn);
        //    conn.Open();
        //    cmd.ExecuteNonQuery();
        //    conn.Close();
        //    var myData = new
        //    {
        //        status = "success"
        //    };
        //    return Json(myData);
        //}
      

        //film
        [HttpPost("FIlm")]
        public IActionResult Postfilm([FromForm] string name, [FromForm] string password)
        {
            return View();
        }
        [HttpGet("FIlm")]
        public IActionResult getfilm()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM FILM",conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<film> list = new List<film>(); 
            while (dr.Read())
            {
                list.Add(new film((string)dr["id_film"], (string)dr["name"], (string)dr["country"], (string)dr["description"], (string)dr["url_img"], (string)dr["url_film"], (string)dr["year_public"], (string)dr["age_limit"]));
            }
            conn.Close();
            return Json(list);
        }
        

    }
}
