using System;

namespace zilver.domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public string UserId { get; set; } = null!;

        // Navigation
        public ApplicationUser? User { get; set; }
    }
}
