using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ETicket.Data;
using ETicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;

namespace ETicket.Controllers
{
    [Authorize]
    public class PurchasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private UserManager<IdentityUser> _userManager;

        public PurchasesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Purchases
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "Ticket.TicketName")
        {
            var id = _userManager.GetUserId(User);

            //var applicationDbContext = _context.Purchases.Where(p=>p.Id==id).Include(p => p.ApplicationUser).Include(p => p.Delivery).Include(p => p.Ticket);
            //return View(await applicationDbContext.ToListAsync());

            var qry = _context.Purchases.Where(p => p.Id == id).Include(p => p.ApplicationUser).Include(p => p.Delivery).Include(p => p.Ticket).ThenInclude(e=>e.Event).OrderBy(c => c.PurchaseTicketDate).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(c => c.Ticket.TicketName.Contains(filter));
            }

            var model = await PagingList.CreateAsync(qry, 10, page, sortExpression, "Ticket.TicketName");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter}
            };

            return View(model);
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _context.Purchases.Where(c => c.Ticket.TicketName.Contains(term)).OrderBy(c => c.PurchaseTicketDate).Select(c => new { c.PurchaseId, c.Ticket.TicketName }).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchases = await _context.Purchases
                .Include(p => p.ApplicationUser)
                .Include(p => p.Delivery)
                .Include(p => p.Ticket)
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchases == null)
            {
                return NotFound();
            }

            return View(purchases);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries, "DeliveryId", "DeliveryType");
            ViewData["TicketId"] = new SelectList(_context.Tickets, "TicketId", "TicketName");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseId,PurchaseTicketDate,PurchaseAmount,Id,TicketId,DeliveryId")] Purchases purchases)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchases);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.ApplicationUsers, "Id", "Id", purchases.Id);
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries, "DeliveryId", "DeliveryType", purchases.DeliveryId);
            ViewData["TicketId"] = new SelectList(_context.Tickets, "TicketId", "TicketName", purchases.TicketId);
            return View(purchases);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchases = await _context.Purchases.FindAsync(id);
            if (purchases == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.ApplicationUsers, "Id", "Id", purchases.Id);
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries, "DeliveryId", "DeliveryType", purchases.DeliveryId);
            ViewData["TicketId"] = new SelectList(_context.Tickets, "TicketId", "TicketName", purchases.TicketId);
            return View(purchases);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseId,PurchaseTicketDate,PurchaseAmount,Id,TicketId,DeliveryId")] Purchases purchases)
        {
            if (id != purchases.PurchaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchases);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchasesExists(purchases.PurchaseId))
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
            ViewData["Id"] = new SelectList(_context.ApplicationUsers, "Id", "Id", purchases.Id);
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries, "DeliveryId", "DeliveryType", purchases.DeliveryId);
            ViewData["TicketId"] = new SelectList(_context.Tickets, "TicketId", "TicketName", purchases.TicketId);
            return View(purchases);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchases = await _context.Purchases
                .Include(p => p.ApplicationUser)
                .Include(p => p.Delivery)
                .Include(p => p.Ticket)
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchases == null)
            {
                return NotFound();
            }

            return View(purchases);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchases = await _context.Purchases.FindAsync(id);
            _context.Purchases.Remove(purchases);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchasesExists(int id)
        {
            return _context.Purchases.Any(e => e.PurchaseId == id);
        }
    }
}
