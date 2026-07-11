using AutoMapper;
using DenounceBeasts.API.Controllers;
using DenounceBeasts.Application.Models.Dtos;
using DenounceBeasts.Application.Models.Responses;
using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infraestructure;
using DenounceBeasts.Infraestructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DenunciaUnaBestia.Api.Controllers
{
    [ApiController]
    [Route("api/sectors")]
    public class SectorsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SectorRepository _sectorRepo;
        private readonly UnitOfWork unitOfWork;

        public SectorsController(ApplicationDbContext dbContext, IMapper mapper, SectorRepository sectorRepo, UnitOfWork unitOfWork) : base(dbContext)
        {
            _context = dbContext;
            _mapper = mapper;
            this._sectorRepo = sectorRepo;
            this.unitOfWork = unitOfWork;
        }

        //private static readonly List<Sector> _sectors = new List<Sector>
        //{
        //    new Sector { Id = 1, Name = "Zona Colonial", MunicipalityId = 1, IsActive = true },
        //    new Sector { Id = 2, Name = "Gascue", MunicipalityId = 1, IsActive = true },
        //    new Sector { Id = 3, Name = "Cienfuegos", MunicipalityId = 2, IsActive = true }
        //};

        [HttpGet] // GET: api/sectors
        //public ActionResult<IEnumerable<SectorDto>> GetAll()
        //public IEnumerable<SectorDto> GetAll()
        public ApiResponse<IEnumerable<SectorDto>> GetAll()
        {
            var sectors = _sectorRepo.GetAll();
            //var response = sectors.Select(s => new SectorDto
            //{
            //    Id = s.Id,
            //    Name = s.Name,
            //    MunicipalityId = s.MunicipalityId,
            //    IsActive = s.IsActive,
            //    RandomNumber = s.RandomNumber
            //}).ToList();
            var response = _mapper.Map<List<SectorDto>>(sectors);

            return ApiResponse<IEnumerable<SectorDto>>.SuccessResponse(response);
            //return response;
            //return Ok(response);
        }
        [HttpGet] // GET: api/sectors
        [Route("with-municipality")] // GET: api/sectors/active
        //public ActionResult<ApiResponse<IEnumerable<SectorDto>>> GetAllWithMunicipality()
        public ApiResponse<IEnumerable<SectorDto>> GetAllWithMunicipality()
        {
            //var sectors = _context.Sectors.ToList();
            var sectors = _sectorRepo.GetAllWithMunicipality();

            // var responseList = new List<SectorDto>();

            //foreach (var sector in sectors)
            //{
            //    var municipalityName = _context.Municipalities
            //        .Where(m => m.Id == sector.MunicipalityId)
            //        .Select(m => m.Name)
            //        .FirstOrDefault() ?? "Unknown";
            //    var sectorDto = new SectorDto
            //    {
            //        Id = sector.Id,
            //        Name = sector.Name,
            //        MunicipalityId = sector.MunicipalityId,
            //        IsActive = sector.IsActive,
            //        MunicipalityName = municipalityName
            //    };
            //    //if (sector.Municipality != null)
            //    //{
            //    //    sectorDto.MunicipalityName = sector.Municipality.Name;
            //    //}
            //    //else
            //    //{
            //    //    sectorDto.MunicipalityName = "Unknown";
            //    //}
            //    sectorDto.MunicipalityName = sector.Municipality != null ? sector.Municipality.Name : "Unknown";
            //    sectorDto.MunicipalityName = sector.Municipality == null ? "Unknow" : sector.Municipality.Name;

            //    responseList.Add(sectorDto);
            //}

            //var response = sectors.Select(s => new SectorDto
            //{
            //    Id = s.Id,
            //    Name = s.Name,
            //    MunicipalityId = s.MunicipalityId,
            //    IsActive = s.IsActive,
            //    RandomNumber = s.RandomNumber,
            //    //MunicipalityName = _context.Municipalities
            //    //    .Where(m => m.Id == s.MunicipalityId)
            //    //    .Select(m => m.Name)
            //    //    .FirstOrDefault() ?? "Unknown"
            //    MunicipalityName = s.Municipality != null ? s.Municipality.Name : "Unknown"
            //}).ToList();

            var response = _mapper.Map<List<SectorDto>>(sectors);
            //return Ok(response);
            return ApiResponse<IEnumerable<SectorDto>>.SuccessResponse(response);
        }

        [HttpGet("{id}")] // GET: api/sectors/5
        //public ActionResult<ApiResponse< SectorDto> > GetById(int id)
        public ApiResponse<SectorDto> GetById(int id)
        {
            var sector = _sectorRepo.GetById(id);
            if (sector == null)
            {
                return ApiResponse<SectorDto>.ErrorResponse("Sector not found", 404);
                //return NotFound();
            }
            //var response = new SectorDto
            //{
            //    Id = sector.Id,
            //    Name = sector.Name,
            //    MunicipalityId = sector.MunicipalityId,
            //    IsActive = sector.IsActive                ,
            //    RandomNumber = sector.RandomNumber,
            //};
            var response = _mapper.Map<SectorDto>(sector);
            //return Ok(response);
            //return Ok(new { bbb= "bbbdbd" });
            return ApiResponse<SectorDto>.SuccessResponse(response);
        }

        [HttpPost] // POST: api/sectors
        public ActionResult<ApiResponse<int>> Create(CreateSectorDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Name of sector is required.");
            }
            if (request.MunicipalityId <= 0)
            {
                return BadRequest("MunicipalityId must be provided and positive.");
            }
            // (Podríamos validar aquí que exista un Municipio con ese Id consultando la lista de municipios, 
            //  pero omitiremos esa comprobación en esta versión inicial.)


            //var sector = new Sector
            //{
            //    Name = request.Name,
            //    MunicipalityId = request.MunicipalityId
            //};

            var sector = _mapper.Map<Sector>(request);
            sector.IsActive = true; // siempre creamos como activo
            //_context.Sectors.Add(sector);
            //_context.SaveChanges(); // Esto asignará un Id al sector
            _sectorRepo.Create(sector);
            unitOfWork.Complete();
            //return Ok(new { Id = sector.Id });

            // return CreatedAtAction(nameof(GetById), new { id = sector.Id }, sector);
            return ApiResponse<int>.SuccessResponse(sector.Id, 201);
        }

        [HttpPut("{id}")] // PUT: api/sectors/5
        public IActionResult Update(int id, UpdateSectorDto request)
        {
            //var existing = _context.Sectors
            //    .FirstOrDefault(s => s.Id == id);
            var existing = _sectorRepo.GetById(id);
            if (existing == null)
                return NotFound();
            // Actualizar campos (excepto Id)
            existing.Name = request.Name;
            existing.MunicipalityId = request.MunicipalityId;
            existing.IsActive = request.IsActive;

            _sectorRepo.Update(id, existing);
            unitOfWork.Complete();

            //_context.Sectors.Update(existing);
            //_context.SaveChanges();

            return NoContent();
        }

        //[HttpDelete("{id}")] // DELETE: api/sectors/5
        [HttpDelete] // DELETE: api/sectors/5
        public IActionResult Delete(DeleteSectorDto request)
        {
            var existing = _sectorRepo.GetById(request.Id);
            if (existing == null)
                return NotFound();
            //_context.Sectors.Remove(existing);
            //_context.SaveChanges();
            _sectorRepo.Delete(request.Id);
            unitOfWork.Complete();

            //_sectorRepo.Delete(existing);
            return NoContent();
        }
    }
}

