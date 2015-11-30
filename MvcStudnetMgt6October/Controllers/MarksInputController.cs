using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStudnetMgt6October.Models;
using MvcStudnetMgt6October.Controllers;
using System.Data.SqlClient;
using System.Data;


namespace MvcStudnetMgt6October.Controllers
{
    public class MarksInputController : Controller
    {
        SqlDataReader dr;
        SqlConnection con = new SqlConnection("Data Source=ALAUDDIN\\SQLEXPRESS;Initial Catalog=dbase;Integrated Security=True");
        SqlCommand cmd;
        //
        // GET: /MarksInput/
        public ActionResult Index()
        {
            MarksInputListModel obmodel = new MarksInputListModel();
            obmodel.MarksInputListCollection = GetStudentMarksList();
            return View(obmodel);
        }
        public List<MarksInputList> GetStudentMarksList()
        {
            List<MarksInputList> m = new List<MarksInputList>();

            SqlConnection con = new SqlConnection("Data Source=ALAUDDIN\\SQLEXPRESS;Initial Catalog=dbase;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select * from markstb",con);
              SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MarksInputList mit = new MarksInputList();
                mit.CourseId = dr.GetValue(2).ToString();
                mit.StId = dr.GetValue(1).ToString();
                mit.CourseMarks = int.Parse(dr.GetValue(0).ToString());


                m.Add(mit);


            }
            con.Close();
                return m;

        }

        public List<StudentList> GetStudentIdList()
        {
            List<StudentList> st = new List<StudentList>();
        cmd = new SqlCommand("select id from StudentDetail", con);
        con.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            StudentList stt = new StudentList();
            stt.Id = dr.GetValue(0).ToString();

            st.Add(stt);
        }
        con.Close();
        return st;
       
        }
        public List<CourseList> GetCourseList()
        {
            List<CourseList> c = new List<CourseList>();
            cmd = new SqlCommand("select courseId,courseName from courseDetail", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CourseList course = new CourseList();

                course.CourseId = dr.GetValue(0).ToString();
                course.CourseName = dr.GetValue(1).ToString();
               // course.CourseMarks = int.Parse(dr.GetValue(2).ToString());

                c.Add(course);
            }
            con.Close();

            return c;
        }
        public ActionResult CalculateMarks()
        {
            CourseListModel obmodel = new CourseListModel();
            // obmodel.CourseListCollection = GetCourseList();
           ////////// obmodel.CourseListCollection =  GetCourseList();
            obmodel.StudentListCollection = GetStudentIdList();
            return View(obmodel);
        }

        [HttpPost]
        public ActionResult CalculateMarks(FormCollection c) 
        {

            CourseListModel obmodel = new CourseListModel();
            obmodel.CourseListCollection = GetCourseList();
            obmodel.StudentListCollection = GetStudentIdList();
          //  obmodel.CourseListCollection = GetStudentIdList();
           // CourseListModel obmodel = new CourseListModel();/////////////

            if (c["addmarks"] != null)
            {
                string s = c["sidata"].ToString();
                ViewBag.siddata = s;
                ViewBag.Msgss = "okk";
                return View(obmodel);

            }
            if (c["savemarks"] != null)
            {
                MarksInputList mil = null;

                string sid1 = c["sidata"].ToString();
                string str = c["marksdata"].ToString();
                string[] splitstr = str.Split(',');
                int i=0;
                bool flag = false;
                foreach(string ss1 in splitstr)
                {
                 //= int.Parse(c["MarksInput"].ToString();
                    string cid = "c-"+ss1.ToString();
                    int marks = int.Parse(c[cid].ToString());
                   // string sid = "53";
                   
                    mil = new MarksInputList();
                    mil.CourseId = cid.Substring(2);
                    mil.StId = sid1;
                    mil.MarksInput = marks;
                    if (SaveMarks(mil))
                        flag = true;


                }
                if (flag == true)
                    ViewBag.msgg = "Marks saved...";
                else
                    ViewBag.msgg = "Not saving..";
         }
          //  obmodel = new CourseListModel();
           // obmodel.CourseListCollection = GetCourseList();

          return View(obmodel); 

            
        }
        public bool SaveMarks(MarksInputList mi)
        {
            CourseList obmodel = new CourseList();

            cmd = new SqlCommand("insert into MarksTb (InputMarks,id,courseid) values (@cmarks,@id,@cid) ", con);
            cmd.Parameters.AddWithValue("@cmarks", mi.MarksInput);
            cmd.Parameters.AddWithValue("@cid", mi.CourseId);
            cmd.Parameters.AddWithValue("@id", mi.StId);

            con.Open();
            cmd.ExecuteNonQuery();

            con.Close();

            return true;
        
        }
	}
}