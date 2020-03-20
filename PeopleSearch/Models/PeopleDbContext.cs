using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PeopleSearch.Models
{
    public class PeopleDbContext : DbContext
    {
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) :
            base(options)
        {
        }

        public DbSet<People> People { get; set; }

    }
}
