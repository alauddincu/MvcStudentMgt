using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MvcStudnetMgt6October.Models;
using System.Data.SqlClient;

namespace MvcStudnetMgt6October.Controllers
{
    public class LoginDetailsController : Controller
    {   //
        // GET: /LoginDetails/
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection c)
        {

            if (c["signin"] != null)
            {
                string uid = c["uname"].ToString();
                string upass = c["pass"].ToString();
                string utype = c["utype"].ToString();
                //string utypeR = c["utypeR"].ToString();

                LoginList l = CheckPass(uid, upass, utype);

                if (l.Uid == uid && l.Pass == upass && l.Type == utype)
                    return RedirectToAction("Index", "SMS");
                    //ViewBag.Msg = "Matched";
                else
                    ViewBag.Msg = "Not Matched";

            }

            return View();
        }
        public LoginList CheckPass(string uid, string upass, string utype)
        {
            LoginList ll = new LoginList();
            SqlConnection con = new SqlConnection("Data Source=ALAUDDIN\\SQLEXPRESS;Initial Catalog=dbase;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select userMail,userPass,userType from UserAcc where userMail=@umail ", con);
            cmd.Parameters.AddWithValue("@umail", uid);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                ll.Uid = dr.GetValue(0).ToString();
                ll.Pass = dr.GetValue(1).ToString();
                ll.Type = dr.GetValue(2).ToString();
                //ll.Type = dr.GetValue(3).ToString();
            }

            con.Close();

            return ll;
        }

        public ActionResult ChangePass()
        {
            return View();
        }

        [HttpPost]

        public ActionResult ChangePass(FormCollection c)
        {

            if (c["changepass"] != null)
            {
                string cpassp = c["prepass"].ToString();

                string uid = c["umail"].ToString();
                string utype = c["utype"].ToString();
                string upass = c["prepass"].ToString();

                ChangePassList l = CheckPass1(uid, uid, utype);

                if (l.UserMail == uid && l.PassPre== upass && l.UserType == utype)
                    
                {
                    string cpass1 = c["newpass1"].ToString();
                    string cpass2 = c["newpass2"].ToString();
                    
                    string mail = c["umail"].ToString();
                  
                    if (cpass1 == cpass2)
                    {
                        if(UpdatePass(cpass1,mail))
                            ViewBag.Msg = "Changed";
                        else
                            ViewBag.Msg = "Not Changed";
                    }
                }
                else
                    ViewBag.Msg = "Not matched to previous pass...";
              }
                return View();
           }
        


        public bool UpdatePass(string cp1,string mail)
        {
            SqlConnection con = new SqlConnection("Data Source=ALAUDDIN\\SQLEXPRESS;Initial Catalog=dbase;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("update UserAcc set userPass=@pass where userMail=@mail", con);
            cmd.Parameters.AddWithValue("@mail", mail);
            cmd.Parameters.AddWithValue("@pass", cp1);
            con.Open();
            int n = cmd.ExecuteNonQuery();

            con.Close();
            if (n > 0)
                return true;
            else
                return false;

        
        }
           
        public ChangePassList CheckPass1(string uid, string upass, string utype)
        {
            ChangePassList ll = new ChangePassList();
            SqlConnection con = new SqlConnection("Data Source=ALAUDDIN\\SQLEXPRESS;Initial Catalog=dbase;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select userMail,userPass,userType from UserAcc where userMail=@umail ", con);
            cmd.Parameters.AddWithValue("@umail", uid);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                ll.UserMail= dr.GetValue(0).ToString();
                ll.PassPre= dr.GetValue(1).ToString();
                ll.UserType = dr.GetValue(2).ToString();
            }

            con.Close();

            return ll;
        }
        
    }
    }

        
