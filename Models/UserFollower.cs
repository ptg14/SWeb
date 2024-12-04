using System.ComponentModel.DataAnnotations.Schema;

namespace SWeb.Models
{
    [Table("userfollowers")]
    public class UserFollower
    {
        [Column("followerid")]
        public int FollowerId { get; set; }

        public User Follower { get; set; } = null!;

        [Column("followedid")]
        public int FollowedId { get; set; }

        public User Followed { get; set; } = null!;
    }
}
