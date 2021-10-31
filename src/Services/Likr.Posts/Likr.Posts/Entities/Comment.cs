namespace Likr.Posts.Entities
{
    public class Comment : BaseEntity
    {
        public string Body { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public string PostId { get; set; }
    }
}