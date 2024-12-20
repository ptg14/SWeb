﻿using System.ComponentModel.DataAnnotations;

namespace SWeb.ViewModels
{
    public class SavePostViewModel
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int UserId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
