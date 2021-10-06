namespace Likr.Posts.Entities
{
    public class Comment
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
    }
}