using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infraestructure;
using DenounceBeasts.Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace DenunciaUnaBestia.Api.Controllers
{
    public class SectorRepository: GenericRespository<Sector>
    {
        private readonly ApplicationDbContext _context;

        public SectorRepository(ApplicationDbContext dbContext): base(dbContext)
        {
            _context = dbContext;
        }
         
        //public IEnumerable<Sector> GetAll()
        //{
        //    var sectors = _context.Sectors.ToList();

        //  return sectors; 
        //}
         
        public IEnumerable<Sector > GetAllWithMunicipality()
        {
            var sectors = _context.Sectors.Include(p => p.Municipality).ToList();
             
           return sectors;
        }

        //public Sector GetById(int id)
        //{
        //    var sector = _context.Sectors
        //        .FirstOrDefault(s => s.Id == id);
        //  return sector;
        //}

        public Sector GetByIdWithMunicipality(int id)
        {
            var sector = _context.Sectors.Include(p=> p.Municipality)
                .FirstOrDefault(s => s.Id == id);
            return sector;
        }

        //public int  Create(Sector sector)
        //{
             
        //    sector.IsActive = true;
        //    _context.Sectors.Add(sector);
        //    _context.SaveChanges();
        //    return sector.Id;
        //}

        public  void Update(int id, Sector request)
        {
            var existing = _context.Sectors
                .FirstOrDefault(s => s.Id == id);

            existing.Name = request.Name;
            existing.MunicipalityId = request.MunicipalityId;
            existing.IsActive = request.IsActive;


            _context.Sectors.Update(existing);
            //_context.SaveChanges();

        }

        //public void Delete(Sector existing)
        //{ 
        //    _context.Sectors.Remove(existing);
        //    _context.SaveChanges();
        //}

        //public void Delete(int id )
        //{
        //    var existing = _context.Sectors.FirstOrDefault(s => s.Id == id);
        //    _context.Sectors.Remove(existing);
        //    _context.SaveChanges();
        //}
    }
}

