using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API;
using DataLayer.DBObject;
using ServiceLayer.Interface;
using APIExtension.Const;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IServiceWrapper services;

        public ClassesController(IServiceWrapper services)
        {
            this.services = services;
        }

        // GET: api/Classes
        [SwaggerOperation(
            Summary = $"[{Actor.Student_Parent}/{Finnished.True}/{Auth.True}] Get list of classes"
        )]
        [Authorize(Roles = Actor.Student_Parent)]
        [HttpGet]
        public async Task<IActionResult> GetClass()
        {
            IQueryable<Class> list = services.Classes.GetList();
          if (!list.Any())
          {
              return NotFound();
          }

            return Ok(list);
        }

        //// GET: api/Classes/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Class>> GetClass(int id)
        //{
        //  if (_context.Class == null)
        //  {
        //      return NotFound();
        //  }
        //    var @class = await _context.Class.FindAsync(id);

        //    if (@class == null)
        //    {
        //        return NotFound();
        //    }

        //    return @class;
        //}

        //// PUT: api/Classes/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutClass(int id, Class @class)
        //{
        //    if (id != @class.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(@class).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ClassExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Classes
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Class>> PostClass(Class @class)
        //{
        //  if (_context.Class == null)
        //  {
        //      return Problem("Entity set 'TempContext.Class'  is null.");
        //  }
        //    _context.Class.Add(@class);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetClass", new { id = @class.Id }, @class);
        //}

        //// DELETE: api/Classes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteClass(int id)
        //{
        //    if (_context.Class == null)
        //    {
        //        return NotFound();
        //    }
        //    var @class = await _context.Class.FindAsync(id);
        //    if (@class == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Class.Remove(@class);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ClassExists(int id)
        //{
        //    return (_context.Class?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
