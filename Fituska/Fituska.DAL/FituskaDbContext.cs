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

}
