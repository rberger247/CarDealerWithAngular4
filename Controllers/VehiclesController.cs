//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using vega.Controllers.Resources;
//using vega.Core.Models;
//using vega.Core;


//namespace vega.Controllers
//{
//    [Route("/api/vehicles")]
//    public class VehiclesController : Controller
//    {
//        private readonly IMapper mapper;
//        private readonly IVehicleRepository repository;
//        private readonly IUnitOfWork unitOfWork;

//        public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork)
//        {
//            this.unitOfWork = unitOfWork;
//            this.repository = repository;
//            this.mapper = mapper;
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
//            vehicle.LastUpdate = DateTime.Now;

//            repository.Add(vehicle);
//            await unitOfWork.CompleteAsync();

//            vehicle = await repository.GetVehicle(vehicle.Id);

//            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

//            return Ok(result);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var vehicle = await repository.GetVehicle(id);

//            if (vehicle == null)
//                return NotFound();

//            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
//            vehicle.LastUpdate = DateTime.Now;

//            await unitOfWork.CompleteAsync();

//            vehicle = await repository.GetVehicle(vehicle.Id);
//            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

//            return Ok(result);
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteVehicle(int id)
//        {
//            var vehicle = await repository.GetVehicle(id, includeRelated: false);

//            if (vehicle == null)
//                return NotFound();

//            repository.Remove(vehicle);
//            await unitOfWork.CompleteAsync();

//            return Ok(id);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetVehicle(int id)
//        {
//            var vehicle = await repository.GetVehicle(id);

//            if (vehicle == null)
//                return NotFound();

//            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

//            return Ok(vehicleResource);
//        }


//    }
//}









using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AThirdCarDealership.Persistence;
using vega.Models;
using AutoMapper;
using vega.Controllers.Resources;

namespace AThirdCarDealership.Controllers
{
    [Produces("application/json")]
    [Route("api/Vehicles")]
    public class VehiclesController : Controller
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;

        public VehiclesController(VegaDbContext context, IMapper mapper)
        {
            _context = context;

            _mapper = mapper;
        }

        // GET: api/Vehicles
        [HttpGet]
        public IEnumerable<Vehicle> GetVehicle()
        {
            return _context.Vehicle;
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await _context.Vehicle.Include(v => v.Features)
                .ThenInclude(vf => vf.Feature).
                Include(v => v.Model).
                ThenInclude(m => m.Make).

                SingleOrDefaultAsync(m => m.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle([FromRoute] int id, [FromBody] VehicleResource vehicleResource)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != vehicleResource.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(vehicleResource).State = EntityState.Modified;

            //try
            //{

                var vehicle = await _context.Vehicles.Include(v => v.Features).
                ThenInclude(vf => vf.Feature).
                Include(m => m.Model).
                SingleOrDefaultAsync(v => v.Id == id);
            await _context.SaveChangesAsync();
            var result =      _mapper.Map<VehicleResource, Vehicle>(vehicleResource);
                
                return Ok(result);
            
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!VehicleExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

           
        }

        // POST: api/Vehicles
        [HttpPost]
        public  async Task<IActionResult> PostVehicle([FromBody] VehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var model = _context.Models.Find(vehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid ModelId");
                return BadRequest(ModelState);


            }
     
            var vehicle = _mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();


            return Ok(vehicle);

        }



        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await _context.Vehicle.SingleOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok(vehicle);
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }
    }
}

