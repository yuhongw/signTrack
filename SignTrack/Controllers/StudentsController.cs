using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SignTrack;
using SignTrack.Models;
using SignTrack.Services;
using Microsoft.Extensions.Logging;

namespace SignTrack.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SignContext _context;
        

        public StudentsController(SignContext context)
        {
            _context = context;
            //_logger = logger;
        }

        

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        /*
        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,PhoneId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,PhoneId")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);
            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        */

        public IActionResult SignIn()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn([Bind("Name,Phone,PhoneId,StuNo")] Student stu,[Bind("reqType")] string reqType)
        {
            var query = StudentService.QGetByExample(_context, stu);
            Student stuQ = query.FirstOrDefault();
            
            if (stuQ != null)
            {
                if (reqType == "history")
                {
                    return RedirectToAction("History", stuQ);
                }
                else if (reqType == "absence")
                {
                    return RedirectToAction("Absence", stuQ);
                }
                else
                {
                    //Add a signin record
                    stuQ.SignIns.Add(new Models.SignIn { Sign = DateTime.Now });
                    _context.SaveChanges();
                    return View("SignInResult");
                }
            }
            else
            {
                var query2 = _context.Students
                            .Where(x => x.Phone == stu.Phone || x.StuNo == stu.StuNo);
                if (query2.Count() > 0)
                {
                    return View("Mismatch");
                }
                else
                {
                    //register
                    _context.Add(stu);
                    _context.SaveChanges();
                    stuQ = StudentService.QGetByExample(_context, stu).FirstOrDefault();
                    
                    stuQ.SignIns.Add(new Models.SignIn { Sign = DateTime.Now });
                    _context.SaveChanges();
                    return View("SignInResult");
                }
            }

            

            /*
            if (ModelState.IsValid)
            {
                _context.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
            */
        }

        public IActionResult History(Student stu)
        {
            ViewBag.stu = stu;
            _context.Entry(stu).Reload();
            _context.Entry(stu).Collection(x => x.SignIns).Load();
            //var stuQ = _context.Students.Include("SignIns").Where(x => x.Id == stu.Id).SingleOrDefault();
            return View( stu.SignIns.ToList());
        }

        public IActionResult Absence(Student stu)
        {
            ViewBag.stu = stu;
            return View(new List<DateTime>());
        }
    }
}
