using Microsoft.AspNetCore.Mvc;
using PersonalManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Controllers
{
    public class CrawlerController : Controller
    {
        private ICrawlingNBAService _nbaCrawler;

        public CrawlerController(ICrawlingNBAService nbaCrawler)
        {
            _nbaCrawler = nbaCrawler;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetNBAOverUnder()
        {
            await _nbaCrawler.GetOddOverUnder();
            return RedirectToAction("Index");
        }
    }
}
