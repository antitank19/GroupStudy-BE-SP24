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
using AutoMapper;
using ShareResource.DTO;
using AutoMapper.QueryableExtensions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly IServiceWrapper services;
        private readonly IMapper mapper;

        public SubjectsController(IServiceWrapper services, IMapper mapper)
        {
            this.services = services;
            this.mapper = mapper;
        }

        // GET: api/Subjects
        [SwaggerOperation(
            Summary = $"[{Actor.Student_Parent}/{Finnished.True}/{Auth.True}] Get list of subjects"
        )]
        [Authorize(Roles = Actor.Student_Parent)]
        [HttpGet]
        public async Task<IActionResult> GetSubject()
        {
            IQueryable<Subject> list = services.Subjects.GetList();
          if (list == null|| !list.Any())
          {
              return NotFound();
          }

            return Ok(list.ProjectTo<SubjectGetDto>(mapper.ConfigurationProvider));
        }

        //// GET: api/Subjects/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Subject>> GetSubject(int id)
        //{
        //    if (services.Subject == null)
        //    {
        //        return NotFound();
        //    }
        //    var subject = await services.Subject.FindAsync(id);

        //    if (subject == null)
        //    {
        //        return NotFound();
        //    }

        //    return subject;
        //}

        //// PUT: api/Subjects/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSubject(int id, Subject subject)
        //{
        //    if (id != subject.Id)
        //    {
        //        return BadRequest();
        //    }

        //    services.Entry(subject).State = EntityState.Modified;

        //    try
        //    {
        //        await services.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SubjectExists(id))
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

        //// POST: api/Subjects
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Subject>> PostSubject(Subject subject)
        //{
        //    if (services.Subject == null)
        //    {
        //        return Problem("Entity set 'TempContext.Subject'  is null.");
        //    }
        //    services.Subject.Add(subject);
        //    await services.SaveChangesAsync();

        //    return CreatedAtAction("GetSubject", new { id = subject.Id }, subject);
        //}

        //// DELETE: api/Subjects/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSubject(int id)
        //{
        //    if (services.Subject == null)
        //    {
        //        return NotFound();
        //    }
        //    var subject = await services.Subject.FindAsync(id);
        //    if (subject == null)
        //    {
        //        return NotFound();
        //    }

        //    services.Subject.Remove(subject);
        //    await services.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool SubjectExists(int id)
        //{
        //    return (services.Subject?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
