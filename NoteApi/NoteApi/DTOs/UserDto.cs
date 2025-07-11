﻿namespace NoteApi.DTOs
{
    public class RegisterDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; } 
    }
    public class LoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; } 
    }
}
