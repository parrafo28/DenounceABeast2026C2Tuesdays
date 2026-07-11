using DenounceBeasts.Domain.Entities;
using DenounceBeasts.Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace DenunciaUnaBestia.Api.Controllers
{

    public class ComplaintTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public ComplaintTypeRepository(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }

        public IEnumerable<ComplaintType> GetAll()
        {
            var complaintTypes = _context.ComplaintTypes.ToList();
            return complaintTypes;
        }

        public ComplaintType GetById(int id)
        {
            var complaintType = _context.ComplaintTypes
                .FirstOrDefault(s => s.Id == id);
            return complaintType;
        }

        public int Create(ComplaintType request)
        {
            var complaintType = new ComplaintType
            {
                Name = request.Name
            };

            _context.ComplaintTypes.Add(complaintType);
            //_context.SaveChanges();

            return complaintType.Id;
        }

        public void Update(int id, ComplaintType request)
        {
            var existing = _context.ComplaintTypes
                .FirstOrDefault(s => s.Id == id);

            existing.Name = request.Name;

            _context.ComplaintTypes.Update(existing);
            //_context.SaveChanges();
        }

        public void Delete(ComplaintType existing)
        { 
            _context.ComplaintTypes.Remove(existing);
            //_context.SaveChanges();
        }

        public void Delete(int id)
        {
            var existing = _context.ComplaintTypes.FirstOrDefault(s => s.Id == id);

            _context.ComplaintTypes.Remove(existing);
            //_context.SaveChanges();
        }
    }
}

