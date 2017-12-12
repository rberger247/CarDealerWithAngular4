

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using AThirdCarDealership.Persistence;
//using vega.Models;
//using AutoMapper;
//using vega.Controllers.Resources;
//using AThirdCarDealership.Core;
//using AThirdCarDealership.Controllers.Resources;




//namespace AThirdCarDealership.Controllers
//{
//    [Produces("application/json")]
//    [Route("api/Vehicles")]
//    public class VehiclesController : Controller
//    {
//        private readonly VegaDbContext _context;
//        private readonly IMapper _mapper;
//        private readonly IvehicleRepository _repository;

//        private readonly IUnitOfWork unitOfWork;

//        public VehiclesController(VegaDbContext context, IMapper mapper, IvehicleRepository repository, IUnitOfWork unitOfWork)
//        {
//            _repository = repository;

//            _context = context;

//            _mapper = mapper;
//            this.unitOfWork = unitOfWork;

//        }


//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetVehicle([FromRoute] int id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//           var   vehicle = await _repository.GetVehicle(id);


//            if (vehicle == null)
//                return NotFound();


//            var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);



//            return Ok(vehicleResource);
//        }

//        // PUT: api/Vehicles/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateVehicle([FromRoute] int id)
//        {

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var vehicle = await _repository.GetVehicle(id);

//            if (vehicle == null)
//                return NotFound();

//            var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);

//            return Ok(vehicleResource);

//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
//            vehicle.LastUpdate = DateTime.Now;

//            _repository.add(vehicle);
//            await unitOfWork.CompleteAsync();

//            vehicle = await _repository.GetVehicle(vehicle.Id);

//            var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);

//            return Ok(result);
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteVehicle(int id)
//        {
//            var vehicle = await _repository.GetVehicle(id, includeRelated: false);

//            if (vehicle == null)
//                return NotFound();

//            _repository.Remove(vehicle);
//            await unitOfWork.CompleteAsync();

//            return Ok(id);
//        }
//    }
//    }

//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using vega.Controllers.Resources;

//using vega.Models;
//using AThirdCarDealership.Core;
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
//            throw new Exception();

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


//        [HttpGet]
//        public async Task<IEnumerable<VehicleResource>> GetVehicles()
//        {

//            var vehicles = await repository.GetVehicles();

//            return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicles);
//        }
//    }



//}




using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Core.Models;
using vega.Core;
using vega.Models;
using AThirdCarDealership.Core;
using AThirdCarDealership.Core.Models;
using AThirdCarDealership.Controllers.Resources;

namespace vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            repository.Add(vehicle);
            await unitOfWork.CompleteAsync();

            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await unitOfWork.CompleteAsync();

            vehicle = await repository.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id, includeRelated: false);

            if (vehicle == null)
                return NotFound();

            repository.Remove(vehicle);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
        //[HttpGet]
        //public async Task<IEnumerable<VehicleResource>> GetVehicles()
        //{

        //    var vehicles = await repository.GetVehicles();

        //    return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicles);
        //}
        [HttpGet]
        public async Task<IEnumerable<VehicleResource>> GetVehicles(FilterResource filterResource)
        {
            var filter = mapper.Map<FilterResource, Filter>(filterResource);
            var vehicles = await repository.GetVehicles(filter);

            return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicles);
        }
    }

}
    




















