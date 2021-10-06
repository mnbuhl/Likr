﻿using Likr.Posts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Likr.Posts.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}