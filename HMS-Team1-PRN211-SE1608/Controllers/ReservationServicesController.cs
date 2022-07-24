using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HMS_Team1_PRN211_SE1608.Models;

namespace HMS_Team1_PRN211_SE1608.Controllers
{
    public class ReservationServicesController : Controller
    {
        private readonly PRN211_HMSContext _context;

        public ReservationServicesController(PRN211_HMSContext context)
        {
            _context = context;
        }

        // GET: ReservationServices
        public async Task<IActionResult> Index()
        {
            var pRN211_HMSContext = _context.ReservationServices.Include(r => r.Reservation).Include(r => r.Service);
            return View(await pRN211_HMSContext.ToListAsync());
        }

        // GET: ReservationServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationService = await _context.ReservationServices
                .Include(r => r.Reservation)
                .Include(r => r.Service)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservationService == null)
            {
                return NotFound();
            }

            return View(reservationService);
        }

        // GET: ReservationServices/Create
        public IActionResult Create()
        {
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "ReservationId");
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName");
            return View();
        }

        // POST: ReservationServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,ServiceId,Price,BookedDate,Quantity")] ReservationService reservationService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservationService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "ReservationId", reservationService.ReservationId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName", reservationService.ServiceId);
            return View(reservationService);
        }

        // GET: ReservationServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationService = await _context.ReservationServices.FindAsync(id);
            if (reservationService == null)
            {
                return NotFound();
            }
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "ReservationId", reservationService.ReservationId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName", reservationService.ServiceId);
            return View(reservationService);
        }

        // POST: ReservationServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,ServiceId,Price,BookedDate,Quantity")] ReservationService reservationService)
        {
            if (id != reservationService.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationServiceExists(reservationService.ReservationId))
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
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "ReservationId", reservationService.ReservationId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceName", reservationService.ServiceId);
            return View(reservationService);
        }

        // GET: ReservationServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationService = await _context.ReservationServices
                .Include(r => r.Reservation)
                .Include(r => r.Service)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservationService == null)
            {
                return NotFound();
            }

            return View(reservationService);
        }

        // POST: ReservationServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationService = await _context.ReservationServices.FindAsync(id);
            _context.ReservationServices.Remove(reservationService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationServiceExists(int id)
        {
            return _context.ReservationServices.Any(e => e.ReservationId == id);
        }
    }
}
