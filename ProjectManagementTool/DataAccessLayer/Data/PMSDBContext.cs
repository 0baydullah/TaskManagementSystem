using DataAccessLayer.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Data
{
    public class PMSDBContext : IdentityDbContext<UserInfo, IdentityRole<int>,int>
    {
        public PMSDBContext(DbContextOptions<PMSDBContext> options) : base(options)
        {

        }

        public DbSet<ProjectInfo> ProjectInfo { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Member> Members { get; set; }

    }
}
