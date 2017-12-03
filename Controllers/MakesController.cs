﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AThirdCarDealership.Models;
using AThirdCarDealership.Persistence;
using AutoMapper;
using AThirdCarDealership.Controllers.Resources;

namespace AThirdCarDealership.Controllers
{
    [Produces("application/json")]
    [Route("api/Makes")]
    public class MakesController : Controller
    {
        private readonly VegaDbContext _context;
        private readonly IMapper mapper;

        public MakesController(VegaDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Makes
//        [HttpGet]
//        public IEnumerable<Make> GetMakes()
//        {

//var makes = await
//            return _context.Makes;
//        }

        [HttpGet]
        public  async Task   <IEnumerable<MakeResource>> GetMakes()
        {

            var makes = await _context.Makes.Include(m => m.Models).ToListAsync();

            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }

        // GET: api/Makes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMake([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var make = await _context.Makes.SingleOrDefaultAsync(m => m.Id == id);

            if (make == null)
            {
                return NotFound();
            }

            return Ok(make);
        }

        // PUT: api/Makes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMake([FromRoute] int id, [FromBody] Make make)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != make.Id)
            {
                return BadRequest();
            }

            _context.Entry(make).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MakeExists(id))
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

        // POST: api/Makes
        [HttpPost]
        public async Task<IActionResult> PostMake([FromBody] Make make)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Makes.Add(make);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMake", new { id = make.Id }, make);
        }

        // DELETE: api/Makes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMake([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var make = await _context.Makes.SingleOrDefaultAsync(m => m.Id == id);
            if (make == null)
            {
                return NotFound();
            }

            _context.Makes.Remove(make);
            await _context.SaveChangesAsync();

            return Ok(make);
        }

        private bool MakeExists(int id)
        {
            return _context.Makes.Any(e => e.Id == id);
        }
    }
}