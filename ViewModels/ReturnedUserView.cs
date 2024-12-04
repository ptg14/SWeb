﻿namespace SWeb.ViewModels
{
    public class ReturnedUserView
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public string? ProfileImageUrl { get; set; }

    }
}
