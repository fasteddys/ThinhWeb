using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entity;
using Infrastructure.Data;

namespace PersonalManagement.Areas.Admin
{
    [Area("Admin")]
    public class BlogSeriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogSeriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/BlogSeries
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogSerie.ToListAsync());
        }

        // GET: Admin/BlogSeries/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogSerie = await _context.BlogSerie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogSerie == null)
            {
                return NotFound();
            }

            return View(blogSerie);
        }

        // GET: Admin/BlogSeries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/BlogSeries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Status,Id,CreatedBy,ModifiedBy")] BlogSerie blogSerie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogSerie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogSerie);
        }

        // GET: Admin/BlogSeries/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogSerie = await _context.BlogSerie.FindAsync(id);
            if (blogSerie == null)
            {
                return NotFound();
            }
            return View(blogSerie);
        }

        // POST: Admin/BlogSeries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Description,Status,Id,CreatedBy,ModifiedBy")] BlogSerie blogSerie)
        {
            if (id != blogSerie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogSerie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogSerieExists(blogSerie.Id))
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
            return View(blogSerie);
        }

        // GET: Admin/BlogSeries/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogSerie = await _context.BlogSerie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogSerie == null)
            {
                return NotFound();
            }

            return View(blogSerie);
        }

        // POST: Admin/BlogSeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var blogSerie = await _context.BlogSerie.FindAsync(id);
            _context.BlogSerie.Remove(blogSerie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogSerieExists(string id)
        {
            return _context.BlogSerie.Any(e => e.Id == id);
        }
    }
}
