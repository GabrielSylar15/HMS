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
    public class ReservationsController : Controller
    {
        private readonly PRN211_HMSContext _context;

        public ReservationsController(PRN211_HMSContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index(string RoomName, DateTime? StartDate, DateTime? EndDate, string NameCustomer, string Phone, float? Price, int page)
        {
            if (Price == 0)
            {
                Price = null;
            }
            if (page <= 0)
            {
                page = 1;
            }
            int pageSize = 5;
            int offSet = (page - 1) * pageSize + 1;
            // Searching and paging
            var reservations = _context.Reservations.Include(r => r.Account).Include(r => r.Room)
                 .Where(x =>
                 x.Room.RoomName.Contains(RoomName ?? x.Room.RoomName) &&
                 x.StartDate >= (StartDate ?? x.StartDate) &&
                 x.EndDate <= (EndDate ?? x.EndDate) &&
                 x.Account.DisplayName.Contains(NameCustomer ?? x.Account.DisplayName) &&
                 x.Account.Phone.Contains(Phone ?? x.Account.Phone) &&
                 x.Price == (Price ?? x.Price)
                 ).Skip(offSet - 1).Take(pageSize);
            // Hiện thị lịch sử search
            ViewData["RoomName"] = RoomName;
            if (StartDate == null || StartDate.Equals("")) ViewData["StartDate"] = StartDate;
            else ViewData["StartDate"] = StartDate.Value.ToString("yyyy-MM-dd");
            if (EndDate == null || EndDate.Equals("")) ViewData["EndDate"] = EndDate;
            else ViewData["EndDate"] = EndDate.Value.ToString("yyyy-MM-dd");
            ViewData["NameCustomer"] = NameCustomer;
            ViewData["Phone"] = Phone;
            ViewData["Price"] = Price;
            // Phân trang 
            // Count tổng số trang 
            int count = _context.Reservations.Include(r => r.Account).Include(r => r.Room)
                 .Where(x =>
                 x.Room.RoomName.Contains(RoomName ?? x.Room.RoomName) &&
                 x.StartDate >= (StartDate ?? x.StartDate) &&
                 x.EndDate <= (EndDate ?? x.EndDate) &&
                 x.Account.DisplayName.Contains(NameCustomer ?? x.Account.DisplayName) &&
                 x.Account.Phone.Contains(Phone ?? x.Account.Phone) &&
                 x.Price == (Price ?? x.Price)
                 ).ToList().Count;
            // Lấy ra dữ liệu để hiện thị thành pager
            int totalPage = (count % pageSize == 0) ? (count / pageSize) : (count / pageSize) + 1;
            // Lấy Url để phân trang cho avancedSearch 
            string url = "/Reservations/Index?";
            string url_param = Request.QueryString.ToString();
            if (url_param != null && url_param.Length > 0)
            {
                url = "/Reservations/Index";
                if (url_param.EndsWith("page=" + page))
                {
                    url_param = url_param.Replace("page=" + page, "");
                }
                // nếu nó không rời vào trường hợp book?page=x và thiếu & thì thêm vào
                if (!url_param.Equals("") && !url_param.EndsWith("&") && !url_param.EndsWith("?"))
                {
                    url_param += "&";
                }
                url += (url_param);
            }
            ViewData["url"] = url;
            ViewData["totalPage"] = totalPage;
            ViewData["pageIndex"] = page;

            var pRN211_HMSContext = _context.Reservations.Include(r => r.Account).Include(r => r.Room);
            return View(await reservations.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Account)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Address");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,RoomId,StartDate,Price,EndDate,ReservationId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Address", reservation.AccountId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomName", reservation.RoomId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Address", reservation.AccountId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomName", reservation.RoomId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,RoomId,StartDate,Price,EndDate,ReservationId")] Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationId))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Address", reservation.AccountId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomName", reservation.RoomId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Account)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }
        // Checkout 
        public async Task<IActionResult> Checkout(int id)
        {
            Reservation reservation = _context.Reservations.FirstOrDefault(r => r.ReservationId == id);
            reservation.EndDate = DateTime.Now;
            if (reservation.StartDate > DateTime.Now)
            {
                reservation.StartDate = DateTime.Now;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}