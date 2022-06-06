using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zaliczenie.Models;
using Zaliczenie.Data;
using Microsoft.AspNetCore.Identity;

namespace Zaliczenie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext db;
        private UserManager<IdentityUser> userManger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<IdentityUser> userManger)
        {
            _logger = logger;
            this.db = db;
            this.userManger = userManger;
        }

        public IActionResult SeedData(int? arg1, int? arg2){
            if(db.Users.FirstOrDefault(u=>u.Id == "001") == null)
                userManger.CreateAsync(new Student() { 
                    Id = "001", UserName = "student1@zaliczenie.com", 
                    EmailConfirmed = true }, "Zaliczenie1!").Wait();
            if(db.Users.FirstOrDefault(u=>u.Id == "002") == null)
                userManger.CreateAsync(new Student() { 
                    Id = "002", UserName = "student2@zaliczenie.com", 
                    EmailConfirmed = true }, "Zaliczenie1!").Wait();     
            if(db.Users.FirstOrDefault(u=>u.Id == "003") == null)
                userManger.CreateAsync(new Student() { 
                    Id = "003", UserName = "student3@zaliczenie.com", 
                    EmailConfirmed = true }, "Zaliczenie1!").Wait();
            if(db.Users.FirstOrDefault(u=>u.Id == "004") == null)
                userManger.CreateAsync(new Student() { 
                    Id = "004", UserName = "student4@zaliczenie.com", 
                    EmailConfirmed = true }, "Zaliczenie1!").Wait();

            if(db.Users.FirstOrDefault(u=>u.Id == "005") == null)
                userManger.CreateAsync(new Professor() { 
                    Id = "005", UserName = "professor1@zaliczenie.com", 
                    EmailConfirmed = true }, "Zaliczenie1!").Wait();
            if(db.Users.FirstOrDefault(u=>u.Id == "006") == null)
                userManger.CreateAsync(new Professor() { 
                    Id = "006", UserName = "professor2@zaliczenie.com", 
                    EmailConfirmed = true }, "Zaliczenie1!").Wait();       
            if(db.Users.FirstOrDefault(u=>u.Id == "007") == null)
                userManger.CreateAsync(new Professor { 
                    Id = "007", UserName = "professor3@zaliczenie.com", 
                    EmailConfirmed = true }, "Zaliczenie1!").Wait();
            if(db.Users.FirstOrDefault(u=>u.Id == "008") == null)
                userManger.CreateAsync(new Professor { 
                    Id = "008", UserName = "professor4@zaliczenie.com", 
                    EmailConfirmed = true }, "Zaliczenie1!").Wait();

            if(db.Courses.FirstOrDefault(u=>u.Id == 1) == null){
                db.Add( new Course(){ Id = 1, Name = "Aplikacje WWW", Date = DateTime.Now, 
                    Semester = 6, SignedUpByGuid = "001", LedByGuid = "005",
                    Exams = new List<Exam>(){
                        new Exam(){Id = 1, Name = "Egzamin 1", 
                            Attempt = 1, Grade = 5.0f},
                        new Exam(){Id = 2, Name = "Egzamin 2", 
                            Attempt = 1, Grade = 5.0f},
                    }                    
                });
                
                db.SaveChanges();
            }
            
            if(db.Courses.FirstOrDefault(u=>u.Id == 2) == null){
                db.Add( new Course(){ Id = 2, Name = "Angielski", Date = DateTime.Now, 
                    Semester = 6, SignedUpByGuid = "002", LedByGuid = "007",
                    Exams = new List<Exam>(){
                        new Exam(){Id = 3, Name = "Egzamin 1", 
                            Attempt = 1, Grade = 5.0f},
                    }                    
            });

                db.SaveChanges();
            } 

            return this.RedirectToAction("Index", "Student");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
