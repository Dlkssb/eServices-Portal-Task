using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Domain.eServicesPortal.Entities
{

    public class User:IdentityUser
    {
        
        public string Name { get; private set; }

        public string Mobile { get; private set; }

        // Constructor for creating a new user
        public User(string name, string email, string mobile)
        {
            Name = name;
            Email = email;
            Mobile = mobile;
        }

       
        // Method for updating user information
        public void UpdateInfo(string name, string email, string mobile,string Username, string Securitystamp)
        {
            Name = name;
            Email = email;
            Mobile = mobile;
            UserName = Username;
            SecurityStamp = Securitystamp;
        }
    }
}
