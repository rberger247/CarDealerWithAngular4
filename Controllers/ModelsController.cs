using System;
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
    [Route("api/Models")]
    public class ModelsController : Controller
    {
        private readonly VegaDbContext _context;

        public ModelsController(VegaDbContext context)
        {
            _context = context;
        }

        // GET: api/Models
        [HttpGet]
        public IEnumerable<Model> GetModel()
        {
            return _context.Model;
        }

        // GET: api/Models/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = await _context.Model.SingleOrDefaultAsync(m => m.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // PUT: api/Models/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModel([FromRoute] int id, [FromBody] Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
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

        // POST: api/Models
        [HttpPost]
        public async Task<IActionResult> PostModel([FromBody] Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Model.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModel", new { id = model.Id }, model);
        }

        // DELETE: api/Models/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = await _context.Model.SingleOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Model.Remove(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        private bool ModelExists(int id)
        {
            return _context.Model.Any(e => e.Id == id);
        }
    }
}