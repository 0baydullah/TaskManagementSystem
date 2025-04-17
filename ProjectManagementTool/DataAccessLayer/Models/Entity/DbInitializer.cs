using DataAccessLayer.Data;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public static class DbInitializer
    {
        private static ILog _log = LogManager.GetLogger(typeof(DbInitializer));
        public static void Initialize(PMSDBContext context)
        { 
            context.Database.EnsureCreated();

            if( context.BugStatuses.Any() )
            {
                Console.WriteLine("Database already seeded!");
            }

            using var transaction = context.Database.BeginTransaction();

            try
            {
                var bugStatus = new List<BugStatus>
                {
                    new BugStatus { Name = "Pending", ColorHex = "#0fb6c2" },
                    new BugStatus { Name = "Dev Running", ColorHex = "#81d98c" },
                    new BugStatus { Name = "Need to Test", ColorHex = "#d77575" },
                    new BugStatus { Name = "Test Running", ColorHex = "#10f9ab" },
                    new BugStatus { Name = "Solved", ColorHex = "#00ff00" },
                    new BugStatus { Name = "Postpone", ColorHex = "#bab0b0" },
                    new BugStatus { Name = "Invalid", ColorHex = "#e5e817" },
                    new BugStatus { Name = "Canceled", ColorHex = "#644790" },
                };
                context.BugStatuses.AddRange(bugStatus);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _log.Error(ex.Message);
                throw;
            }

        }

    }
}
