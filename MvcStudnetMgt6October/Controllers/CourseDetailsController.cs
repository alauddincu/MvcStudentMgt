using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using MvcStudnetMgt6October.Models;

namespace MvcStudnetMgt6October.Controllers
{
    public class CourseDetailsController : Controller
    {

        SqlConnection con = new SqlConnection("Data Source=ALAUDDIN\\SQLEXPRESS;Initial Catalog=dbase;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        public ActionResult ShowAll()
        {

            CourseListModel obmodel = new CourseListModel();
            obmodel.CourseListCollection = GetCourseList();
            if (TempData["Msg"] != null)
            {
                ////////ViewBag.Addmsg = "Yes";
                ViewBag.Msg1 = "done";
                ViewBag.Msg2 = "Updated..";

            }
            return View(obmodel);
        }
        [HttpPost]

        
        public ActionResult ShowAll(FormCollection c)
        {
            CourseListModel obmodel = new CourseListModel();
            obmodel.CourseListCollection = GetCourseList();////goTo method
            if (c["add"] != null)
            {
                ViewBag.Addmsg = "Yes";
                return View(obmodel);
            }


            if (c["save"] != null)
            {
                CourseList cl = new CourseList();
                cl.CourseId = c["cid1"].ToString().Substring(0,c["cid1"].ToString().Length-1);

                cl.CourseName = c["cname1"].ToString().Substring(0,c["cname1"].ToString().Length-1);

                cl.CourseMarks = int.Parse(c["cmarks1"].ToString());

                if (SaveData(cl))
                    TempData["Msg"] = "Info added..";
                else
                    TempData["Msg"] = "Error";

                return  RedirectToAction("ShowAll","CourseDetails");
            }
            if (c["show"] != null)
            {
                
                ViewBag.Addmsg1 = "ok";
                return View(obmodel);
            }

            else if (c["Update"] != null)
            {
                string cid = c["cid1"].ToString();
                string cname = c["cname1"].ToString();
                int cmks = int.Parse(c["cmarks"].ToString());

                if (UpdateMarks(cid, cname, cmks))
                    //ViewBag.Msg = "Updated CourseInfo";
                    TempData["Msg"] = "Info added..";
                else
                    ViewBag.Msg = "Not Updatted";

                return RedirectToAction("ShowAll", "CourseDetails");
            }
            else if (c["Remove"] != null)
            {
                string idd = c["cid1"].ToString();
                if (DeleteCourse(idd))
                    ViewBag.Msg = "Deleted Course";
                else
                    ViewBag.Msg = "Not Deleted";

                return RedirectToAction("ShowAll", "CourseDetails");

            }
         return View(obmodel);
    
            
        }
        public List<CourseList> GetCourseList()
        {
            List<CourseList> c = new List<CourseList>();
            cmd = new SqlCommand("select * from courseDetail", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CourseList course = new CourseList();

                 course.CourseId = dr.GetValue(0).ToString();
                course.CourseName = dr.GetValue(1).ToString();
                course.CourseMarks = int.Parse(dr.GetValue(2).ToString());

                c.Add(course);
            }
            con.Close();

            return c;
        }
        public bool SaveData(CourseList course)
        {
            cmd = new SqlCommand("insert into CourseDetail values (@cid,@cname,@cmarks)", con);

            cmd.Parameters.AddWithValue("@cid", course.CourseId);
            cmd.Parameters.AddWithValue("@cname", course.CourseName);
            cmd.Parameters.AddWithValue("@cmarks", course.CourseMarks);

            con.Open();
            cmd.ExecuteNonQuery();

            con.Close();

            return true;
        }
        public ActionResult Update()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Update(FormCollection c)
        {
            if (c["show"] != null)
            {
                string cid = c["cid1"].ToString();
                CourseList cl = ShowData(cid);

                if (cl.CourseId != null)
                {
                    ViewBag.cid = cl.CourseId;
                    ViewBag.cname = cl.CourseName;
                    ViewBag.cmarks = cl.CourseMarks;

                    ViewBag.Msg = "Found";

                }
                else
                    ViewBag.Msg = "Not Found";

            }
            else if (c["Update"] != null)
            {
                string cid = c["cid1"].ToString();
                string cname = c["cname1"].ToString();
                int cmks = int.Parse(c["cmarks"].ToString());

                if (UpdateMarks(cid, cname, cmks))
                    ViewBag.Msg = "Updated CourseInfo";
                else
                    ViewBag.Msg = "Not Updatted";
            }
            else if (c["Remove"] != null)
            {
                string idd = c["cid1"].ToString();
                if (DeleteCourse(idd))
                    ViewBag.Msg = "Deleted Course";
                else
                    ViewBag.Msg = "Not Deleted";

            }


            return View();
        }
        public bool UpdateMarks(string cid,string name, int cmks)
        {
            cmd = new SqlCommand("update CourseDetail set courseMarks=@cmarks,courseName=@name where courseId=@cid", con);
            cmd.Parameters.AddWithValue("@cid", cid);
            //cmd.Parameters.AddWithValue("@cid",cid);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@cmarks", cmks);
            con.Open();
            int n = cmd.ExecuteNonQuery();

            con.Close();
            if (n > 0)
                return true;
            else
                return false;

            // return true;
        }
        public CourseList ShowData(string cid)
        {
            CourseList cl = new CourseList();
            cmd = new SqlCommand("Select * from CourseDetail where courseId=@cid", con);
            cmd.Parameters.AddWithValue("@cid", cid);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cl.CourseId = dr.GetValue(0).ToString();
                cl.CourseName = dr.GetValue(1).ToString();
                cl.CourseMarks = int.Parse(dr.GetValue(2).ToString());

            }

            con.Close();


            return cl;

        }

        //public ActionResult Delete()
        //{
        //    return View();
        //}
       // [HttpPost]
        //public ActionResult Delete(FormCollection c)
        //{
        //    if (c["Remove"] != null)
        //    {
        //        string idd = c["cid1"].ToString();
        //        if (DeleteCourse(idd))
        //            ViewBag.Msg = "Deleted Course";
        //        else
        //            ViewBag.Msg = "Not Deleted";

        //    }
        //    return View();
        //}
        public bool DeleteCourse(string cid)
        {
            cmd = new SqlCommand("delete from courseDetail where courseId=@cid", con);
            cmd.Parameters.AddWithValue("@cid", cid);
            con.Open();
            int n = cmd.ExecuteNonQuery();

            con.Close();
            if (n > 0)
                return true;
            else
                return false;



        }

    }


}
