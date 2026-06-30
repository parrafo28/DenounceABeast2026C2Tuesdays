using DenounceBeasts.API.Controllers;
using DenounceBeasts.API.Data;
using DenounceBeasts.API.Models.Dtos;
using DenounceBeasts.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DenunciaUnaBestia.Api.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class StatusController : BaseController
    {
        public StatusController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        [HttpGet]
        public ActionResult<IEnumerable<StatusDto>> GetAll()
        {
            var status = Context.Status.ToList();
            var response = status.Select(s => new StatusDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<StatusDto> GetById(int id)
        {
            var status = Context.Status
                .FirstOrDefault(s => s.Id == id);
            if (status == null)
            {
                return NotFound();
            }
            var response = new StatusDto
            {
                Id = status.Id,
                Name = status.Name
            };
            return Ok(response);
        }

        [HttpPost]  
        public ActionResult<int> Create(StatusDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Name of status is required.");
            }
             

            var status = new Status
            {
                Name = request.Name
            };
             
            Context.Status.Add(status);
            Context.SaveChanges(); 

            return Ok(new { Id = status.Id });
        }

        [HttpPut("{id}")]  
        public IActionResult Update(int id, StatusDto request)
        {
            var existing = Context.Status
                .FirstOrDefault(s => s.Id == id);
            if (existing == null)
                return NotFound(); 
            existing.Name = request.Name; 

            Context.Status.Update(existing);
            Context.SaveChanges();

            return NoContent();
        }
          
        [HttpDelete]  
        public IActionResult Delete(StatusDto request)
        {
            var existing = Context.Status.FirstOrDefault(s => s.Id == request.Id);
            if (existing == null)
                return NotFound();
            Context.Status.Remove(existing);
            Context.SaveChanges();
            return NoContent();
        }
    }
}

