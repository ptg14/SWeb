using System.ComponentModel.DataAnnotations;

namespace SWeb.ViewModels
{
    public class UserFriendViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
    }
}
