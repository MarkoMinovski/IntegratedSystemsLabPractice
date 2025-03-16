using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConcertPlanner.Data;
using ConcertPlanner.Models;
using Microsoft.AspNetCore.Identity;
using ConcertPlanner.Areas.Identity;

namespace ConcertPlanner.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<LabOneUser> _userManagerLocal;

        public TicketsController(ApplicationDbContext context, UserManager<LabOneUser> userManager)
        {
            _context = context;
            _userManagerLocal = userManager;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tickets.Where(t => t.PurchaserID == this._userManagerLocal.GetUserId(HttpContext.User));
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Concert)
                .Include(t => t.Purchaser)
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["ConcertGuid"] = new SelectList(_context.Concerts, "Guid", "ConcertName");
            ViewData["PurchaserID"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,NumberOfPeople,ConcertGuid,PurchaserID")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Guid = Guid.NewGuid();
                ticket.Purchaser = _context.Users.FirstOrDefault(u => u.Id == ticket.PurchaserID);
                ticket.Concert = _context.Concerts.FirstOrDefault(c => c.Guid == ticket.ConcertGuid);
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } else
            {
                Console.WriteLine("Model state invalid because: ");
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            ViewData["ConcertGuid"] = new SelectList(_context.Concerts, "Guid", "ConcertName", ticket.ConcertGuid);
            ViewData["PurchaserID"] = new SelectList(_context.Users, "Id", "Id", ticket.PurchaserID);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["ConcertGuid"] = new SelectList(_context.Concerts, "Guid", "ConcertName", ticket.ConcertGuid);
            ViewData["PurchaserID"] = new SelectList(_context.Users, "Id", "Id", ticket.PurchaserID);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guid,NumberOfPeople,ConcertGuid,PurchaserID")] Ticket ticket)
        {
            if (id != ticket.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Guid))
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
            ViewData["ConcertGuid"] = new SelectList(_context.Concerts, "Guid", "ConcertName", ticket.ConcertGuid);
            ViewData["PurchaserID"] = new SelectList(_context.Users, "Id", "Id", ticket.PurchaserID);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Concert)
                .Include(t => t.Purchaser)
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id)
        {
            return _context.Tickets.Any(e => e.Guid == id);
        }
    }
}
