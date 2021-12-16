using Microsoft.AspNetCore.Mvc;
using PersonalManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.Controllers
{
    public class CrawlStockController : Controller
    {
        private ICrawlingStockService _crawlerService;

        public CrawlStockController(ICrawlingStockService crawlerService)
        {
            _crawlerService = crawlerService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetNhomNganh()
        {
            await _crawlerService.GetDanhSachCoPhieu();
            //await _crawlerService.GetNhomNganh();
            //await _crawlerService.GetNhomNganhPhu();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetNganh()
        {
            await _crawlerService.GetNganh();
            return RedirectToAction("Index");
        }
    }
}
