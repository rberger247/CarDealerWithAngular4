using Microsoft.EntityFrameworkCore;


using AThirdCarDealership.Models;
using vega.Models;

namespace AThirdCarDealership.Persistence
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options) 
          : base(options)
        {
        }
        public DbSet<Model> Models { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<VehicleFeature> VehicleFeatures { get; set; }
        public DbSet<vega.Models.Vehicle> Vehicle { get; set; }

        public DbSet<AThirdCarDealership.Models.Model> Model { get; set; }
protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<VehicleFeature>().HasKey(

                vf => new { vf.VehicleId, vf.FeatureId });

        }



    }
    
}