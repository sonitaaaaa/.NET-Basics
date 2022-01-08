using Microsoft.AspNetCore.Identity;
using System;

namespace BlogsDemoWebApi.identityAuth
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
    }
}
