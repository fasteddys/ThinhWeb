using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PersonalManagement.Extension;
using PersonalManagement.Models;
using PersonalManagement.Models.Home;
using PersonalManagement.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonalManagement.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger,
            IPostService postService, 
            HttpClient httpClient)
        {
            _logger = logger;
            _postService = postService;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            return RedirectToAction("UpdateMatchData", "NBA");
        }
        public async Task<IActionResult> Index2()
        {
            var matches = new Dictionary<string, string>
            {
                //{ "j374ws3jzyyugko", "new-york-knicks-milwaukee-bucks"},
                //{ "g676jsrxeo8aokr", "detroit-pistons-brooklyn-nets" },
                //{ "ezk90sg8lr9u1kn", "oklahoma-city-thunder-dallas-mavericks" },
                //{ "l6kers6ovn2bvq5", "san-antonio-spurs-new-orleans-pelicans" },
                //{ "xvkjvsn4ly4t8k9", "portland-trail-blazers-minnesota-timberwolves" },
                //{ "m2q19swv5xjfek6", "los-angeles-lakers-orlando-magic" },

                { "zrknxsexogjswql", "cleveland-cavaliers-miami-heat" },
                { "oj7x6s059l1t47g", "indiana-pacers-golden-state-warriors" },
                { "vmqy6sw465jagk9", "toronto-raptors-sacramento-kings" },
                { "o17p8s9x1drbykj", "atlanta-hawks-houston-rockets" },
                { "jr7o9s10z4jsg70", "boston-celtics-milwaukee-bucks" },
                { "9gkl6sr8ndobmkx", "memphis-grizzlies-philadelphia-76ers" },
                { "ndkzysjwx0xcx73", "dallas-mavericks-charlotte-hornets" },
                { "527rjslgjzpf4ke", "denver-nuggets-washington-wizards" },
                { "wv784smlwr3soqr", "los-angeles-clippers-phoenix-suns" },

            };
            var siteCodes = new List<string>
            {
                "2",
                "9",
                //"101",
                //"105"
            };

            var rateMatches = new ChartViewModel();
            foreach (var match in matches)
            {
                var rates = new List<Rate>();
                foreach (var siteCode in siteCodes)
                {
                    var url = $"https://api.aiscore.com/v1/m/api/match/odds/detail?match_id={match.Key}&odds_type=bs&cid={siteCode}";
                    var urlSpread = $"https://api.aiscore.com/v1/m/api/match/odds/detail?match_id={match.Key}&odds_type=asia&cid={siteCode}";
                    var getRequest = await _httpClient.GetAsync(url);
                    var site = new Rate();
                    site.Site = siteCode;
                    site.Match = match.Value;
                    if (getRequest.IsSuccessStatusCode)
                    {
                        var responseString = await getRequest.Content.ReadAsStringAsync();
                        var total = new List<double>();
                        var over = new List<double>();
                        var under = new List<double>();
                        var phiNhaCai = new List<double>();
                        var overunder = new List<double>();
                        var overReal = new List<double>();
                        var underReal = new List<double>();
                        foreach (var rate in responseString.Split("\n").Where(x => !x.Contains("Q") && x.Length > 30))
                        {
                            var rateFormat = Regex.Replace(rate, @"[^\u0000-\u007F]+", string.Empty);
                            rateFormat = Regex.Replace(rateFormat, @"[^0-9a-z.]+", "|");

                            var t = double.Parse(rateFormat.Split("|")[rate.Contains("Q") ? 7 : 5]);
                            var o = double.Parse(rateFormat.Split("|")[rate.Contains("Q") ? 6 : 4]);
                            var u = double.Parse(rateFormat.Split("|")[rate.Contains("Q") ? 8 : 6]);
                            var ou = Math.Round(o / u * 100, 2);
                            var phi = Math.Round(((200 - (o * 100 + u * 100)) / 2), 2);
                            var or = Math.Round((o * 100 + phi) * t / 100, 2);
                            var ur = Math.Round((u * 100 + phi) * t / 100, 2);
                            total.Add(t);
                            over.Add(o);
                            under.Add(u);
                            phiNhaCai.Add(phi);
                            overunder.Add(ou);
                            overReal.Add(or);
                            underReal.Add(ur);
                        }

                        site.Total = total.ReverseList();
                        site.Over = over.ReverseList();
                        site.Under = under.ReverseList();
                        site.Fee = phiNhaCai.ReverseList();
                        //site.OU = String.Join(",", overunder);
                        site.OverReal = overReal.ReverseList();
                        site.UnderReal = underReal.ReverseList();
                        site.TotalAverage = Math.Round(total.Average(x => x), 2);
                        site.OverAverage = Math.Round(over.Average(x => x), 2);
                        site.UnderAverage = Math.Round(under.Average(x => x), 2);
                        site.OverRealAverage = Math.Round(overReal.Average(x => x), 2);
                        site.UnderRealAverage = Math.Round(underReal.Average(x => x), 2);
                    }

                    var getSpreadRequest = await _httpClient.GetAsync(urlSpread);
                    if (getSpreadRequest.IsSuccessStatusCode)
                    {
                        var responseString = await getSpreadRequest.Content.ReadAsStringAsync();
                        var spread = new List<double>();
                        var team1 = new List<double>();
                        var team2 = new List<double>();
                        foreach (var rate in responseString.Split("\n").Where(x => !x.Contains("Q") && x.Length > 30))
                        {
                            var rateFormat = Regex.Replace(rate, @"[^\u0000-\u007F]+", string.Empty);
                            rateFormat = Regex.Replace(rateFormat, @"[^0-9a-z.]+", "|");

                            var s = double.Parse(rateFormat.Split("|")[rate.Contains("Q") ? 7 : 5]);
                            var t1 = double.Parse(rateFormat.Split("|")[rate.Contains("Q") ? 6 : 4]);
                            var t2 = double.Parse(rateFormat.Split("|")[rate.Contains("Q") ? 8 : 6]);

                            spread.Add(s);
                            team1.Add(t1 * s);
                            team2.Add(t2 * s);
                        }
                        site.Spread = spread.ReverseList();
                        site.Team1 = team1.ReverseList();
                        site.Team2 = team2.ReverseList();
                    }
                    rates.Add(site);
                }
                rateMatches.Rates.Add(rates);
            }

            //var ttt = JsonConvert.SerializeObject(rateMatches, Formatting.Indented);
            return View(rateMatches);
        }

        //public async Task<IActionResult> Index(string searchString, string tag, DateTime? createAt, int pageIndex, int pageSize)
        //{
        //    var totalRecords = 0;
        //    var postDtos = _postService.GetListOfPosts(searchString, tag, out totalRecords, createAt, pageIndex, pageSize);
        //    var model = new Portal_Posts_IndexViewModel
        //    {
        //        Posts = new Common_PagingViewModel<DTO.PostDto>
        //        {
        //            DataSource = postDtos,
        //            PageIndex = pageIndex,
        //            RecordPerPage = pageSize,
        //            TotalRecords = totalRecords
        //        },
        //        Tag = tag,
        //        SearchStr = searchString,
        //        CreateAt = createAt
        //    };
        //    //bet365: https://api.aiscore.com/v1/m/api/match/odds/detail?match_id=ndkzysjwxg2tx73&odds_type=bs&cid=2
        //    //william: https://api.aiscore.com/v1/m/api/match/odds/detail?match_id=ndkzysjwxg2tx73&odds_type=bs&cid=9
        //    //betway: https://api.aiscore.com/v1/m/api/match/odds/detail?match_id=ndkzysjwxg2tx73&odds_type=bs&cid=105

            

        //    return View(model);
        //}

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
