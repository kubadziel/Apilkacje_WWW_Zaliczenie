using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zaliczenie.Models;
using Microsoft.AspNetCore.Identity;

namespace Zaliczenie.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

         protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>()
                .ToTable("AspNetUsers")
                .HasDiscriminator<string>("UserType")
                .HasValue<Student>("Student")
                .HasValue<Professor>("Professor");
        }

         public DbSet<Zaliczenie.Models.Student> Student { get; set; }

         public DbSet<Zaliczenie.Models.Professor> Professor { get; set; }
    }
}

