﻿using System.ComponentModel.DataAnnotations;

namespace SimpleAPI.Models
{
    public class User
    {
        [Key]
        public required int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
