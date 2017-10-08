using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ContactViewModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Address { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public double Lat { set; get; }
        public double Lng { set; get; }
        public string Content { set; get; }
        public bool Status { set; get; }
    }
}