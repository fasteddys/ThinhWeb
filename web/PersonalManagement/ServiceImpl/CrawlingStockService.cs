using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PersonalManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalManagement.ServiceImpl
{
    public class CrawlingStockService : ICrawlingStockService
    {
        private ApplicationDbContext _dbContext;
        ChromeOptions options = new ChromeOptions();
        ChromeDriver driver;
        public CrawlingStockService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            
            options.AddArgument("--start-maximized");
            options.AddArgument("no-sandbox");
            driver = new ChromeDriver(options);
        }

        
        public async Task GetNhomNganh()
        {
            var driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl("https://finance.vietstock.vn/chi-so-nganh.htm");
                var bangNhomNganh = driver.FindElementByXPath("//*[@id='page-content']/div/div[1]/table/tbody");
                if (bangNhomNganh != null)
                {
                    IList<IWebElement> dsDongNhomNganh = bangNhomNganh.FindElements(By.TagName("tr"));
                    IList<IWebElement> tableCells;
                    foreach(IWebElement row in dsDongNhomNganh)
                    {
                        tableCells = row.FindElements(By.TagName("td"));
                        try
                        {
                            #region Nhóm ngành
                            var aTag = tableCells[1].FindElement(By.TagName("a"));
                            if (aTag != null)
                            {
                                var text = aTag.Text;
                                var href = aTag.GetAttribute("href");
                                var nhomNganh = _dbContext.Stock_NhomNganh.FirstOrDefault(x => x.Ten == text);
                                if (nhomNganh != null)
                                {
                                    nhomNganh.UrlChiTiet = href;
                                    _dbContext.Stock_NhomNganh.Update(nhomNganh);
                                }
                                else
                                {
                                    nhomNganh = new Stock_NhomNganh();
                                    nhomNganh.Ten = text;
                                    nhomNganh.UrlChiTiet = href;
                                    _dbContext.Stock_NhomNganh.Add(nhomNganh);
                                }
                                
                                await _dbContext.SaveChangesAsync();
                            }
                            #endregion
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                driver.Close();
                driver.Quit();
            }
        }
        public async Task LoginPage()
        {
            
            driver.Navigate().GoToUrl("https://finance.vietstock.vn/doanh-nghiep-a-z");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div[6]/div/div[2]/div[2]/a[3]")));
            driver.FindElement(By.XPath("/html/body/div[2]/div[6]/div/div[2]/div[2]/a[3]")).Click();
            driver.FindElement(By.Id("txtEmailLogin")).SendKeys("nvthinh09t4@gmail.com");
            driver.FindElement(By.Id("txtPassword")).SendKeys("Protoss123@");
            driver.FindElement(By.Id("btnLoginAccount")).Click();
        }

        public async Task GetDanhSachCoPhieuBySan(string san)
        {

        }

        public async Task GetDanhSachCoPhieu()
        {
            try
            {
                await LoginPage();
                var dsSan = new List<string>() { "HOSE", "HNX", "UPCoM" };
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div[12]/div/div/div[1]/div/div/div[1]/div[1]/div[2]/select")));
                SelectElement selectSan = new SelectElement(driver.FindElement(By.XPath("/html/body/div[2]/div[12]/div/div/div[1]/div/div/div[1]/div[1]/div[2]/select")));
                foreach (var san in dsSan)
                {
                    selectSan.SelectByText(san);

                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='corporate-az']/div/div[1]/div[1]/button")));
                    driver.FindElement(By.XPath("//*[@id='corporate-az']/div/div[1]/div[1]/button")).Click();
                    Thread.Sleep(5000);

                    while (driver.FindElements(By.Name("btn-next1"))[0].Enabled)
                    {
                        var tableCP = driver.FindElement(By.XPath("/html/body/div[2]/div[12]/div/div/div[1]/div/div/div[2]/div/div/div[2]/table/tbody"));
                        var rows = tableCP.FindElements(By.TagName("tr"));
                        foreach (var row in rows)
                        {
                            var cells = row.FindElements(By.TagName("td"));
                            var maCP = cells[1].FindElement(By.TagName("a")).Text;
                            var url = cells[1].FindElement(By.TagName("a")).GetAttribute("href");
                            var tenCty = cells[2].Text;
                            var soLuongCP = 0L;
                            long.TryParse(cells[5].Text.Replace(",", ""), out soLuongCP);
                            var CP = _dbContext.Stock.FirstOrDefault(x => x.MaCP == maCP);
                            if (CP == null)
                                CP = new Stock();
                            CP.MaCP = maCP;
                            CP.UrlChiTiet = url;
                            CP.TenCongTy = tenCty;
                            CP.SoLuongLuuHanh = soLuongCP;
                            CP.SanNiemYet = san;
                            await _dbContext.Stock.Upsert(CP).RunAsync();
                        }
                        driver.FindElements(By.Name("btn-next1"))[0].Click();
                        Thread.Sleep(5000);
                    }

                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                //driver.Quit();
                //driver.Close();
            }
            
        }

        public async Task GetNganh()
        {
            var dsCP = _dbContext.Stock.ToList();
            var taskList = new List<Task>();
            for (var i = 0; i < dsCP.Count; i++)
            {
                if (i % 5 == 0)
                {
                    Task.WaitAll(taskList.ToArray());
                    taskList = new List<Task>();
                }
                taskList.Add(GetNganh(dsCP[i]));
            }
        }
        public async Task GetNganh(Stock cp) {
            using (var driverCon = new ChromeDriver(options))
            {
                if (!String.IsNullOrEmpty(cp.UrlChiTiet))
                {
                    driverCon.Navigate().GoToUrl(cp.UrlChiTiet);
                    var nhomNganh = driverCon.FindElement(By.XPath("/html/body/div[2]/div[11]/div/div[2]/div[1]/div[2]"));
                    var dsNganh = nhomNganh.FindElements(By.TagName("h3"));
                    var lstNganh = new List<string>();
                    for (var i = 0; i < dsNganh.Count; i++)
                    {
                        var nganhTag = dsNganh[i].FindElement(By.TagName("a"));
                        var tenNganh = nganhTag.Text;
                        var urlNganh = nganhTag.GetAttribute("href");

                        var nganhInDb = _dbContext.Stock_NhomNganh.FirstOrDefault(x => x.Ten == tenNganh && x.Level == i);
                        if (nganhInDb == null)
                            nganhInDb = new Stock_NhomNganh();
                        nganhInDb.Ten = tenNganh;
                        nganhInDb.UrlChiTiet = urlNganh;
                        nganhInDb.Level = i;
                        if (i >= 1 && lstNganh.Count > i - 1)
                        {
                            nganhInDb.NhomNganhChaId = lstNganh[i - 1];
                        }
                        await _dbContext.Stock_NhomNganh.Upsert(nganhInDb).RunAsync();
                        lstNganh.Add(nganhInDb.Id);
                        if (i == dsNganh.Count - 1)
                        {
                            cp.NhomNganhId = nganhInDb.Id;
                            await _dbContext.Stock.Upsert(cp).RunAsync();
                        }
                    }
                }
            }
            
        }
        public async Task GetBaoCaoCanDoiKeToan() { }
        public async Task GetBaoCaoKetQuaKinhDoanh() { }
        public async Task GetBaoCaoLuuChuyenTienTe() { }
        public async Task GetChiSoTaiChinh() { }
    }
}
