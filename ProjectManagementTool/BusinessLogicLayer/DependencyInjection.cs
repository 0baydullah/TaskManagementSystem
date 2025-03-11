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
            services.AddScoped<IMemberService, MemberService>();

            return services;
        }
    }
}
