using System.Collections.Generic;
using System.Linq;
using Likr.Identity.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace Likr.Identity.Server.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DatabaseSeeder(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void Seed()
        {
            if (!_userManager.Users.Any())
            {
                var users = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Id = "628a5306-09d7-4d28-856b-09994ed4381f",
                        UserName = "UserOne",
                        DisplayName = "User 1",
                        Email = "userone@test.com",
                        LockoutEnabled = false,
                        EmailConfirmed = true
                    },
                    new ApplicationUser
                    {
                        Id = "2201bbb9-8f16-48d5-92a9-2a96bd80fbe9",
                        UserName = "UserTwo",
                        DisplayName = "User 2",
                        Email = "usertwo@test.com",
                        LockoutEnabled = false,
                        EmailConfirmed = true
                    },
                    new ApplicationUser
                    {
                        Id = "3cb10b30-efb4-4621-b508-797958cd957c",
                        UserName = "UserThree",
                        DisplayName = "User 3",
                        Email = "userthree@test.com",
                        LockoutEnabled = false,
                        EmailConfirmed = true
                    },
                    new ApplicationUser
                    {
                        Id = "9fba0c51-aa4f-417b-ac8b-30d72ff7e629",
                        UserName = "UserFour",
                        DisplayName = "User 4",
                        Email = "userfour@test.com",
                        LockoutEnabled = false,
                        EmailConfirmed = true
                    }
                };

                foreach (var user in users)
                {
                    _userManager.CreateAsync(user, "Pa$$w0rd").Wait();
                }
            }
        }
    }
}