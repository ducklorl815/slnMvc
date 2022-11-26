using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjMvc.Models
{
    public class CCustomer
    {
        public int fid { get; set; }
        public string fName { get; set; }
        public string fPhone { get; set; }
        public string fEmail { get; set; }
        public string fAddress { get; set; }
        public string fPassword { get; set; }
        public bool fActived { get; set; }

    }
}