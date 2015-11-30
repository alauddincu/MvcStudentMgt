using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcStudnetMgt6October.Models
{
    public class ChangePassListModelss
    {
        public List<ChangePassList> LoginListCollection { get; set; }
        public ChangePassList LoginListDetails { get; set; }
    }

    public class ChangePassList
    {
        public string UserMail { get; set; }

        public string UserType { get; set; }

        public string PassPre { get; set; }
        public string NewPass1 { get; set; }
        public string NewPass2 { get; set; }
    }
}