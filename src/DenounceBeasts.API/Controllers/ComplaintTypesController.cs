using DenounceBeasts.API.Controllers;
using DenounceBeasts.Application.Models.Dtos;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infraestructure;
using DenounceBeasts.Infraestructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DenunciaUnaBestia.Api.Controllers
{
    [ApiController]
    [Route("api/complaintTypes")]
    public class ComplaintTypesController : BaseController
    {
        //private readonly ComplaintTypeRepository _complaintTypeRepository;
        private readonly UnitOfWork unitOfWork;

        public ComplaintTypesController(ApplicationDbContext dbContext,
            //ComplaintTypeRepository complaintTypeRepository,
            //GenericRespository<Status> genericRepository,
            //SectorRepository sectorRepository,
            UnitOfWork unitOfWork) : base(dbContext)
        {
            //this._complaintTypeRepository = complaintTypeRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ComplaintTypeDto>> GetAll()
        {
            var complaintTypes = unitOfWork.ComplaintType.GetAll();
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
            var complaintType = unitOfWork.ComplaintType.GetById(id);
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

            //Context.ComplaintTypes.Add(complaintType);
            //Context.SaveChanges();
            unitOfWork.ComplaintType.Create(complaintType);
            unitOfWork.Complete();

            return Ok(new { Id = complaintType.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ComplaintTypeDto request)
        {
            //var existing = Context.ComplaintTypes
            //    .FirstOrDefault(s => s.Id == id);
            var existing = unitOfWork.ComplaintType.GetById(id);
            if (existing == null)
                return NotFound();
            existing.Name = request.Name;

            unitOfWork.ComplaintType.Update(id, existing);
            //Context.ComplaintTypes.Update(existing);
            //Context.SaveChanges();
            unitOfWork.Complete();

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(ComplaintTypeDto request)
        {
            var existing = unitOfWork.ComplaintType.GetById(request.Id);

            if (existing == null)
                return NotFound();
            //Context.ComplaintTypes.Remove(existing);
            //Context.SaveChanges();
            unitOfWork.ComplaintType.Delete(existing);
            unitOfWork.Complete();

            return NoContent();
        }
    }
}

