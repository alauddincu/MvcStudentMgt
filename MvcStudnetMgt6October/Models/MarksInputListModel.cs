using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcStudnetMgt6October.Models
{
    public class MarksInputListModel
    {
        public List<MarksInputList> MarksInputListCollection { get; set;}
    }
    public class MarksInputList:CourseList
    {
        public int MarksInput { get; set; }
        public string StId { get; set; }

        

    }
    
    
}