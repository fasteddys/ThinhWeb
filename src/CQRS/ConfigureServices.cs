using CQRS.Queries;
using CQRS.Queries.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CQRS
{
    public static class CQRSExtension
    {
        public static void ConfigureCQRSServices(this IServiceCollection services)
        {
            services.AddScoped<IUserQueries, UserQueriesHandler>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
