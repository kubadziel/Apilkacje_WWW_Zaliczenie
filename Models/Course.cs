using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zaliczenie.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Semester { get; set; }
        public string SignedUpByGuid { get; set; }
        public string LedByGuid { get; set; }
        public virtual IList<Exam> Exams { get; set; }    
        [ForeignKey("SignedUpByGuid")]
        public virtual Student SignedUpBy { get; set; } 
        [ForeignKey("LedByGuid")]
        public virtual Professor LedBy { get; set; } 
    }
}