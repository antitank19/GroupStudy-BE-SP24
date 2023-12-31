using ShareResource.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.APIModel
{
    public class LoginModel
    {
        public string UsernameOrEmail { get; set; }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value.CustomHash(); }
        }
        //public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
