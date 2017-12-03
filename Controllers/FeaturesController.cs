﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AThirdCarDealership.Models;
using AThirdCarDealership.Persistence;

namespace AThirdCarDealership.Controllers
{
    [Produces("application/json")]
    [Route("api/Features")]
    public class FeaturesController : Controller
    {
        private readonly VegaDbContext _context;

        public FeaturesController(VegaDbContext context)
        {
            _context = context;
        }

        // GET: api/Features
        [HttpGet]
        public IEnumerable<Feature> GetFeatures()
        {
            return _context.Features;
        }

        // GET: api/Features/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeature([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var feature = await _context.Features.SingleOrDefaultAsync(m => m.Id == id);

            if (feature == null)
            {
                return NotFound();
            }

            return Ok(feature);
        }

        // PUT: api/Features/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeature([FromRoute] int id, [FromBody] Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feature.Id)
            {
                return BadRequest();
            }

            _context.Entry(feature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeatureExists(id))
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

        // POST: api/Features
        [HttpPost]
        public async Task<IActionResult> PostFeature([FromBody] Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Features.Add(feature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeature", new { id = feature.Id }, feature);
        }

        // DELETE: api/Features/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeature([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var feature = await _context.Features.SingleOrDefaultAsync(m => m.Id == id);
            if (feature == null)
            {
                return NotFound();
            }

            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();

            return Ok(feature);
        }

        private bool FeatureExists(int id)
        {
            return _context.Features.Any(e => e.Id == id);
        }
    }
}
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//using vega.Models;

//using AThirdCarDealership.Persistence;
//using AThirdCarDealership.Controllers.Resources;
//using AThirdCarDealership.Models;

//namespace vega.Controllers
//{
//    public class FeaturesController : Controller
//    {
//        private readonly VegaDbContext context;
//        private readonly IMapper mapper;
//        public FeaturesController(VegaDbContext context, IMapper mapper)
//        {
//            this.mapper = mapper;
//            this.context = context;
//        }

//        [HttpGet("/api/features")]
//        public async Task<IEnumerable<FeatureResource>> GetFeatures()
//        {
//            var features = await context.Features.ToListAsync();

//            return mapper.Map<List<Feature>, List<FeatureResource>>(features);
//        }
//    }
//}