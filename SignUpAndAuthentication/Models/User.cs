﻿using System;
using System.Collections.Generic;

namespace SignUpAndAuthentication.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        //Hashed Value
        public string Password { get; set; }

        public List<Telephone> Telephones { get; set; }
    }

    public class Telephone
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
    }
}
