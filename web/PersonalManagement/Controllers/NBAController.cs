using Microsoft.AspNetCore.Mvc;
using PersonalManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Controllers
{
    public class NBAController : Controller
    {
        private ICrawlingNBAService _crawlingNBAService;

        public NBAController(ICrawlingNBAService crawlingNBAService)
        {
            _crawlingNBAService = crawlingNBAService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult UpdateMatchData()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMatchData(DateTime? UpdateToDate)
        {
            var date = UpdateToDate ?? DateTime.Now;
            for (var date2 = DateTime.Now; date2.Date > new DateTime(2021, 10, 20).Date; date2.AddDays(-1))
            {
                await _crawlingNBAService.GetMatches(date2);
            }
            

            return View();
        }
    }
}
