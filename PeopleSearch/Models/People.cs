using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace PeopleSearch.Models
{
    public class People
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Interest { get; set; }
        public string PictureUrl { get; set; }
    }
}
