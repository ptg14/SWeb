using System.ComponentModel.DataAnnotations.Schema;

namespace SWeb.Models
{
    [Table("comments")]
    public class Comment
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("content")]
        public string Content { get; set; } = string.Empty;

        [Column("postid")]
        public int PostId { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("publishedon")]
        public DateTime PublishedOn { get; set; }

        public User Author { get; set; } = null!;
        public Post Post { get; set; } = null!;
    }
}
