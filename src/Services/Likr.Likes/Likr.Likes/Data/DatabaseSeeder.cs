using System.Collections.Generic;
using System.Linq;
using Likr.Likes.Entities;
using Microsoft.EntityFrameworkCore;

namespace Likr.Likes.Data
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _context;

        public DatabaseSeeder(AppDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();
            _context.Database.Migrate();

            if (!_context.Likes.Any() && !_context.Users.Any() && !_context.Posts.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Id = "628a5306-09d7-4d28-856b-09994ed4381f",
                        UserName = "UserOne",
                        DisplayName = "User 1"
                    },
                    new User
                    {
                        Id = "2201bbb9-8f16-48d5-92a9-2a96bd80fbe9",
                        UserName = "UserTwo",
                        DisplayName = "User 2"
                    },
                    new User
                    {
                        Id = "3cb10b30-efb4-4621-b508-797958cd957c",
                        UserName = "UserThree",
                        DisplayName = "User 3"
                    },
                    new User
                    {
                        Id = "9fba0c51-aa4f-417b-ac8b-30d72ff7e629",
                        UserName = "UserFour",
                        DisplayName = "User 4"
                    }
                };

                _context.Users.AddRange(users);
                _context.SaveChanges();

                var posts = new List<Post>
                {
                    new Post
                    {
                        Id = "a0d38424-5ea1-4d7e-963a-c0f3126ba59d",
                        Body = "This is the first post",
                        UserId = "628a5306-09d7-4d28-856b-09994ed4381f"
                    },
                    new Post
                    {
                        Id = "07435fc1-c479-4bb3-9a04-097f3ea9b9ee",
                        Body = "The pigs were insulted that they were named hamburgers.",
                        UserId = "628a5306-09d7-4d28-856b-09994ed4381f"
                    },
                    new Post
                    {
                        Id = "2e91505c-627c-49a1-a740-ad08d54f9985",
                        Body = "This is the last random sentence I will be writing and I am going to stop mid-sent",
                        UserId = "628a5306-09d7-4d28-856b-09994ed4381f"
                    },
                    new Post
                    {
                        Id = "87e05806-ed55-4eb0-90f7-ca097b3e732e",
                        Body = "Whenever he saw a red flag warning at the beach he grabbed his surfboard.",
                        UserId = "2201bbb9-8f16-48d5-92a9-2a96bd80fbe9"
                    },
                    new Post
                    {
                        Id = "238f3a97-2d8c-4a39-b975-6bea844d2e6f",
                        Body = "She traveled because it cost the same as therapy and was a lot more enjoyable.",
                        UserId = "2201bbb9-8f16-48d5-92a9-2a96bd80fbe9"
                    },
                    new Post
                    {
                        Id = "eac9d206-71f9-4c82-bb93-6de0faca065d",
                        Body = "The waves were crashing on the shore; it was a lovely sight.",
                        UserId = "2201bbb9-8f16-48d5-92a9-2a96bd80fbe9"
                    },
                    new Post
                    {
                        Id = "4f8338b0-794c-4f41-a930-c0575fe98684",
                        Body = "She insisted that cleaning out your closet was the key to good driving.",
                        UserId = "3cb10b30-efb4-4621-b508-797958cd957c"
                    },
                    new Post
                    {
                        Id = "d470fbce-6329-4d71-a261-821972c3b82c",
                        Body = "The estate agent quickly marked out his territory on the dance floor.",
                        UserId = "3cb10b30-efb4-4621-b508-797958cd957c"
                    },
                    new Post
                    {
                        Id = "ccd77681-127b-4112-889d-f5e3fed9d604",
                        Body = "He stomped on his fruit loops and thus became a cereal killer.",
                        UserId = "9fba0c51-aa4f-417b-ac8b-30d72ff7e629"
                    },
                    new Post
                    {
                        Id = "a6188cef-d474-4af0-9395-0637e34c1cbc",
                        Body = "When transplanting seedlings, candied teapots will make the task easier.",
                        UserId = "9fba0c51-aa4f-417b-ac8b-30d72ff7e629"
                    },
                    new Post
                    {
                        Id = "f21aa5f8-c4f2-4330-a9ad-cb32f6f67e3c",
                        Body = "They finished building the road they knew no one would ever use.",
                        UserId = "628a5306-09d7-4d28-856b-09994ed4381f"
                    },
                    new Post
                    {
                        Id = "ab56582b-46f3-4ca4-be0c-8c66ef3c43b6",
                        Body = "The secret code they created made no sense, even to them.",
                        UserId = "2201bbb9-8f16-48d5-92a9-2a96bd80fbe9"
                    },
                    new Post
                    {
                        Id = "8b540efb-2953-4798-ab4f-6dac38b05229",
                        Body = "Little Red Riding Hood decided to wear orange today.",
                        UserId = "3cb10b30-efb4-4621-b508-797958cd957c"
                    },
                    new Post
                    {
                        Id = "4a82dd64-6f0d-414e-8ce2-3cf31971856f",
                        Body = "He hated that he loved what she hated about hate.",
                        UserId = "3cb10b30-efb4-4621-b508-797958cd957c"
                    },
                    new Post
                    {
                        Id = "d598e012-c94f-4f91-b312-249f5adfb08d",
                        Body = "You bite up because of your lower jaw.",
                        UserId = "9fba0c51-aa4f-417b-ac8b-30d72ff7e629"
                    }
                };
                
                _context.Posts.AddRange(posts);
                _context.SaveChanges();

                var likes = new List<Like>
                {
                    new Like
                    {
                        ObserverId = "9fba0c51-aa4f-417b-ac8b-30d72ff7e629",
                        TargetId = "f21aa5f8-c4f2-4330-a9ad-cb32f6f67e3c"
                    },
                    new Like
                    {
                        ObserverId = "3cb10b30-efb4-4621-b508-797958cd957c",
                        TargetId = "a6188cef-d474-4af0-9395-0637e34c1cbc"
                    },
                    new Like
                    {
                        ObserverId = "628a5306-09d7-4d28-856b-09994ed4381f",
                        TargetId = "a6188cef-d474-4af0-9395-0637e34c1cbc"
                    },
                    new Like
                    {
                        ObserverId = "2201bbb9-8f16-48d5-92a9-2a96bd80fbe9",
                        TargetId = "a6188cef-d474-4af0-9395-0637e34c1cbc"
                    },
                    new Like
                    {
                        ObserverId = "2201bbb9-8f16-48d5-92a9-2a96bd80fbe9",
                        TargetId = "d470fbce-6329-4d71-a261-821972c3b82c"
                    }
                };
                
                _context.Likes.AddRange(likes);
                _context.SaveChanges();
            }
        }
    }
}