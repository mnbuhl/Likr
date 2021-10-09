namespace Likr.Comments.Entities
{
    public class Post
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public int LikesCount { get; set; }
    }
}