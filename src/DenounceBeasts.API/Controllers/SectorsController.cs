using DenounceBeasts.API.Models.Dtos;
using DenounceBeasts.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DenunciaUnaBestia.Api.Controllers
{
    [ApiController]
    [Route("api/sectors")]
    public class SectorsController : ControllerBase
    {
        private static readonly List<Sector> _sectors = new List<Sector>
        {
            new Sector { Id = 1, Name = "Zona Colonial", MunicipalityId = 1, IsActive = true },
            new Sector { Id = 2, Name = "Gascue", MunicipalityId = 1, IsActive = true },
            new Sector { Id = 3, Name = "Cienfuegos", MunicipalityId = 2, IsActive = true }
        };

        [HttpGet] // GET: api/sectors
        public ActionResult<IEnumerable<SectorDto>> GetAll()
        {
            var response = _sectors.Select(s => new SectorDto
            {
                Id = s.Id,
                Name = s.Name,
                MunicipalityId = s.MunicipalityId,
                IsActive = s.IsActive
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")] // GET: api/sectors/5
        public ActionResult<SectorDto> GetById(int id)
        {
            var sector = _sectors.FirstOrDefault(s => s.Id == id);
            if (sector == null)
                return NotFound();
            var response = new SectorDto
            {
                Id = sector.Id,
                Name = sector.Name,
                MunicipalityId = sector.MunicipalityId,
                IsActive = sector.IsActive
            };
            return Ok(response);
        }

        [HttpPost] // POST: api/sectors
        public ActionResult<int> Create(CreateSectorDto request)
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

            int newId = _sectors.Any() ? _sectors.Max(s => s.Id) + 1 : 1;
          
            var sector = new Sector
            {
                Name = request.Name,
                MunicipalityId = request.MunicipalityId
            };


            sector.Id = newId;
            sector.IsActive = true; // siempre creamos como activo
            _sectors.Add(sector);
            return Ok(new { Id = sector.Id });
           // return CreatedAtAction(nameof(GetById), new { id = sector.Id }, sector);
        }

        [HttpPut("{id}")] // PUT: api/sectors/5
        public IActionResult Update(int id, UpdateSectorDto request)
        {
            var existing = _sectors.FirstOrDefault(s => s.Id == id);
            if (existing == null)
                return NotFound();
            // Actualizar campos (excepto Id)
            existing.Name = request.Name;
            existing.MunicipalityId = request.MunicipalityId;
            existing.IsActive = request.IsActive;
            return NoContent();
        }

        //[HttpDelete("{id}")] // DELETE: api/sectors/5
        [HttpDelete] // DELETE: api/sectors/5
        public IActionResult Delete(DeleteSectorDto request)
        {
            var existing = _sectors.FirstOrDefault(s => s.Id == request.Id);
            if (existing == null)
                return NotFound();
            _sectors.Remove(existing);
            return NoContent();
        }
    }
}

