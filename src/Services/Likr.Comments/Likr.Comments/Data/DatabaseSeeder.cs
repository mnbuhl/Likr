using System;
using System.Collections.Generic;
using Likr.Comments.Entities;
using Raven.Client.Documents;

namespace Likr.Comments.Data
{
    public class DatabaseSeeder
    {
        private readonly IDocumentStore _store;

        public DatabaseSeeder(IDocumentStore documentStore)
        {
            _store = documentStore;
        }

        public void Seed()
        {
            using var session = _store.OpenSession();

            var users = new List<User>
            {
                new User
                {
                    Id = "628a5306-09d7-4d28-856b-09994ed4381f",
                    Username = "UserOne",
                    DisplayName = "User 1"
                },
                new User
                {
                    Id = "2201bbb9-8f16-48d5-92a9-2a96bd80fbe9",
                    Username = "UserTwo",
                    DisplayName = "User 2"
                },
                new User
                {
                    Id = "3cb10b30-efb4-4621-b508-797958cd957c",
                    Username = "UserThree",
                    DisplayName = "User 3"
                },
                new User
                {
                    Id = "9fba0c51-aa4f-417b-ac8b-30d72ff7e629",
                    Username = "UserFour",
                    DisplayName = "User 4"
                }
            };

            foreach (var user in users)
            {
                session.Store(user);
            }
            
            var comments = new List<Comment>
            {
                new Comment
                {
                    Id = "f21aa5f8-c4f2-4330-a9ad-cb32f6f67e3c",
                    Body = "They finished building the road they knew no one would ever use.",
                    LikesCount = 0,
                    UserId = "628a5306-09d7-4d28-856b-09994ed4381f",
                    User = users[0],
                    PostId = "a0d38424-5ea1-4d7e-963a-c0f3126ba59d"
                },
                new Comment
                {
                    Id = "ab56582b-46f3-4ca4-be0c-8c66ef3c43b6",
                    Body = "The secret code they created made no sense, even to them.",
                    LikesCount = 0,
                    UserId = "2201bbb9-8f16-48d5-92a9-2a96bd80fbe9",
                    User = users[1],
                    PostId = "a0d38424-5ea1-4d7e-963a-c0f3126ba59d"
                },
                new Comment
                {
                    Id = "8b540efb-2953-4798-ab4f-6dac38b05229",
                    Body = "Little Red Riding Hood decided to wear orange today.",
                    LikesCount = 0,
                    UserId = "3cb10b30-efb4-4621-b508-797958cd957c",
                    User = users[2],
                    PostId = "a6188cef-d474-4af0-9395-0637e34c1cbc"
                },
                new Comment
                {
                    Id = "4a82dd64-6f0d-414e-8ce2-3cf31971856f",
                    Body = "He hated that he loved what she hated about hate.",
                    LikesCount = 0,
                    UserId = "3cb10b30-efb4-4621-b508-797958cd957c",
                    User = users[2],
                    PostId = "ccd77681-127b-4112-889d-f5e3fed9d604"
                },
                new Comment
                {
                    Id = "d598e012-c94f-4f91-b312-249f5adfb08d",
                    Body = "You bite up because of your lower jaw.",
                    LikesCount = 0,
                    UserId = "9fba0c51-aa4f-417b-ac8b-30d72ff7e629",
                    User = users[3],
                    PostId = "d470fbce-6329-4d71-a261-821972c3b82c"
                }
            };
            
            foreach (var comment in comments)
            {
                session.Store(comment, comment.Id);
            }

            session.SaveChanges();
        }
    }
}