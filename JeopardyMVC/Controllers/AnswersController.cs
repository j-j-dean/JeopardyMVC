/*********************************************************************
 * 
 * JeopardyMVC - AnswersController class
 * 
 * Controls the following actions:
 *     /Answers/          - Display all answers
 *     /Answers/2         - Display answers for question with id=2
 *     /Answers/Details/2 - Display answer details for answer with id=2
 *     /Answers/Create    - Create a new answer (not currently a choice)
 *     /Answers/Edit/2    - Display answer details to edit for answer with id=2
 *     /Answers/Delete/2  - Delete answer with id=2 (not currently a choice)
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
    public class AnswersController : Controller
    {
        private readonly JeopardyContext _context;

        public AnswersController(JeopardyContext context)
        {
            _context = context;
        }

        // GET: Answers
        // Get: Answers/2
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                var jeopardyContext = _context.Answers
                    .Include(a => a.Question)
                    .OrderBy(a => a.QuestionId);

                ViewBag.TypeOfData = "Many";

                return View(await jeopardyContext.ToListAsync());

             } 
            else
            {
                var jeopardyContext = _context.Answers
                    .Include(q=>q.Question)
                    .Where(q => q.QuestionId == id);

                ViewBag.TypeOfData = "One";

                return View(await jeopardyContext.ToListAsync());

            }
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            //ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id");
            ViewData["QuestionName"] = new SelectList(_context.Questions, "Id", "Question");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionId,Answer,CorrectAnswer,Id")] Answers answers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", answers.QuestionId);
            ViewData["QuestionName"] = new SelectList(_context.Questions, "Id", "Question", answers.QuestionId);
            return View(answers);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers.FindAsync(id);
            if (answers == null)
            {
                return NotFound();
            }
            //ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", answers.QuestionId);
            ViewData["QuestionName"] = new SelectList(_context.Questions, "Id", "Question", answers.QuestionId);
            return View(answers);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionId,Answer,CorrectAnswer,Id")] Answers answers)
        {
            if (id != answers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswersExists(answers.Id))
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
            //ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", answers.QuestionId);
            ViewData["QuestionName"] = new SelectList(_context.Questions, "Id", "Question", answers.QuestionId);
            return View(answers);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answers = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(answers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswersExists(int id)
        {
            return _context.Answers.Any(e => e.Id == id);
        }
    }
}
