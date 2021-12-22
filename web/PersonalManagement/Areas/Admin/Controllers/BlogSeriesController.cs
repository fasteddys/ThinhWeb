using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entity;
using Infrastructure.Data;
using MediatR;
using CQRS.Command.BlogSerieCommands;

namespace PersonalManagement.Areas.Admin
{
    [Area("Admin")]
    public class BlogSeriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public BlogSeriesController(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // GET: Admin/BlogSeries
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogSerie.Include(x => x.Posts).ToListAsync());
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
        public async Task<IActionResult> Create()
        {
            await InitViewBag();
            return View();
        }

        // POST: Admin/BlogSeries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBlogSerieCommand blogSerie)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(blogSerie);
                //await _context.SaveChangesAsync();
                //var createBlogSerieCommand = new CreateBlogSerieCommand()
                //{
                //    Name = blogSerie.Name,
                //    Description = blogSerie.Description,
                //    Status = blogSerie.Status
                //};

                await _mediator.Send(blogSerie);
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

        private async Task InitViewBag()
        {
            var posts = await _context.Posts.Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.Title
            }).ToListAsync();
            ViewBag.Posts = posts;
        }
    }
}
