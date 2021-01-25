/*********************************************************************
 * 
 * JeopardyMVC - PlayController class
 * 
 * Controls the following actions:
 *     /Play/    - Accesses all the categories, questions and answers 
 *                 from the database marked as 'active' and passes them to 
 *                 the browser. Game play is then controlled in JavaScript.
 * 
 *********************************************************************/

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JeopardyLibrary.EF;
using JeopardyLibrary.Models;
using JeopardyMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace JeopardyMVC.Controllers
{
    public class PlayController : Controller
    {

        private readonly JeopardyContext _context;

        public PlayController(JeopardyContext context)
        {
            _context = context;
        }

        // GET: Play
        public async Task<IActionResult> Index()
        {
 
            var answers = _context.Answers.Include(a => a.Question).Include(a => a.Question.Categories)
                .Where(a=>a.Question.Categories.Active == true).OrderBy(a => a.Question.Categories.Name).ThenBy(a => a.Question.PointValue);

             return View(await answers.ToListAsync());
        }
    }
}
