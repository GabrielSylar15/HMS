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
    public class SizeRoomsController : Controller
    {
        private readonly PRN211_HMSContext _context;

        public SizeRoomsController(PRN211_HMSContext context)
        {
            _context = context;
        }

        // GET: SizeRooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.SizeRooms.ToListAsync());
        }

        // GET: SizeRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizeRoom = await _context.SizeRooms
                .FirstOrDefaultAsync(m => m.SizeId == id);
            if (sizeRoom == null)
            {
                return NotFound();
            }

            return View(sizeRoom);
        }

        // GET: SizeRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SizeRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SizeId,Size,Price")] SizeRoom sizeRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sizeRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sizeRoom);
        }

        // GET: SizeRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizeRoom = await _context.SizeRooms.FindAsync(id);
            if (sizeRoom == null)
            {
                return NotFound();
            }
            return View(sizeRoom);
        }

        // POST: SizeRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SizeId,Size,Price")] SizeRoom sizeRoom)
        {
            if (id != sizeRoom.SizeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sizeRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SizeRoomExists(sizeRoom.SizeId))
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
            return View(sizeRoom);
        }

        // GET: SizeRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizeRoom = await _context.SizeRooms
                .FirstOrDefaultAsync(m => m.SizeId == id);
            if (sizeRoom == null)
            {
                return NotFound();
            }

            return View(sizeRoom);
        }

        // POST: SizeRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sizeRoom = await _context.SizeRooms.FindAsync(id);
            _context.SizeRooms.Remove(sizeRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SizeRoomExists(int id)
        {
            return _context.SizeRooms.Any(e => e.SizeId == id);
        }
    }
}
