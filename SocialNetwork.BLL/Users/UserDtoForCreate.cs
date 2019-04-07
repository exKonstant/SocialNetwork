using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.BLL.Users
{
    public class UserDtoForCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Info { get; set; }
    }
}

