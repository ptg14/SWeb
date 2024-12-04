using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWeb.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("profileimageurl")]
        public string? ProfileImageUrl { get; set; }

        [Column("emailconfirmed")]
        public bool EmailConfirmed { get; set; }

        [Phone]
        [Column("phone")]
        public string? Phone { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        public List<UserFollower> Following { get; set; } = new List<UserFollower>();
        public List<UserFollower> Followers { get; set; } = new List<UserFollower>();
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
