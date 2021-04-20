using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalManagement.Models;
using PersonalManagement.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;

        public HomeController(ILogger<HomeController> logger,
            IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public IActionResult Index(string searchString, string tag, DateTime? createAt, int pageIndex, int pageSize)
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
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
