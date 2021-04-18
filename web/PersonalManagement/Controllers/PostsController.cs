using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entity;
using Infrastructure.Data;
using PersonalManagement.Service;
using PersonalManagement.DTO;
using AutoMapper;
using PersonalManagement.CQRS.Post;
using MediatR;

namespace PersonalManagement.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUtilService _utilService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostsController(ApplicationDbContext context, 
            IUtilService utilService,
            IMapper mapper,
            IMediator mediator)
        {
            _context = context;
            _utilService = utilService;
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posts
                .Include(p => p.Author)
                .Include(x => x.PostTags).ThenInclude(y => y.Tag);
            var posts = await applicationDbContext.ToListAsync();
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            return View(postDtos);
        }

        // GET: Posts/Details/5
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

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id");
            ViewBag.TagsList = _utilService.GetListTags();
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post_PostDto postDto)
        {
            try
            {
                var createPostCommand = new CreatePostCommand { PostDto = postDto };
                await _mediator.Send(createPostCommand);
            }
            catch (Exception ex)
            {
                ViewBag.TagsList = _utilService.GetListTags();
                return View(postDto);
            }
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.Include(x => x.PostTags).FirstOrDefaultAsync(x => x.Id == id);
            var postDto = _mapper.Map<Post_PostDto>(post);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", post.CreatedBy);
            ViewBag.TagsList = _utilService.GetListTags();
            return View(postDto);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Title,Content,Id,CreatedBy,ModifiedBy")] Post post)
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

        // GET: Posts/Delete/5
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

        // POST: Posts/Delete/5
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
