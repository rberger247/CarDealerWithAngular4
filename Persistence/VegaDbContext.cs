﻿using Microsoft.EntityFrameworkCore;


using AThirdCarDealership.Models;

namespace AThirdCarDealership.Persistence
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options) 
          : base(options)
        {
        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<AThirdCarDealership.Models.Model> Model { get; set; }
    }
}