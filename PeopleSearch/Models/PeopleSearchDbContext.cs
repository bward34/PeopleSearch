using System;
using Microsoft.EntityFrameworkCore;
namespace PeopleSearch.Models
{
    public class PeopleSearchDbContext : DbContext
    {
        public PeopleSearchDbContext(DbContextOptions<PeopleSearchDbContext> contextOptions)
            : base(contextOptions)
        {
        }
    }
}
