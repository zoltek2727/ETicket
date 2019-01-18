using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ETicket.Data;
using ETicket.Models;

namespace ETicket.Controllers
{
    public class PerformerCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerformerCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PerformerCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.PerformerCategories.ToListAsync());
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _context.PerformerCategories.Where(c => c.PerformerCategoryName.Contains(term)).OrderBy(c => c.PerformerCategoryName).Select(c => new { c.PerformerCategoryId, c.PerformerCategoryName }).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: PerformerCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performerCategories = await _context.PerformerCategories
                .FirstOrDefaultAsync(m => m.PerformerCategoryId == id);
            if (performerCategories == null)
            {
                return NotFound();
            }

            return View(performerCategories);
        }

        // GET: PerformerCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PerformerCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PerformerCategoryId,PerformerCategoryName")] PerformerCategories performerCategories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(performerCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(performerCategories);
        }

        // GET: PerformerCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performerCategories = await _context.PerformerCategories.FindAsync(id);
            if (performerCategories == null)
            {
                return NotFound();
            }
            return View(performerCategories);
        }

        // POST: PerformerCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PerformerCategoryId,PerformerCategoryName")] PerformerCategories performerCategories)
        {
            if (id != performerCategories.PerformerCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performerCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformerCategoriesExists(performerCategories.PerformerCategoryId))
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
            return View(performerCategories);
        }

        // GET: PerformerCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performerCategories = await _context.PerformerCategories
                .FirstOrDefaultAsync(m => m.PerformerCategoryId == id);
            if (performerCategories == null)
            {
                return NotFound();
            }

            return View(performerCategories);
        }

        // POST: PerformerCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performerCategories = await _context.PerformerCategories.FindAsync(id);
            _context.PerformerCategories.Remove(performerCategories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformerCategoriesExists(int id)
        {
            return _context.PerformerCategories.Any(e => e.PerformerCategoryId == id);
        }
    }
}
