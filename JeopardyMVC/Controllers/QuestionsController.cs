/*********************************************************************
 * 
 * JeopardyMVC - QuestionsController class
 * 
 * Controls the following actions:
 *     /Questions/          - Display all questions
 *     /Questions/2         - Display questions with id=2
 *     /Questions/Details/2 - Display questions details for question with id=2
 *     /Questions/Create    - Create a new question (not currently a choice)
 *     /Questions/Edit/2    - Display question details to edit for question with id=2
 *     /Questions/Delete/2  - Delete question with id=2 (not currently a choice)
 *     
 * Note:
 * Create and Delete occur when a new category is created to ensure the right
 * number of questions and answers are created.
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
    public class QuestionsController : Controller
    {
        private readonly JeopardyContext _context;

        public QuestionsController(JeopardyContext context)
        {
            _context = context;
        }

        // GET: Questions
        // GET: Questions/1
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                var jeopardyContext = _context.Questions
                    .Include(q => q.Categories)
                    .OrderBy(q => q.CategoryId)
                    .ThenBy(q => q.PointValue);

                ViewBag.TypeOfData = "Many";

                return View(await jeopardyContext.ToListAsync());
            }
            else
            {
                var jeopardyContext = _context.Questions
                    .Include(q => q.Categories)
                    .Where(q => q.CategoryId == id);

                ViewBag.TypeOfData = "One";

                return View(await jeopardyContext.ToListAsync());
            }
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions
                .Include(q => q.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["CategoryName"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Question,PointValue,Id")] Questions questions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", questions.CategoryId);
            ViewData["CategoryName"] = new SelectList(_context.Categories, "Id", "Name", questions.CategoryId);
            return View(questions);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", questions.CategoryId);
            ViewData["CategoryName"] = new SelectList(_context.Categories, "Id", "Name", questions.CategoryId);
            return View(questions);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Question,PointValue,Id")] Questions questions)
        {
            if (id != questions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionsExists(questions.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", questions.CategoryId);
            return View(questions);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions
                .Include(q => q.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questions = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionsExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
