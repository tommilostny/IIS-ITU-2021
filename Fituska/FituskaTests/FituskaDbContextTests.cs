using System.Threading.Tasks;
using Fituska.Server.Factories;
using Fituska.Server.Data;
using Xunit;
using System;
using System.Linq;
using Fituska.Server.Entities;
using System.Text;

namespace FituskaTests
{
    public class FituskaDbContextTests : IAsyncLifetime
    {
        private readonly IDbContextFactory dbContextFactory;
        private readonly FituskaDbContext dbContext;

        public FituskaDbContextTests()
        {
            dbContextFactory = new InMemoryDbContextFactory(nameof(FituskaDbContextTests));
            dbContext = dbContextFactory.Create();
        }

        public async Task DisposeAsync() => await dbContext.DisposeAsync();

        public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

        [Fact]
        public void AddNewUser()
        {
            //Arrange
            var newUser = new UserEntity
            {
                Id = new Guid("15949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                DiscordUsername = "RivolaCz",
                UserName = "RivolaCz",
                FirstName = "Michal",
                LastName = "Rivola",
                PasswordHash = "123456789",
                PhoneNumber = "737657683",
                EmailConfirmed = true,
                //PhotoID = new Guid("84949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                AccessFailedCount = 2,
                RegistrationDate = new DateTime(2021, 10, 6, 22, 56, 59, 450),
                LastLoginDate = new DateTime(2021, 10, 6, 22, 57, 59, 450),
                Email = "m.rivola2@seznam.cz",
            };

            //Act
            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();

            //Assert
            using var testDbContext = dbContextFactory.Create();
            var retrievedUser = testDbContext.Users.SingleOrDefault(user => user.Id == newUser.Id);
            Assert.StrictEqual(newUser, retrievedUser);
        }

        [Fact]
        public void AddNewAnswer()
        {
            //Arrange
            var newAnswer = new AnswerEntity
            {
                CreationTime = new DateTime(2020, 10, 22, 12, 15, 6, 450),
                Id = new Guid("55949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                Text = "Ahoj",
            };

            dbContext.Answers.Add(newAnswer);
            dbContext.SaveChanges();

            //Assert
            using var testDbContext = dbContextFactory.Create();
            var retrievedAnswer = testDbContext.Answers.SingleOrDefault(answer => answer.Id == newAnswer.Id);
            Assert.Equal(newAnswer, retrievedAnswer);
        }

        [Fact]
        public void AddNewCategory()
        {
            //Arrange
            var newCategory = new CategoryEntity
            {
                Id = new Guid("65949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                CourseId = new Guid("65749177-2e41-4e1d-8b6f-7d50ac8190cf"),
                Name = "Pùlsemestrálka",
                Description = "Ptejte se k pùlsemestrálce"
            };

            dbContext.Categories.Add(newCategory);
            dbContext.SaveChanges();

            //Assert
            using var testDbContext = dbContextFactory.Create();
            var retrievedCategory = testDbContext.Categories.SingleOrDefault(category => category.Id == newCategory.Id);
            Assert.Equal(newCategory, retrievedCategory);
        }

        [Fact]
        public void AddNewCourse()
        {
            //Arrange
            var newCourse = new CourseEntity
            {
                Id = new Guid("10049177-2e41-4e1d-8b6f-7d50ac8190cf"),
                Name = "Signály a systémy",
                Description = "Fourierovo peklo",
                AcademicYear = 2020,
                Credits = 6,
                Semester = Fituska.Shared.Enums.Semester.Summer,
                Shortcut = "ISS",
                Url = "www.google.com",
                YearOfStudy = Fituska.Shared.Enums.YearOfStudy.BIT2
            };

            dbContext.Courses.Add(newCourse);
            dbContext.SaveChanges();

            //Assert
            using var testDbContext = dbContextFactory.Create();
            var retrievedCourse = testDbContext.Courses.SingleOrDefault(course => course.Id == newCourse.Id);
            Assert.Equal(newCourse, retrievedCourse);
        }

        [Fact]
        public void AddNewCourseAttendence()
        {
            //Arrange
            var newCourseAttendence = new CourseAttendanceEntity
            {
                Id = new Guid("10149177-2e41-4e1d-8b6f-7d50ac8190cf"),
                AttendingYear = 2020,
                CourseId = new Guid("10049177-2e41-4e1d-8b6f-7d50ac8190cf"),
                UserId = new Guid("15949177-2e41-4e1d-8b6f-7d50ac8190cf"),

            };

            dbContext.CourseAttendances.Add(newCourseAttendence);
            dbContext.SaveChanges();

            //Assert
            using var testDbContext = dbContextFactory.Create();
            var retrievedCourse = testDbContext.CourseAttendances.SingleOrDefault(courseAttendence => courseAttendence.Id == newCourseAttendence.Id);
            Assert.Equal(newCourseAttendence, retrievedCourse);
        }

        [Fact]
        public void AddNewDiscussion()
        {
            //Arrange
            var newDiscussion = new DiscussionEntity
            {
                Id = new Guid("15049177-2e41-4e1d-8b6f-7d50ac8190cf"),
                AuthorId = new Guid("15949177-2e41-4e1d-8b6f-7d50ac8190cf"),
                CreationTime = new DateTime(2021, 10, 22, 17, 16, 50, 150),
                Text = "Sin + cos = arctg ?",
                AnswerId = new Guid("55949177-2e41-4e1d-8b6f-7d50ac8190cf"),
            };

            dbContext.Discussions.Add(newDiscussion);
            dbContext.SaveChanges();

            //Assert
            using var testDbContext = dbContextFactory.Create();
            var retrievedDiscussion = testDbContext.Discussions.SingleOrDefault(discussion => discussion.Id == newDiscussion.Id);
            Assert.Equal(newDiscussion, retrievedDiscussion);
        }
        [Fact]
        public void AddNewFile()
        {
            //Arrange
            var newFile = new FileEntity
            {
                Id = new Guid("14049177-2e41-4e1d-8b6f-7d50ac8190cf"),
                Name = "luxusnívideosešumem.wav",
                AnswerId = new Guid("55949177-2e41-4e1d-8b6f-7d50ac8190cf"),

            };
            dbContext.Files.Add(newFile);
            dbContext.SaveChanges();
            //Assert
            using var testDbContext = dbContextFactory.Create();
            var retrievedFile = testDbContext.Files.SingleOrDefault(file => file.Id == newFile.Id);
            Assert.Equal(newFile, retrievedFile);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(new byte[] {1,2,3,4,5})]
        [InlineData(new byte[] {})]
        public void AddNewPhoto(byte[] content)
        {
            //Arrange
            var newPhoto = new PhotoEntity
            {
                Id = new Guid(),
                Content = content
            };

            dbContext.Photos.Add(newPhoto);
            dbContext.SaveChanges();

            //Assert
            using var testDbContext = dbContextFactory.Create();
            var retrievedFile = testDbContext.Photos.SingleOrDefault(photo => photo.Id == newPhoto.Id);
            Assert.Equal(newPhoto, retrievedFile);
        }

        [Fact]
        public void AddNewQuestion()
        {
            var newQuestion = new QuestionEntity
            {
                Id = new Guid("13549177-2e41-4e1d-8b6f-7d50ac8190cf"),
                Text = "2+2?",
                Title = "Sèítání"
            };

            dbContext.Question.Add(newQuestion);
            dbContext.SaveChanges();

            //Assert
            using var testDbContext = dbContextFactory.Create();
            var retrievedQuestion = testDbContext.Question.SingleOrDefault(question => question.Id == newQuestion.Id);
            Assert.Equal(newQuestion, retrievedQuestion);
        }


        [Fact]
        public void AddNewVote()
        {
            var newVote = new VoteEntity
            {
                Id = new Guid("10149177-2e41-4e1d-8b6f-7d50ac8190cf"),
                Vote = Fituska.Shared.Enums.QuestionVote.Like,
            };

            dbContext.Votes.Add(newVote);
            dbContext.SaveChanges();

            //Assert
            using var testDbContext = dbContextFactory.Create();
            var retrievedVote = testDbContext.Votes.SingleOrDefault(vote => vote.Id == newVote.Id);
            Assert.Equal(newVote, retrievedVote);
        }
    }
}