using prjMvc.Models;
using prjXamlDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvc.Controllers
{
    public class AController : Controller
    {
        static int count = 0;

        public ActionResult ShowCountByCookie()
        {
            int count = 0;
            HttpCookie x = Request.Cookies["KK"];
            if (x != null)
                count = Convert.ToInt32(x.Value);
            count++;
            x = new HttpCookie("KK");
            x.Value = count.ToString();
            x.Expires = DateTime.Now.AddMinutes(1);
            Response.Cookies.Add(x);

            ViewBag.COUNT = count;
            return View();
        }
        public ActionResult ShowCountBySession()
        {
            int count=0;
            if (Session["COUNT"] != null)
                count = (int)Session["COUNT"];
            count++;
            Session["COUNT"] = count;


            ViewBag.COUNT = count;
            return View();
        }
        public ActionResult Count()
        {
            count++;
            ViewBag.COUNT = count;
            return View();
        }

        [NonAction]
        public string SayHello()
        {
            return "Hello World";
        }

        public string Lotto()
        {
            string number = (new CLottoGen()).LottoGen();
            return number;
        }
        public string demoResponse()
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();

            Response.WriteFile(@"C:\MDA\moon100.png");
            Response.End();
            return "";
        }
        public string demoServer()
        {
            return "目前伺服器上的實體位置：" + Server.MapPath(".");
        }

        public string demoRequest()
        {
            string id = Request.QueryString["pid"];
            if (id == "1")
                return "XBox 加入購物車成功";
            else if (id == "2")
                return "PS5 加入購物車成功";
            else if (id == "3")
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }


        public string demoParameter(int? pid) // ( 傳遞參數法 )   int?=>讓參數接受 null 
        {
            if (pid == null)  //可以對null進行回應
                return "未設定ID";
            if (pid == 1)
                return "XBox 加入購物車成功";
            else if (pid == 2)
                return "PS5 加入購物車成功";
            else if (pid == 3)
                return "Switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string queryByid(int? id)
        {
            string s = "未設定查詢條件";
            if (id == null)  //可以對null進行回應
                return s;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            //Data Source =.; Initial Catalog = MDA; Integrated Security = True; MultipleActiveResultSets = True; Application Name = EntityFramework
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fid="+ id.ToString(),con);
            SqlDataReader reader = cmd.ExecuteReader();
            s = "查詢不到任何條件";

            if(reader.Read())
            {
                s = reader["fName"].ToString() + " / " + reader["fPhone"].ToString();
            }
            con.Close();
            return s;
        }
        public string testingUpdate()
        {
            CCustomer x = new CCustomer()
            {
                fid = 1106,
                fName = "洪暐婷",
                fEmail = "waiting19911118@gmail.com",
                fPassword = "0000",
                fPhone = "0915151154",
                fAddress = "台中市",
                fActived = false
            };
            new CCustomerFactory().update(x);

            return "修改資料成功";
        }
        public string testingInsert()
        {
            CCustomer x = new CCustomer()
            {
                fName = "王婷薇",
                fEmail = "babacoco@gmail.com",
                fPassword = "0000",
                fPhone = "0915151154",
                fAddress="台北市"
            };
            new CCustomerFactory().create(x);

            return "新增資料成功";
        }

        public string testingDelete(int? id)
        {
            if (id == null)
                return "請勿拿系統開玩笑";

            new CCustomerFactory().delete((int)id);
            return "刪除資料成功";
        }
        public ActionResult demoForm() 
        {
            ViewBag.ANS = "?";
            if (!string.IsNullOrEmpty(Request.Form["txtA"]))
            {
                double a = Convert.ToDouble(Request.Form["txtA"]);
                double b = Convert.ToDouble(Request.Form["txtB"]);
                double c = Convert.ToDouble(Request.Form["txtC"]);
                double sqrt = Math.Sqrt((b * b) - (4 * a * c));
                ViewBag.a = a;
                ViewBag.b = b;
                ViewBag.c = c;
                
                ViewBag.ANS = ((-b + sqrt) / (2 * a)).ToString("0.00#") + " , " + ((-b - sqrt) / (2 * a)).ToString("0.00#");
            }
            return View();
        }
        // GET: A
        public ActionResult showById(int? id) //JS弱型別
        {
            if (id != null)  //可以對null進行回應
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                //Data Source =.; Initial Catalog = MDA; Integrated Security = True; MultipleActiveResultSets = True; Application Name = EntityFramework
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fid=" + id.ToString(), con);
                SqlDataReader reader = cmd.ExecuteReader();
                ViewBag.KK = "查詢不到任何條件";

                if (reader.Read())
                {
                    CCustomer x = new CCustomer();
                    x.fid = (int)reader["fid"];
                    x.fName = reader["fName"].ToString();
                    x.fPhone = reader["fPhone"].ToString();
                    x.fEmail = reader["fEmail"].ToString();
                    ViewBag.KK = x;
                }
                con.Close();
            }

            return View();
        }


        public string testingQuery()
        {
            return "目前客戶數: " + (new CCustomerFactory()).queryAll().Count.ToString();
        }
        public ActionResult bindingById(int? id)   //C#強型別
        {
            CCustomer x = null;
            if (id != null)  //可以對null進行回應
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                //Data Source =.; Initial Catalog = MDA; Integrated Security = True; MultipleActiveResultSets = True; Application Name = EntityFramework
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fid=" + id.ToString(), con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    x = new CCustomer();
                    x.fid = (int)reader["fid"];
                    x.fName = reader["fName"].ToString();
                    x.fPhone = reader["fPhone"].ToString();
                    x.fEmail = reader["fEmail"].ToString();
                }
                con.Close();
            }

            return View(x);
        }

    }
}