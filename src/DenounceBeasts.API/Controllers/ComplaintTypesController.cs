using DenounceBeasts.API.Controllers;
using DenounceBeasts.API.Data;
using DenounceBeasts.API.Models.Dtos;
using DenounceBeasts.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DenunciaUnaBestia.Api.Controllers
{
    [ApiController]
    [Route("api/complaintTypes")]
    public class ComplaintTypesController : BaseController
    {
        public ComplaintTypesController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        [HttpGet]
        public ActionResult<IEnumerable<ComplaintTypeDto>> GetAll()
        {
            var complaintTypes = Context.ComplaintTypes.ToList();
            var response = complaintTypes.Select(s => new ComplaintTypeDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<ComplaintTypeDto> GetById(int id)
        {
            var complaintType = Context.ComplaintTypes
                .FirstOrDefault(s => s.Id == id);
            if (complaintType == null)
            {
                return NotFound();
            }
            var response = new ComplaintTypeDto
            {
                Id = complaintType.Id,
                Name = complaintType.Name
            };
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<int> Create(ComplaintTypeDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Name of complaintType is required.");
            }


            var complaintType = new ComplaintType
            {
                Name = request.Name
            };

            Context.ComplaintTypes.Add(complaintType);
            Context.SaveChanges();

            return Ok(new { Id = complaintType.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ComplaintTypeDto request)
        {
            var existing = Context.ComplaintTypes
                .FirstOrDefault(s => s.Id == id);
            if (existing == null)
                return NotFound();
            existing.Name = request.Name;

            Context.ComplaintTypes.Update(existing);
            Context.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(ComplaintTypeDto request)
        {
            var existing = Context.ComplaintTypes.FirstOrDefault(s => s.Id == request.Id);
            if (existing == null)
                return NotFound();
            Context.ComplaintTypes.Remove(existing);
            Context.SaveChanges();
            return NoContent();
        }
    }
}

