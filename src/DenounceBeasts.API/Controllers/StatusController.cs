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
    [Route("api/status")]
    public class StatusController : BaseController
    {
        private readonly GenericRespository<Status> _statusRepo;
        private readonly UnitOfWork unitOfWork;

        public StatusController(ApplicationDbContext dbContext, GenericRespository<Status> statusRepo, UnitOfWork unitOfWork) : base(dbContext)
        {
            this._statusRepo = statusRepo;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StatusDto>> GetAll()
        {
            var status = _statusRepo.GetAll();
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
            var status = _statusRepo.GetById(id);
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

            _statusRepo.Create(status);
            unitOfWork.Complete();


            return Ok(new { Id = status.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, StatusDto request)
        {
            var existing = _statusRepo.GetById(id);

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
            var existing = _statusRepo.GetById(request.Id);
            if (existing == null)
                return NotFound();

            _statusRepo.Delete(existing);
            unitOfWork.Complete();

            return NoContent();
        }
    }
}

