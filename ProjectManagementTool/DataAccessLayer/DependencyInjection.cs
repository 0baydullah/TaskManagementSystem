using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
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
            services.AddScoped<IFeatureRepo, FeatureRepo>();
            services.AddScoped<ISubTasksRepo, SubTasksRepo>();
            services.AddScoped<IStatusRepo, StatusRepo>();
            services.AddScoped<IPriorityRepo, PriorityRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<ITimeTrackRepo, TimeTrackRepo>();
            services.AddScoped<IBugRepo, BugRepo>();

            return services;
        }
    }
}
