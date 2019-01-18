using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ETicket.Data;
using ETicket.Models;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;

namespace ETicket.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index(string filter, string categoryFilter, string countryFilter, int page = 1, string sortExpression = "EventName")
        {
            var qry = _context.Events
                .Include(t=>t.Tickets)
                .Include(p=>p.Place)
                    .ThenInclude(c=>c.City)
                        .ThenInclude(c=>c.Country)
                .Include(t=>t.Tour)
                    .ThenInclude(p=>p.Performer)
                        .ThenInclude(p=>p.PerformerCategory)
                .Include(p=>p.PhotoEvents)
                    .ThenInclude(p=>p.Photo)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                if (string.IsNullOrWhiteSpace(categoryFilter))
                {
                    qry = qry.Where(p => p.EventName.Contains(filter));
                }
                else
                {
                    qry = qry.Where(p => p.EventName.Contains(filter)).Where(p => p.Tour.Performer.PerformerCategory.PerformerCategoryId == Convert.ToInt32(categoryFilter));
                }

                if (string.IsNullOrWhiteSpace(countryFilter))
                {
                    qry = qry.Where(p => p.EventName.Contains(filter));
                }
                else
                {
                    qry = qry.Where(p => p.EventName.Contains(filter)).Where(p => p.Place.City.Country.CountryId == Convert.ToInt32(countryFilter));
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(categoryFilter))
                {
                    qry = qry.Where(p => p.Tour.Performer.PerformerCategory.PerformerCategoryId == Convert.ToInt32(categoryFilter));
                }
                if (!string.IsNullOrWhiteSpace(countryFilter))
                {
                    qry = qry.Where(p => p.Place.City.Country.CountryId == Convert.ToInt32(countryFilter));
                }
            }

            var model = await PagingList.CreateAsync(qry, 10, page, sortExpression, "EventName");

            model.RouteValue = new RouteValueDictionary {
                { "filter", filter},
                { "categoryFilter", categoryFilter },
                { "countryFilter", countryFilter },
                { "sortExpression", sortExpression }
            };

            ViewBag.ListOfCategories = _context.PerformerCategories.OrderBy(c => c.PerformerCategoryName).ToList();
            ViewBag.ListOfCountries = _context.Countries.OrderBy(c => c.CountryName).ToList();

            return View(model);
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _context.Events.Where(c => c.EventName.Contains(term)).Select(e=>e.EventName).ToList();

                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .Include(e => e.Place)
                    .ThenInclude(c=>c.City)
                        .ThenInclude(c=>c.Country)
                .Include(e => e.Tour)
                    .ThenInclude(p=>p.Performer)
                .Include(t=>t.Tickets)
                .Include(p => p.PhotoEvents)
                    .ThenInclude(p => p.Photo)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceAddress");
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "TourName");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,EventDescription,EventStart,EventEnd,EventTicketPurchaseLimit,PlaceId,TourId")] Events events)
        {
            if (ModelState.IsValid)
            {
                _context.Add(events);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceAddress", events.PlaceId);
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "TourName", events.TourId);
            return View(events);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events.FindAsync(id);
            if (events == null)
            {
                return NotFound();
            }
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceAddress", events.PlaceId);
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "TourName", events.TourId);
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,EventDescription,EventStart,EventEnd,EventTicketPurchaseLimit,PlaceId,TourId")] Events events)
        {
            if (id != events.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(events);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsExists(events.EventId))
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
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceAddress", events.PlaceId);
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "TourName", events.TourId);
            return View(events);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .Include(e => e.Place)
                .Include(e => e.Tour)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var events = await _context.Events.FindAsync(id);
            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
