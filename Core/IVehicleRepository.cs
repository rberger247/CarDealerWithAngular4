//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using vega.Models;

//namespace AThirdCarDealership.Core
//{
//   public interface IvehicleRepository
//    {

//        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
//        void add(Vehicle vehicle);

//        void Remove(Vehicle vehicle);
//    }
//}
using AThirdCarDealership.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Core.Models;
using vega.Models;

namespace vega.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
        Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery filter);
        //Task<IEnumerable<Vehicle>> GetVehicles(Filter filter);
    }
}