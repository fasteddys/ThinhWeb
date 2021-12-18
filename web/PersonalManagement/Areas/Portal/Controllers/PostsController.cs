using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entity;
using Infrastructure.Data;
using PersonalManagement.Models;
using PersonalManagement.Service;

namespace PersonalManagement.Areas.Portal.Controllers
{
    [Area("Portal")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostService _postService;

        public PostsController(ApplicationDbContext context, IPostService postService)
        {
            _context = context;
            _postService = postService;
        }

        // GET: Portal/Posts
        public async Task<IActionResult> Index(string searchString, string tag, DateTime? createAt, int pageIndex, int pageSize)
        {
            var totalRecords = 0;
            var postDtos = _postService.GetListOfPosts(searchString, tag, out totalRecords, createAt, pageIndex, pageSize);
            var model = new Portal_Posts_IndexViewModel
            {
                Posts = new Common_PagingViewModel<DTO.PostDto>
                {
                    DataSource = postDtos,
                    PageIndex = pageIndex,
                    RecordPerPage = pageSize,
                    TotalRecords = totalRecords
                },
                Tag = tag,
                SearchStr = searchString,
                CreateAt = createAt
            };
            //bet365: https://api.aiscore.com/v1/m/api/match/odds/detail?match_id=ndkzysjwxg2tx73&odds_type=bs&cid=2
            //william: https://api.aiscore.com/v1/m/api/match/odds/detail?match_id=ndkzysjwxg2tx73&odds_type=bs&cid=9
            //betway: https://api.aiscore.com/v1/m/api/match/odds/detail?match_id=ndkzysjwxg2tx73&odds_type=bs&cid=105



            return View(model);
        }

        // GET: Portal/Posts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Portal/Posts/Create
        public IActionResult Create()
        {
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Portal/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,Status,Id,CreatedBy,ModifiedBy")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", post.CreatedBy);
            return View(post);
        }

        // GET: Portal/Posts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", post.CreatedBy);
            return View(post);
        }

        // POST: Portal/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Title,Content,Status,Id,CreatedBy,ModifiedBy")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", post.CreatedBy);
            return View(post);
        }

        // GET: Portal/Posts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Portal/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(string id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
