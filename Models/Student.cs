using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Zaliczenie.Models
{
    public class Student : IdentityUser
    {
        public string FullName { get; set; }
        public string Major { get; set; }
        public int GradeBookId { get; set; }
        public virtual IList<Course> Courses { get; set; } 
    }
}