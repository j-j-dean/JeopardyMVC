/*********************************************************************
 * 
 * JeopardyMVC - CategoriesController class
 * 
 * Controls the following actions:
 *     /Categories/          - Display all categories
 *     /Categories/Details/2 - Display category details for category with id=2
 *     /Categories/Create    - Create a new category
 *     /Categories/Edit/2    - Display category details to edit for category with id=2
 *     /Categories/Delete/2  - Delete category with id=2
 *     
 * Note:
 * Create will create questions and answers automatically.  Those questions
 * and answers can then be edited.  Delete will delete the category and the
 * associated questions and answers.
 * 
 *********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JeopardyLibrary.EF;
using JeopardyLibrary.Models;

namespace JeopardyMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly JeopardyContext _context;

        public CategoriesController(JeopardyContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,Active")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categories);
                await _context.SaveChangesAsync();

                var questions = new List<Questions>
                {
                    new Questions { Question = "Edit and Enter Question 1", PointValue = 100, Categories = categories },
                    new Questions { Question = "Edit and Enter Question 2", PointValue = 200, Categories = categories },
                    new Questions { Question = "Edit and Enter Question 3", PointValue = 300, Categories = categories },
                    new Questions { Question = "Edit and Enter Question 4", PointValue = 400, Categories = categories },
                    new Questions { Question = "Edit and Enter Question 5", PointValue = 500, Categories = categories }
                };
                _context.Questions.AddRange(questions);
                await _context.SaveChangesAsync();

                var answers = new List<Answers>
                {
                    new Answers { Answer = "Edit and Enter Answer a", Question = questions[0], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer b", Question = questions[0], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer c", Question = questions[0], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer d", Question = questions[0], CorrectAnswer=false },

                    new Answers { Answer = "Edit and Enter Answer a", Question = questions[1], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer b", Question = questions[1], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer c", Question = questions[1], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer d", Question = questions[1], CorrectAnswer=false },

                    new Answers { Answer = "Edit and Enter Answer a", Question = questions[2], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer b", Question = questions[2], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer c", Question = questions[2], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer d", Question = questions[2], CorrectAnswer=false },

                    new Answers { Answer = "Edit and Enter Answer a", Question = questions[3], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer b", Question = questions[3], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer c", Question = questions[3], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer d", Question = questions[3], CorrectAnswer=false },

                    new Answers { Answer = "Edit and Enter Answer a", Question = questions[4], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer b", Question = questions[4], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer c", Question = questions[4], CorrectAnswer=false },
                    new Answers { Answer = "Edit and Enter Answer d", Question = questions[4], CorrectAnswer=false }
                };
                _context.Answers.AddRange(answers);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Active,Id")] Categories categories)
        {
            if (id != categories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesExists(categories.Id))
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
            return View(categories);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
