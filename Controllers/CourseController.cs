using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zaliczenie.Data;
using Zaliczenie.Models;

namespace Zaliczenie.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Course
        public async Task<IActionResult> Index(string StudentId)
        {
            if(StudentId != null){
                var student = await _context.Users
                    .FirstOrDefaultAsync(u=>u.Id == StudentId);
                if(student == null)
                    return NotFound();
                ViewData["student_id"] = StudentId;
                ViewData["student_name"] = student.UserName;    

                return View(await _context.Courses
                .Where(i=>i.SignedUpByGuid == StudentId)
                .Include(i => i.LedBy).Include(i => i.SignedUpBy)
                .ToListAsync());     
            }

            var applicationDbContext = _context.Courses
                .Include(i => i.LedBy).Include(i => i.SignedUpBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.LedBy)
                .Include(c => c.SignedUpBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index","Exam", new {course_id = course.Id});
        }

        // GET: Course/Create
        public IActionResult Create(string student_id)
        {
            var prof = _context.Set<Professor>()
                .FirstOrDefault();
            var stud = _context.Set<Student>()
                .FirstOrDefault(u=>u.Id == student_id);
            if(stud == null || prof == null)
                return NotFound();
            return View(new Course(){Date=DateTime.Now, LedBy = prof, SignedUpBy = stud, LedByGuid = prof.Id, SignedUpByGuid = stud.Id});
        }

        // POST: Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date,Semester,SignedUpByGuid,LedByGuid")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LedByGuid"] = new SelectList(_context.Professor, "Id", "Id", course.LedByGuid);
            ViewData["SignedUpByGuid"] = new SelectList(_context.Student, "Id", "Id", course.SignedUpByGuid);
            return View(course);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["LedByGuid"] = new SelectList(_context.Professor, "Id", "Id", course.LedByGuid);
            ViewData["SignedUpByGuid"] = new SelectList(_context.Student, "Id", "Id", course.SignedUpByGuid);
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date,Semester,SignedUpByGuid,LedByGuid")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LedByGuid"] = new SelectList(_context.Professor, "Id", "Id", course.LedByGuid);
            ViewData["SignedUpByGuid"] = new SelectList(_context.Student, "Id", "Id", course.SignedUpByGuid);
            return View(course);
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.LedBy)
                .Include(c => c.SignedUpBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
