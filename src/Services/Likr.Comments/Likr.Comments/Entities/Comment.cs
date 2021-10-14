﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Likr.Comments.Entities
{
    public class Comment
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public int LikesCount { get; set; }
        public string PostId { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}