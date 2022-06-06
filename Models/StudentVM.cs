using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Zaliczenie.Models
{
    public class StudentVM
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
        [Required]
        [Display(Name = "Student Name")]
        public string FullName { get; set; }
        [Required]
         public string Major { get; set; }
        [Required]
        public int GradeBookId { get; set; } 
    }
}