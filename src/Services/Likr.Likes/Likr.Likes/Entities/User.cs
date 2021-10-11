namespace Likr.Likes.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Biography { get; set; }
    }
}