namespace Likr.Likes.Entities
{
    public class Like
    {
        public string ObserverId { get; set; }
        public User Observer { get; set; }
        public string TargetId { get; set; }
        public Post Target { get; set; }
    }
}