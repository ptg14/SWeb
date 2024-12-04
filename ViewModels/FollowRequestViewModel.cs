using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SWeb.ViewModels
{
    public class FollowRequestViewModel
    {
        [Required]
        public int senderId { get; set; } 
        [Required]
        public int receiverId { get; set; }
    }
}
