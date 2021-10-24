using System;

namespace Likr.Client.Dtos
{
    public class CreatePostDto
    {
        public string Body { get; set; }
        public Guid UserId { get; set; }
    }
}