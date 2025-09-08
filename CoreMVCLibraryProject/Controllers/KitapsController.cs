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
    public class KitapsController : Controller
    {
        private readonly LibraryContext _context;

        public KitapsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Kitaps
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.Kitaps.Include(k => k.Tur).Include(k => k.Yazar);
            return View(await libraryContext.ToListAsync());
        }

        // GET: Kitaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaps
                .Include(k => k.Tur)
                .Include(k => k.Yazar)
                .FirstOrDefaultAsync(m => m.KitapNo == id);
            if (kitap == null)
            {
                return NotFound();
            }

            return View(kitap);
        }

        // GET: Kitaps/Create
        public IActionResult Create()
        {
            ViewData["TurNo"] = new SelectList(_context.Turs, "TurNo", "TurAdi");
            ViewData["YazarNo"] = new SelectList(_context.Yazars, "YazarNo", "YazarAd");
            return View();
        }

        // POST: Kitaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KitapNo,IsbnNo,KitapAdi,SayfaSayisi,Puan,TurNo,YazarNo")] Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kitap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TurNo"] = new SelectList(_context.Turs, "TurNo", "TurAdi", kitap.TurNo);
            ViewData["YazarNo"] = new SelectList(_context.Yazars, "YazarNo", "YazarAd", kitap.YazarNo);
            return View(kitap);
        }

        // GET: Kitaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaps.FindAsync(id);
            if (kitap == null)
            {
                return NotFound();
            }
            ViewData["TurNo"] = new SelectList(_context.Turs, "TurNo", "TurNo", kitap.TurNo);
            ViewData["YazarNo"] = new SelectList(_context.Yazars, "YazarNo", "YazarNo", kitap.YazarNo);
            return View(kitap);
        }

        // POST: Kitaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KitapNo,IsbnNo,KitapAdi,SayfaSayisi,Puan,TurNo,YazarNo")] Kitap kitap)
        {
            if (id != kitap.KitapNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kitap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KitapExists(kitap.KitapNo))
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
            ViewData["TurNo"] = new SelectList(_context.Turs, "TurNo", "TurNo", kitap.TurNo);
            ViewData["YazarNo"] = new SelectList(_context.Yazars, "YazarNo", "YazarNo", kitap.YazarNo);
            return View(kitap);
        }

        // GET: Kitaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.Kitaps
                .Include(k => k.Tur)
                .Include(k => k.Yazar)
                .FirstOrDefaultAsync(m => m.KitapNo == id);
            if (kitap == null)
            {
                return NotFound();
            }

            return View(kitap);
        }

        // POST: Kitaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kitap = await _context.Kitaps.FindAsync(id);
            if (kitap != null)
            {
                _context.Kitaps.Remove(kitap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KitapExists(int id)
        {
            return _context.Kitaps.Any(e => e.KitapNo == id);
        }
    }
}
