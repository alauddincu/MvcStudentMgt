using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcStudnetMgt6October.Models
{
    public class LoginListModel
    {
        public List<LoginList> LoginListCollection { get; set; }
        public LoginList LoginListDetails { get; set; }
    
    }
    public class LoginList
    {
        public string Uid { get; set; }
        public string Pass { get; set; }
        public string Type { get; set; }
    }
}