using System;
using System.Collections.Generic;
using Fituska.Server.Repositories.Interfaces;
using Fituska.Server.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Fituska.Server.Repositories
{
    public class UserRepository : IAppRepository<User>
    {
        private readonly AppDbContext database;
        public UserRepository(AppDbContext database)
        {
            this.database = database;
        }

        private void LoadUserCourses(User user)
        {
            if (user != null) return;
                //user.AttendingCourses = database.Courses.Where(course => course.)
        }

        public IList<User> GetAll()
        {
            var users = database.Users.ToList();
            return users;
        }

        public User GetById(Guid id)
        {
            var user = database.Users.Find(id);
            return user;
        }

        public Guid Create(User manufacturer)
        {
            database.Users.Add(manufacturer);
            database.SaveChanges();
            return manufacturer.Id;
        }

        public void Delete(Guid id)
        {
            var user = database.Users.Find(id);
            if (user != null)
            {
                database.Users.Remove(user);
                database.SaveChanges();
            }
        }

        public Guid Update(User user)
        {
            var userForUpdate = database.Users.Attach(user);
            userForUpdate.State = EntityState.Modified;
            database.SaveChangesAsync();
            return user.Id;
        }
    }
}
