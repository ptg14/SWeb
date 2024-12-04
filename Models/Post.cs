using System.ComponentModel.DataAnnotations.Schema;

namespace SWeb.Models
{
    [Table("posts")]
    public class Post
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("content")]
        public string Content { get; set; } = string.Empty;

        [Column("userid")]
        public int UserId { get; set; }

        [Column("imageurl")]
        public string? ImageUrl { get; set; }

        [Column("publishedon")]
        public DateTime PublishedOn { get; set; }

        public User Author { get; set; } = null!;
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
