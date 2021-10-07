using Microsoft.EntityFrameworkCore;
using Fituska.Server.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Fituska.Server.Factories;
using Fituska.Server.Data;
using System.Collections.Generic;

namespace DALTests
{
    public class DbContextTests : IAsyncLifetime
    {
        private readonly IDbContextFactory dbContextFactory;
        private readonly ApplicationDbContext dbContext;

        public DbContextTests()
        {
            dbContextFactory = new InMemoryDbContextFactory(nameof(DbContextTests));
            dbContext = dbContextFactory.Create();
        }

        public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

        public async Task DisposeAsync() => await dbContext.DisposeAsync();

        [Fact]
        public void AddNewMusicBandTest()
        {
            //Arrange
            var newUser = new User
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
            Assert.Equal(newUser, retrievedUser);
        }
    }
}
