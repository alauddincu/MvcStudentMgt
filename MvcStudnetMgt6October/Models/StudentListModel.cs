using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcStudnetMgt6October.Models
{
    public class StudentListModel
     {
        public List<StudentList> StdentListCollection { get; set; }
        public StudentList StudentListDetails { get; set; }
    }
    public class StudentList
    {
        public string Id{get;set;}
        public string Name{get;set;}
        public string Address{get;set;}
        public string Section{get;set;}
        public int Marks{get;set;}

    }
    
}