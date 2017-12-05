using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vega.Models;

namespace AThirdCarDealership.Core
{
   public interface IvehicleRepository
    {

        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        void add(Vehicle vehicle);

        void Remove(Vehicle vehicle);
    }
}
