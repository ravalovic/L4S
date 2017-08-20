using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace WebPortal.Models
{
    public static class WebRoles
    {
        public static string User = "User";
        public static string Admin = "Admin";
        public static string Ekonom = "Ekonóm";
        //public static string SuperUser = "SuperUser";

        public static List<string> All
        {
            get
            {
                List<string> _all = new List<string>();
                _all.Add(WebRoles.Admin);
                _all.Add(WebRoles.User);
                _all.Add(WebRoles.Ekonom);
                // _all.Add(WebRoles.SuperUser);
                return _all;
            }
        }
    }
}