using Fituska.Server.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fituska.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,UserRole,Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Question> Question { get; set; }
    }
}
