﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Backend.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext (DbContextOptions <StoreContext> options): base(options) {  }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brand> Brands { get; set; }


    }
}
