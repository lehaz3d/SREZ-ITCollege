using System;
using System.Collections.Generic;

namespace WebAPI.DB
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public byte[]? Photo { get; set; }
    }
}
