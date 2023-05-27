using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.eServicesPortal.Users.Command
{
    public class CreateUserCommand
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
