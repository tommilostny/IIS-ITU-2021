using Microsoft.EntityFrameworkCore;
using Fituska.Server.Entities;

namespace Fituska.Server
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Category {  get; set; }
        public DbSet<Course> Courses {  get; set; }
        public DbSet<Discussion> Discussions {  get; set;}
        public DbSet<File> Files {  get; set; }
        public DbSet<Photo> Photos {  get; set; }   
        public DbSet<Question> Question {  get; set; }
        public DbSet<User> Users {  get; set; }

    }
}
