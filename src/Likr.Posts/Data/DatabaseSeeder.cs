using System;
using System.Collections.Generic;
using System.Linq;
using Likr.Posts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Likr.Posts.Data
{
    public class DatabaseSeeder
    {
        public DatabaseSeeder(AppDbContext context)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();

            List<Post> posts;
            List<Comment> comments;
            var users = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
            };

            if (!context.Posts.Any())
            {
                var random = new Random();
                posts = new List<Post>();

                for (int i = 0; i < 100; i++)
                {
                    posts.Add(new Post
                    {
                        Body = $"Post {i} body bla BLA BLA bla bla this post is from seeding context",
                        LikesCount = random.Next(0, 1000),
                        UserId = users[random.Next(3)].ToString()
                    });
                }
                
                context.Posts.AddRange(posts);
            }

            if (!context.Comments.Any())
            {
                
            }

            context.SaveChanges();
        }
    }
}