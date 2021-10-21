using Fituska.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using Xunit;
using Fituska.Server.Entities;
using System.Text;
using System.Collections.Generic;
using Fituska.Server.Data;
using Microsoft.Extensions.Caching.Memory;

namespace DALTests.Fixtures
{
    public class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        protected readonly ApiWebApplicationFactory _factory;
        protected readonly HttpClient _client;
        protected readonly IConfiguration _configuration;

        public IntegrationTest(ApiWebApplicationFactory fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();
            _configuration = new ConfigurationBuilder()
                  .Build();
        }

        public DbContextOptions<ApplicationDbContext> SetupDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Users.Add(
                   new User
                   {
                       Id = new Guid("15949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                       DiscordUserName = "RivolaCz",
                       UserName = "RivolaCz",
                       Name = "Michal",
                       Surname = "Rivola",
                       PasswordHash = "123456789",
                       PhoneNumber = "737657683",                       
                       AttendingCourses = new List<UserAttendsCourse>
                       {
                           new UserAttendsCourse
                           {
                               Id = new Guid("95949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                               AttendingYear = 2021,
                               CourseId = new Guid("85949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                               UserId = new Guid("15949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                           }                           
                       },
                       EmailConfirmed = true,
                       PhotoID = new Guid("84949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                       AccessFailedCount = 2,
                       RegistrationDate = new DateTime(2021,10,6,22,56,59,450),
                       LastLoginDate = new DateTime(2021, 10, 6, 22, 57, 59, 450),
                       Email = "m.rivola2@seznam.cz",
                   });
            context.Users.Add(new User
                   {
                       Id = new Guid("25949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                       DiscordUserName = "Guest",
                       UserName = "Guest",
                       Name = "Guest",
                       Surname = "Guest",
                       PasswordHash = "123",
                       PhoneNumber = "158",
                       AttendingCourses = new List<UserAttendsCourse>
                       {
                           new UserAttendsCourse
                           {
                               Id = new Guid("95949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                               AttendingYear = 2021,
                               CourseId = new Guid("85949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                               UserId = new Guid("15949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                           }
                       },
                       RegistrationDate = new DateTime(2021, 10, 6, 22, 56, 59, 450),
                       LastLoginDate = new DateTime(2021, 10, 6, 22, 57, 59, 450),
                       Email = "guest@seznam.cz",
                   }
               ) ;
            context.Photos.Add(
                new Photo
                {
                    Id = new Guid("84949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                    Content = Encoding.ASCII.GetBytes("User image&&&"),
                }
              );
            context.Photos.Add(
                new Photo
                {
                    Id = new Guid("84549177-2e41-4e1d-8b6f-7d50ac8190cf"),
                    Content = Encoding.ASCII.GetBytes("Tabulka sčítání"),
                }
              );
            context.Courses.Add(
                new Course
                {
                    Id = new Guid("42949177 - 2e41 - 4e1d - 8b6f - 7d50ac8190cf"),
                    AcademicYear = 2021,
                    Credits = 5,
                    Description = "To nedáš!",
                    Name = "Signály a systémy",
                    Semester = Fituska.Server.Enums.Semester.Winter,
                    Shortcut = "ISS",
                    YearOfStudy = Fituska.Server.Enums.YearOfStudy.BIT2,
                    Url = "www.iss.org"
                }
              );
            context.Categories.Add(
                new Category
                {
                    Id = new Guid("69949177 - 2e41 - 4e1d - 8b6f - 7d50ac8190cf"),
                    CourseId = new Guid("42949177 - 2e41 - 4e1d - 8b6f - 7d50ac8190cf"),
                    Description = "Tady se můžete ptát na věci o semestrálce",
                    Name = "Semestrálka",
                }
              );
            context.Question.Add(
                new Question
                {
                    Id = new Guid("01949177 - 2e41 - 4e1d - 8b6f - 7d50ac8190cf"),
                    Name = "Sčítání",
                    CreationTime = new DateTime(2021, 10, 5, 23, 11, 59),
                    Text = "Kolik je 2+2?",
                    Answers = new List<Answer>()
                    {
                        new Answer
                        {
                            Id = new Guid("05149177 - 2e41 - 4e1d - 8b6f - 7d50ac8190cf"),
                            QuestionId = new Guid("01949177 - 2e41 - 4e1d - 8b6f - 7d50ac8190cf"),
                            CreationTime = new DateTime(2021,10,6,23,11,59),
                            Text= "To je jasný to je přece 9!",
                            UserId = new Guid("15949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                            UsersSawQuestion = new List<User>()
                            {
                                new User()
                                {
                                    Id = new Guid("15949177-2e41-4e1d-8b6f-7d50ac8190cf")
                                }
                            }
                        },
                        new Answer
                        {
                            Id = new Guid("05149177 - 2e41 - 4e1d - 8b6f - 7d50ac8190cf"),
                            QuestionId = new Guid("01949177 - 2e41 - 4e1d - 8b6f - 7d50ac8190cf"),
                            CreationTime = new DateTime(2021,10,6,23,15,59),
                            Text= "4 ez",
                            UserId = new Guid("15949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                        }
                    },
                    UserId = new Guid("15949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                    CategoryId = new Guid("69949177 - 2e41 - 4e1d - 8b6f - 7d50ac8190cf"),
                    UsersVoteQuestion = new List<UserVoteQuestion> 
                    {  new UserVoteQuestion
                    {
                        Id = new Guid("99949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                        UserId = new Guid("25949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                        QuestionId = new Guid("01949177 - 2e41 - 4e1d - 8b6f - 7d50ac8190cf"),
                        Vote = Fituska.Server.Enums.QuestionVote.Like
                    },
                    new UserVoteQuestion
                    {
                        Id = new Guid("99849177-2e41-4e1d-8b6f-7d50ac8190cf"),
                        UserId = new Guid("15949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                        QuestionId = new Guid("01949177 - 2e41 - 4e1d - 8b6f - 7d50ac8190cf"),
                        Vote = Fituska.Server.Enums.QuestionVote.Dislike
                    }
                    }
                }
                );
            context.Discussions.Add(new Discussion()
            {
                Id = new Guid("88849177-2e41-4e1d-8b6f-7d50ac8190cf"),
                AnswerId = new Guid("05149177 - 2e41 - 4e1d - 8b6f - 7d50ac8190cf"),
                AuthorId = new Guid("25949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                Text = "Nope!",
                CreationTime = new DateTime(2021, 10, 7, 8, 10, 15, 500)
            });
            context.Discussions.Add(new Discussion()
            {
                Id = new Guid("88749177-2e41-4e1d-8b6f-7d50ac8190cf"),
                OriginId = new Guid("88849177-2e41-4e1d-8b6f-7d50ac8190cf"),
                AuthorId = new Guid("25949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                Text = "Dík",
                CreationTime = new DateTime(2021, 10, 7, 10, 10, 15, 500)
            });
            context.SaveChanges();
            return options;
        }
    }
}
