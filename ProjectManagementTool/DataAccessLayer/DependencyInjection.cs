using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryLayer(this IServiceCollection services)
        {
            //services.AddScoped<IxxxxRepository, xxxxRepository>();
<<<<<<< HEAD
            services.AddScoped<IUserStoryRepo, UserStoryRepo>();
=======

            services.AddScoped<IProjectInfoRepo, ProjectInfoRepo>();
>>>>>>> origin/Project-CRUD
            return services;
        }
    }
}
