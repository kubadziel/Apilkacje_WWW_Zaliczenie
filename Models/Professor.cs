using System;
using Microsoft.AspNetCore.Identity;

namespace Zaliczenie.Models
{
    public class Professor : IdentityUser
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
    }
}