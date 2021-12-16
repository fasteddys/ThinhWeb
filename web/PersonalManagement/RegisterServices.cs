using Microsoft.Extensions.DependencyInjection;
using PersonalManagement.Service;
using PersonalManagement.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement
{
    public static class RegisterServices
    {
        public static void RegisterCrawlerService(this IServiceCollection services)
        {
            services.AddTransient<ICrawlingStockService, CrawlingStockService>();
        }
    }
}
