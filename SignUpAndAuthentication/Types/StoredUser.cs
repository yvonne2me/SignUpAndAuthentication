using System;
using SignUpAndAuthentication.Models;

namespace SignUpAndAuthentication.Types
{
    public class StoredUser
    {
        public Guid Id { get; set; }
        public User user { get; set; }
        public AuthorizationResponse signUpResponse { get; set; }
    }
}
