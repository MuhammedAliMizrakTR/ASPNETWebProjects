using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreMVCLibraryProject.Models.Context;
using CoreMVCLibraryProject.Models.Entities;

namespace CoreMVCLibraryProject.Controllers
{
    public class IslemsController : Controller
    {
        private readonly LibraryContext _context;

        public IslemsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Islems
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Islems.Include(i => i.Kitap).Include(i => i.Ogrenci);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Islems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var islem = await _context.Islems
                .Include(i => i.Kitap)
                .Include(i => i.Ogrenci)
                .FirstOrDefaultAsync(m => m.IslemNo == id);
            if (islem == null)
            {
                return NotFound();
            }

            return View(islem);
        }

        // GET: Islems/Create
        public IActionResult Create()
        {
            ViewData["KitapNo"] = new SelectList(_context.Kitaps, "KitapNo", "KitapAdi");
            ViewData["OgrNo"] = new SelectList(_context.Ogrencis, "OgrNo", "OgrAd");
            return View();
        }

        // POST: Islems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IslemNo,OgrNo,KitapNo,ATarih,VTarih")] Islem islem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(islem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KitapNo"] = new SelectList(_context.Kitaps, "KitapNo", "KitapAdi", islem.KitapNo);
            ViewData["OgrNo"] = new SelectList(_context.Ogrencis, "OgrNo", "OgrAd", islem.OgrNo);
            return View(islem);
        }

        // GET: Islems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var islem = await _context.Islems.FindAsync(id);
            if (islem == null)
            {
                return NotFound();
            }
            ViewData["KitapNo"] = new SelectList(_context.Kitaps, "KitapNo", "KitapNo", islem.KitapNo);
            ViewData["OgrNo"] = new SelectList(_context.Ogrencis, "OgrNo", "OgrNo", islem.OgrNo);
            return View(islem);
        }

        // POST: Islems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IslemNo,OgrNo,KitapNo,ATarih,VTarih")] Islem islem)
        {
            if (id != islem.IslemNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(islem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IslemExists(islem.IslemNo))
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
            ViewData["KitapNo"] = new SelectList(_context.Kitaps, "KitapNo", "KitapNo", islem.KitapNo);
            ViewData["OgrNo"] = new SelectList(_context.Ogrencis, "OgrNo", "OgrNo", islem.OgrNo);
            return View(islem);
        }

        // GET: Islems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var islem = await _context.Islems
                .Include(i => i.Kitap)
                .Include(i => i.Ogrenci)
                .FirstOrDefaultAsync(m => m.IslemNo == id);
            if (islem == null)
            {
                return NotFound();
            }

            return View(islem);
        }

        // POST: Islems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var islem = await _context.Islems.FindAsync(id);
            if (islem != null)
            {
                _context.Islems.Remove(islem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IslemExists(int id)
        {
            return _context.Islems.Any(e => e.IslemNo == id);
        }
    }
}
