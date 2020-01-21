using System;
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.DTO
{
    public class UserForListDTO
    {
         public int Id { get; set; }

        public string Username { get; set; }

        public string Gender { get; set; }

         public int Age { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastACtive { get; set; }

        public string Introduction { get; set; }

        public string Country { get; set; }

        public string PhotoUrl { get; set; } 
    }
}