using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IUserStoryService, UserStoryService>();
            services.AddScoped<IProjectInfoService, ProjectInfoService>();
            services.AddScoped<ITasksService, TasksService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IReleaseService, ReleaseService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<ISubTaskService, SubTaskService>();
            services.AddScoped<ISprintService, SprintService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IPriorityService, PriorityService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ITimeTrackService, TimeTrackService>();
            services.AddScoped<IBugService, BugService>();
            services.AddScoped<IFilterService, FilterService>();

            return services;
        }
    }
}
