//using AThirdCarDealership.Core.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;


//namespace AThirdCarDealership.Core
//{

//        public interface IVehicleRepository
//        {
//            Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
//            void Add(Vehicle vehicle);
//            void Remove(Vehicle vehicle);
//        }

//}

using AThirdCarDealership.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vega.Models;

namespace AThirdCarDealership.Core
{

    public class VehicleRepository : IvehicleRepository
    {

        private readonly  VegaDbContext context;
        public VehicleRepository(VegaDbContext context)
        {
       
               this.context = context;
        }

        public VegaDbContext Context { get; }

        public async Task <Vehicle> GetVehicle(int id, bool includeRelated = true)
        {

            if (!includeRelated)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
           
              .Include(v => v.Model)
                .ThenInclude(m => m.Make).Include(v => v.Features)
            .ThenInclude(vf => vf.Feature)
              .SingleOrDefaultAsync(v => v.Id == id);


        }

     public   void Remove(Vehicle vehicle) {
            context.Vehicle.Remove(vehicle);

        }
     public   void add(Vehicle vehicle)
        {
            context.Vehicle.Add(vehicle);

        }




    }


}
