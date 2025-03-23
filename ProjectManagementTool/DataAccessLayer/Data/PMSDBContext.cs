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
        public DbSet<SubTask> SubTask { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TimeTrack> TimeTracks { get; set; }
        public DbSet<Bug> Bugs { get; set; }

    }
}
