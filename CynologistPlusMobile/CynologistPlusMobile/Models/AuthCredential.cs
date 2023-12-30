using System;
using System.Collections.Generic;

namespace CynologistPlusMobile.Model
{
    public class AuthCredential
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
    }
}

