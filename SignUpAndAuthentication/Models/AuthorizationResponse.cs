using System;
namespace SignUpAndAuthentication.Models
{
    public class AuthorizationResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public DateTime LastLoginOn { get; set; }
        public string Token { get; set; }
    }
}
