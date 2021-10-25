﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fituska.Server.Data;

public class FituskaDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public FituskaDbContext(DbContextOptions<FituskaDbContext> options) : base(options)
    {
    }

    public DbSet<AnswerEntity> Answers { get; set; }
    
    public DbSet<CategoryEntity> Categories { get; set; }

    public DbSet<CourseAttendanceEntity> CourseAttendances { get; set; }

    public DbSet<CourseEntity> Courses { get; set; }
    
    public DbSet<DiscussionEntity> Discussions { get; set; }
    
    public DbSet<FileEntity> Files { get; set; }
    
    public DbSet<PhotoEntity> Photos { get; set; }
    
    public DbSet<QuestionEntity> Question { get; set; }

    public DbSet<VoteEntity> Votes { get; set; }
}
