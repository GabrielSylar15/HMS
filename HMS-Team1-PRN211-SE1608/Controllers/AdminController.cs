﻿using HMS_Team1_PRN211_SE1608.Logic;
using HMS_Team1_PRN211_SE1608.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_Team1_PRN211_SE1608.Controllers
{
    public class AdminController : Controller
    {
        private readonly PRN211_HMSContext _context;

        public AdminController(PRN211_HMSContext context)
        {
            _context = context;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult RoomList(string name, int? price, int? size, int? sort, int page)
        {
            DateTime? fromdate = null;
            DateTime? todate = null;

            var sizeRoom = _context.SizeRooms.ToList<SizeRoom>();
            ViewBag.sizeRoom = sizeRoom;

            if (page <= 0) page = 1;


            List<Room> rooms;

            RoomManager roomManager = new RoomManager();
            if (page <= 0)
            {
                page = 1;
            }
            int pageSize = 2;
            int offSet = (page - 1) * pageSize + 1;

            int count = roomManager.countRoom(name, fromdate, todate, size, price);


            int totalPage = (count % pageSize == 0) ? (count / pageSize) : (count / pageSize) + 1;
            rooms = roomManager.AvancedSearchByRoomAndPaging(name, fromdate, todate, size, price, sort, offSet, pageSize);

            string url = "/Admin/RoomList?";
            string url_param = Request.QueryString.ToString();
            if (url_param != null && url_param.Length > 0)
            {
                url = "/Admin/RoomList";
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
            ViewBag.name = name;
            if (fromdate != null)
                ViewBag.fromdate = fromdate.Value.ToString("yyyy-MM-dd");
            else ViewBag.fromdate = fromdate;
            if (todate != null)
                ViewBag.todate = todate.Value.ToString("yyyy-MM-dd");
            else ViewBag.todate = todate;

            ViewBag.price = price;
            ViewBag.sizer = size;
            ViewBag.sort = sort;
            ViewBag.page = page;
            ViewBag.totalPage = totalPage;
            ViewBag.url = url;
            ViewBag.sort = sort;
            return View(rooms);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Size)
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            ViewData["SizeId"] = new SelectList(_context.SizeRooms, "SizeId", "SizeId");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,RoomName,SizeId,RoomDescription")] Room room, IFormFile image)
        {
            if (ModelState.IsValid)
            {

                //copy file
                DateTimeOffset now = DateTimeOffset.UtcNow;
                long unixTimeMilliseconds = now.ToUnixTimeMilliseconds();
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MyFiles", unixTimeMilliseconds + "_room" + image.FileName.Substring(image.FileName.IndexOf('.')));
                //copy file
                using (var file = new FileStream(fullPath, FileMode.Create))
                {
                    image.CopyTo(file);
                }
                room.RoomImage = unixTimeMilliseconds + "_room" + image.FileName.Substring(image.FileName.IndexOf('.'));
                _context.Add(room);
                await _context.SaveChangesAsync();
                return Redirect("/Admin/RoomList");
            }
            ViewData["SizeId"] = new SelectList(_context.SizeRooms, "SizeId", "SizeId", room.SizeId);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["SizeId"] = new SelectList(_context.SizeRooms, "SizeId", "SizeId", room.SizeId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,RoomName,SizeId,RoomDescription")] Room room, IFormFile image)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            long unixTimeMilliseconds = now.ToUnixTimeMilliseconds();
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MyFiles", unixTimeMilliseconds + "_room" + image.FileName.Substring(image.FileName.IndexOf('.')));
            //copy file
            using (var file = new FileStream(fullPath, FileMode.Create))
            {
                image.CopyTo(file);
            }
            room.RoomImage = unixTimeMilliseconds + "_room" + image.FileName.Substring(image.FileName.IndexOf('.'));
            if (id != room.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.RoomId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Admin/Details/" + id);
            }
            ViewData["SizeId"] = new SelectList(_context.SizeRooms, "SizeId", "SizeId", room.SizeId);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Size)
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }
    }
}

