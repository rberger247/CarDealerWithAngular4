using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AThirdCarDealership.Persistence;
using vega.Models;

namespace AThirdCarDealership.Controllers
{
    [Produces("application/json")]
    [Route("api/VehicleFeatures")]
    public class VehicleFeaturesController : Controller
    {
        private readonly VegaDbContext _context;

        public VehicleFeaturesController(VegaDbContext context)
        {
            _context = context;
        }

        // GET: api/VehicleFeatures
        [HttpGet]
        public IEnumerable<VehicleFeature> GetVehicleFeatures()
        {
            return _context.VehicleFeatures;
        }

        // GET: api/VehicleFeatures/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleFeature([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleFeature = await _context.VehicleFeatures.SingleOrDefaultAsync(m => m.VehicleId == id);

            if (vehicleFeature == null)
            {
                return NotFound();
            }

            return Ok(vehicleFeature);
        }

        // PUT: api/VehicleFeatures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleFeature([FromRoute] int id, [FromBody] VehicleFeature vehicleFeature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicleFeature.VehicleId)
            {
                return BadRequest();
            }

            _context.Entry(vehicleFeature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleFeatureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VehicleFeatures
        [HttpPost]
        public async Task<IActionResult> PostVehicleFeature([FromBody] VehicleFeature vehicleFeature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VehicleFeatures.Add(vehicleFeature);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VehicleFeatureExists(vehicleFeature.VehicleId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVehicleFeature", new { id = vehicleFeature.VehicleId }, vehicleFeature);
        }

        // DELETE: api/VehicleFeatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleFeature([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicleFeature = await _context.VehicleFeatures.SingleOrDefaultAsync(m => m.VehicleId == id);
            if (vehicleFeature == null)
            {
                return NotFound();
            }

            _context.VehicleFeatures.Remove(vehicleFeature);
            await _context.SaveChangesAsync();

            return Ok(vehicleFeature);
        }

        private bool VehicleFeatureExists(int id)
        {
            return _context.VehicleFeatures.Any(e => e.VehicleId == id);
        }
    }
}