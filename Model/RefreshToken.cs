﻿namespace Container_App.Model
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }
        public string UserId { get; set; } // Dùng để liên kết với người dùng
    }
}
