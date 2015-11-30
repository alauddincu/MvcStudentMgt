using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcStudnetMgt6October.Models
{
    public class CourseList
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public int CourseMarks { get; set; }
        public int MarksInput { get; set; }
    }
    public class CourseListModel
    {
        public List<CourseList> CourseListCollection { get; set; }
        public List<StudentList> StudentListCollection { get; set; }
      //  public CourseList CourseListDetaills { get; set; }
    }
   
}