using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fituska.DAL;

public class FituskaDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public FituskaDbContext(DbContextOptions<FituskaDbContext> options) : base(options)
    {
    }

    public DbSet<AnswerEntity> Answers { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<CourseAttendanceEntity> CourseAttendances { get; set; }
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<FileEntity> Files { get; set; }
    public DbSet<QuestionEntity> Questions { get; set; }
    public DbSet<VoteEntity> Votes { get; set; }
    public DbSet<UserSawAnswerEntity> UsersSawAnswers { get; set; }
    public DbSet<UserSawQuestionEntity> UsersSawQuestions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<CourseAttendanceEntity>()
            .HasOne(attendance => attendance.User)
            .WithMany(user => user.AttendingCourses)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<CourseAttendanceEntity>()
            .HasOne(attendance => attendance.Course)
            .WithMany(course => course.Attendees)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<QuestionEntity>()
            .HasOne(question => question.Category)
            .WithMany(category => category.Questions)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CategoryEntity>()
            .HasMany(category => category.Questions)
            .WithOne(question => question.Category)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<AnswerEntity>()
            .HasOne(answer => answer.Question)
            .WithMany(question => question.Answers)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<QuestionEntity>()
            .HasMany(question => question.Answers)
            .WithOne(answer => answer.Question)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<UserSawQuestionEntity>()
            .HasOne(sawQ => sawQ.Question)
            .WithMany(question => question.UserSawQuestions)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<UserSawAnswerEntity>()
            .HasOne(sawA => sawA.Answer)
            .WithMany(answer => answer.UsersSawAnswer)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<VoteEntity>()
            .HasOne(vote => vote.Answer)
            .WithMany(answer => answer.UsersVoteAnswers)
            .OnDelete(DeleteBehavior.NoAction);

        base.OnModelCreating(builder);
    }
}
