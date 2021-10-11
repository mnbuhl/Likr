﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Likr.Posts.Entities
{
    public class Post : BaseEntity
    {
        public string Body { get; set; }
        public string UserId { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount => Comments?.Count ?? 0;
        public ICollection<Comment> Comments { get; set; }
    }
}