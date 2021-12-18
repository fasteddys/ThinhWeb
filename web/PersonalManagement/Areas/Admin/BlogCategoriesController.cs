using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entity;
using Infrastructure.Data;
using PersonalManagement.Areas.Admin.ViewModels.BlogCategory;
using AutoMapper;

namespace PersonalManagement.Areas.Admin
{
    [Area("Admin")]
    public class BlogCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BlogCategoriesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Admin/BlogCategories
        public async Task<IActionResult> Index(IndexViewModel indexVM)
        {
            if (indexVM == null)
            {
                indexVM = new IndexViewModel();
            }

            indexVM.BlogCategories = await _context.BlogCategory.ToListAsync();

            return View(indexVM);
        }

        // GET: Admin/BlogCategories/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        // GET: Admin/BlogCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/BlogCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                var blogCategory = _mapper.Map<BlogCategory>(model.CreateBlogCategoryInfor);
                _context.Add(blogCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/BlogCategories/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategory.FindAsync(id);
            if (blogCategory == null)
            {
                return NotFound();
            }
            return View(blogCategory);
        }

        // POST: Admin/BlogCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Description,Status,Id,CreatedBy,ModifiedBy")] BlogCategory blogCategory)
        {
            if (id != blogCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogCategoryExists(blogCategory.Id))
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
            return View(blogCategory);
        }

        // GET: Admin/BlogCategories/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        // POST: Admin/BlogCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var blogCategory = await _context.BlogCategory.FindAsync(id);
            _context.BlogCategory.Remove(blogCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogCategoryExists(string id)
        {
            return _context.BlogCategory.Any(e => e.Id == id);
        }
    }
}
