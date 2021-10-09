using System;
using System.Collections.Generic;
using System.Linq;
using Likr.Posts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Likr.Posts.Data
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

            List<Post> posts;
            List<Comment> comments;
            var users = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
            };

            if (!_context.Posts.Any())
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

                _context.Posts.AddRange(posts);
            }

            if (!_context.Comments.Any())
            {
            }

            _context.SaveChanges();
        }
    }
}