using Domain.Entity;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PersonalManagement.Service;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalManagement.ServiceImpl
{
    public class CrawlingNBAService : ICrawlingNBAService
    {
        private ApplicationDbContext _dbContext;
        ChromeOptions options = new ChromeOptions();
        ChromeDriver driver;
        public CrawlingNBAService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            options.AddArgument("--start-maximized");
            options.AddArgument("no-sandbox");
            //options.EnableMobileEmulation("iPhone 6");
            driver = new ChromeDriver(options);
        }

        public async Task GetMatches(DateTime date)
        {
            try
            {
                var url = $"https://www.aiscore.com/basketball/{date.ToString("yyyyMMdd")}";
                driver.Navigate().GoToUrl(url);
                //driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                Thread.Sleep(10000);
                var leagueTitles = driver.FindElements(By.ClassName("list_title"));
                if (leagueTitles != null)
                {
                    foreach (var leagueTitle in leagueTitles)
                    {
                        var leagueTitleTagA = leagueTitle.FindElement(By.ClassName("compText"));
                        if (leagueTitleTagA != null)
                        {
                            var leagueName = leagueTitleTagA.Text;
                            if (leagueName.Replace(" ", "").ToLower() == "nationalbasketballassociation")
                            {
                                var leagueParents = leagueTitle.FindElement(By.XPath("./.."));
                                if (leagueParents != null)
                                {
                                    //var leagueMatchDivs = leagueParents.FindElements(By.TagName("div"))[1];
                                    var leagueMatches = leagueParents.FindElements(By.XPath(".//div[@class='list']"));
                                    foreach (var match in leagueMatches)
                                    {
                                        var aTag = match.FindElement(By.TagName("a"));
                                        var matchUrl = aTag.GetAttribute("href");
                                        var contents = aTag.FindElements(By.XPath("./div"));

                                        //div 0 time and status
                                        var divStatus = contents[0].FindElement(By.TagName("div")).Text;
                                        //div 1 name of teams
                                        var teams = match.FindElements(By.XPath(".//div[@class='w-o-h']"));
                                        var homeName = "";
                                        var awayName = "";
                                        if (teams != null && teams.Count == 2)
                                        {
                                            homeName = teams[0].Text;
                                            awayName = teams[1].Text;
                                        }

                                        //div 2 scores
                                        var scores = contents[2].FindElements(By.XPath("./div/div"));
                                        var homeScore = String.Join(";", scores[0].FindElements(By.ClassName("flex-1")).Select(x => x.Text).ToList());
                                        var awayScore = String.Join(";", scores[1].FindElements(By.ClassName("flex-1")).Select(x => x.Text).ToList());

                                        NBAMatch nbaMatch = _dbContext
                                                                .NBAMatches
                                                                .FirstOrDefault(
                                                                    match => 
                                                                        match.Date.HasValue
                                                                        && match.Date.Value.Date == date.Date 
                                                                        && match.Away == awayName 
                                                                        && match.Home == homeName
                                                                );
                                        bool isNewRecord = false;
                                        if (nbaMatch == null)
                                        {
                                            nbaMatch = new NBAMatch();
                                            isNewRecord = true;
                                        }

                                        nbaMatch.Away = awayName;
                                        nbaMatch.Home = homeName;
                                        nbaMatch.AwayScore = awayScore;
                                        nbaMatch.HomeScore = homeScore;
                                        nbaMatch.Status = divStatus;
                                        nbaMatch.Date = date;
                                        nbaMatch.Code = matchUrl.Split("/").Last();
                                        if (isNewRecord)
                                        {
                                            _dbContext.NBAMatches.Add(nbaMatch);
                                        }
                                        else
                                        {
                                            _dbContext.NBAMatches.Update(nbaMatch);
                                        }
                                        
                                    }
                                    await _dbContext.SaveChangesAsync();
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task GetOddOverUnder()
        {
            try
            {
                dynamic data = new ExpandoObject();
                driver.Navigate().GoToUrl("https://m.aiscore.com/basketball/match-san-antonio-spurs-denver-nuggets/ndkzysjwxymux73/odds");
                var oddTabLink = driver.FindElementByXPath("/html/body/div/div/div/div[2]/div[3]/div[1]/div[1]/a[3]");
                if (oddTabLink != null)
                {
                    oddTabLink.Click();
                    var rateOnSites = driver.FindElementsByClassName("iconjiantou");
                    if (rateOnSites != null)
                    {
                        rateOnSites[0].Click();
                        Thread.Sleep(5000);
                        var rows = driver.FindElementsByClassName("contentItem");
                        if (rows != null)
                        {
                            foreach (var row in rows)
                            {
                                var cells = row.FindElements(By.ClassName("borderR "));
                                data.rates = new List<object>();
                                if (!cells[0].Text.Contains("Q"))
                                {
                                    dynamic rate = new
                                    {
                                        Total = cells[3].Text,
                                        Over = cells[2].Text,
                                        Under = cells[4].Text,
                                        Time = cells[5].Text,
                                    };
                                    data.rates.Add(rate);
                                }
                            }
                        }
                        //foreach (var site in rateOnSites) 
                        //{
                        //    site.
                        //}
                    }
                }
                var zzz = JsonConvert.SerializeObject(data, Formatting.Indented);
            }
            catch (Exception e)
            {
            }
            throw new NotImplementedException();
        }

        public async Task GetOddSpread()
        {
            throw new NotImplementedException();
        }
    }
}
