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
<<<<<<< HEAD
            services.AddScoped<IEmailSenderService, EmailSenderService>();

=======
            //services.AddScoped<IxxxxxService, xxxxxService>();
            //services.AddScoped<IxxxxxService, xxxxxService>();
            services.AddScoped<IUserStoryService, UserStoryService>();
>>>>>>> origin/UserStory

            return services;
        }
    }
}
