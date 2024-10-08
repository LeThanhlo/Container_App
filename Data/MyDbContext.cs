using Container_App.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Container_App.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUserInvite> ProjectUserInvites { get; set; }

        public DbSet<RoleGroup> RoleGroups { get; set; }
    }
}
