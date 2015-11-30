using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStudnetMgt6October.Models;
using System.Data.SqlClient;


namespace MvcStudnetMgt6October.Controllers
{
    public class StudentDetailsController : Controller
    {
        public ActionResult Index()
        {
            StudentListModel obmodel = new StudentListModel();
            // obmodel.StudentListCollection = new List<StudentList>();
            obmodel.StdentListCollection = GetStudentList();

            return View(obmodel);
        }
        public List<StudentList> GetStudentList()
        {
            List<StudentList> s = new List<StudentList>();
            SqlConnection con = new SqlConnection("Data Source=ALAUDDIN\\SQLEXPRESS;Initial Catalog=dbase;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Select * from studentDetail", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                StudentList stud = new StudentList();
                stud.Id = dr.GetValue(0).ToString();
                stud.Name = dr.GetValue(1).ToString();
                stud.Address = dr.GetValue(2).ToString();
                stud.Section = dr.GetValue(3).ToString();
                stud.Marks = int.Parse(dr.GetValue(4).ToString());

                s.Add(stud);

            }

            con.Close();
            return s;
        }
        
        public ActionResult Insert()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Insert(FormCollection c)
        {
            StudentList s = new StudentList();
            s.Id = c["id1"].ToString();
            s.Name = c["name1"].ToString();
            s.Address = c["address"].ToString();
            s.Section = c["section"].ToString();
            s.Marks = int.Parse(c["marks"].ToString());
            if (SaveData(s))
                ViewBag.Msg = "Successfully Added";
            else
                ViewBag.Msg = "Error Occured";
            return View();
        }

        public bool SaveData(StudentList stud)
        {


            SqlConnection con = new SqlConnection("Data Source=ALAUDDIN\\SQLEXPRESS;Initial Catalog=dbase;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("insert into StudentDetail values (@id,@name,@address,@section,@marks) ", con);
            cmd.Parameters.AddWithValue("@id", stud.Id);
            cmd.Parameters.AddWithValue("@name", stud.Name);
            cmd.Parameters.AddWithValue("@address", stud.Address);
            cmd.Parameters.AddWithValue("@section", stud.Section);
            cmd.Parameters.AddWithValue("@marks", stud.Marks);
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
                string id = c["id1"].ToString();
                StudentList s = ShowData(id);

                if (s.Id != null)
                {
                    ViewBag.id = s.Id;
                    ViewBag.name = s.Name;
                    ViewBag.address = s.Address;
                    ViewBag.section = s.Section;
                    ViewBag.marks = s.Marks;

                    ViewBag.Msg = "Found";
                }

                //if(s.Id!=null)
                //ViewBag.Msg = "Found";
                else
                    ViewBag.Msg = "Not Found";


            }
            else if (c["Update"] != null)
            {
                string id = c["id1"].ToString();
                int mks = int.Parse(c["marks"].ToString());
                if (UpdateMarks(id, mks))
                    ViewBag.Msg = "Updated Marks";

                else
                    ViewBag.Msg = "Error";


            }
            return View();
        }
        public bool UpdateMarks(string id, int mks)
        {

            SqlConnection con = new SqlConnection("Data Source=ALAUDDIN\\SQLEXPRESS;Initial Catalog=dbase;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("update StudentDetail set marks=@marks where id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@marks", mks);
            con.Open();
            int n = cmd.ExecuteNonQuery();

            con.Close();
            if (n > 0)
                return true;
            else
                return false;



            // return true;
        }
        public StudentList ShowData(string id)
        {
            // List<StudentList> s = new List<StudentList>();
            StudentList s = new StudentList();
            SqlConnection con = new SqlConnection("Data Source=ALAUDDIN\\SQLEXPRESS;Initial Catalog=dbase;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Select id,name,address,section,marks from StudentDetail where id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                //StudentList stud = new StudentList();
                s.Id = dr.GetValue(0).ToString();
                s.Name = dr.GetValue(1).ToString();
                s.Address = dr.GetValue(2).ToString();
                s.Section = dr.GetValue(3).ToString();
                s.Marks = int.Parse(dr.GetValue(4).ToString());

                // s.Add(stud);

            }


            con.Close();
            return s;
        }

    }
}


