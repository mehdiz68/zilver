using System;
using System.Collections.Generic;
using System.Text;
using zilver.domain.Entities;

namespace zilver.auth.Models
{
   public class AuthResult
{
    public bool Success { get; set; }
    public string Message { get; set; }

    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

        public DateTime AccessTokenExpires { get; set; }
        public string UserId { get; set; }
}

}
