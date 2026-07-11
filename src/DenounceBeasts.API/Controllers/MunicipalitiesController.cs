using AutoMapper;
using DenounceBeasts.Application.Models.Dtos;
using DenounceBeasts.Application.Models.Responses;
using DenounceBeasts.Application.Services;
using DenounceBeasts.Infraestructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DenunciaUnaBestia.Api.Controllers
{

    [ApiController]
    [Route("api/municipalities")]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MunicipalityServices _municipalityServices;

        public MunicipalitiesController(UnitOfWork unitOfWork, IMapper mapper,
            MunicipalityServices municipalityServices)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._municipalityServices = municipalityServices;
        }


        [HttpGet]
        public ApiResponse<List<MunicipalityDto>> GetAll() =>
            _municipalityServices.GetAllMunicipalities();


        [HttpGet("{id}")]
        public ApiResponse<MunicipalityDto> GetById(int id) =>
                        _municipalityServices.GetMunicipalityById(id);

        [HttpPost]
        public ApiResponse<int> Create(MunicipalityDto request) =>
            _municipalityServices.CreateMunicipality(request);

        [HttpPut("{id}")]
        public IActionResult Update(int id, MunicipalityDto municipality)
        {
            var response = _municipalityServices.UpdateMunicipality(id, municipality);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            //return Ok(response);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //var existing = _unitOfWork.Municipality.GetById(id);
            //if (existing == null)
            //{
            //    return NotFound();
            //}
            //_unitOfWork.Municipality.Delete(existing);
            //_unitOfWork.Complete();
            var response = _municipalityServices.DeleteMunicipality(id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return NoContent();
        }
    }
}
