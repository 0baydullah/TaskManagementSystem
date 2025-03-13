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
            services.AddScoped<IRoleRepo, RoleRepo>();
            services.AddScoped<IUserStoryRepo, UserStoryRepo>();
            services.AddScoped<IProjectInfoRepo, ProjectInfoRepo>();
            services.AddScoped<IMemberRepo, MemberRepo>();
            services.AddScoped<ITasksRepo, TasksRepo>();
            services.AddScoped<IReleaseRepo, ReleaseRepo>();
            services.AddScoped<ISprintRepo, SprintRepo>();

            return services;
        }
    }
}
