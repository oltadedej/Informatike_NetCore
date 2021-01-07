using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDb_Infor.Services;
using UniversityDb_Infor.Services.Contract;

namespace UniversityInformatike.DPI
{
    public static class ServiceCollectionExtension
    {
        //Konfigurimi i serviceve per aplikacionin
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceUniversityDB, ServiceUniversityDB>();
            services.AddSingleton<StaticCache>();
            return services;
        }
      
    }


}
