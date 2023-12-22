using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentGraph
{
    public static class DataAutorization
    {
        private static string password;
        private static string login;

        public static string Password
        {
            get {  return password; }  
            
            set { password = value; }
        }

        public static string Login
        {
            get {  return login;  }

            set {  login = value; }
        }
    }
}
