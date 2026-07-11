using DenounceBeasts.Domain.Core;
using DenounceBeasts.Domain.Entities;

namespace DenounceBeasts.Infraestructure.Repository
{
    public class GenericRespository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRespository(ApplicationDbContext dbContext)
        {
            this._context = dbContext;
        }

        public IEnumerable<T> GetAll()
        {
            var entities = _context.Set<T>().ToList();
            return entities;
        }

        public T GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            return entity;
        }

        public int Create(T entity)
        {

            _context.Set<T>().Add(entity);
            //_context.SaveChanges();

            return entity.Id;
        }

        //public void Update(int id, T request)
        //{
        //    var existing = _context.ComplaintTypes
        //        .FirstOrDefault(s => s.Id == id);

        //    existing.Name = request.Name;

        //    _context.ComplaintTypes.Update(existing);
        //    _context.SaveChanges();
        //}

        public void Update(T existing)
        {
            _context.Set<T>().Update(existing);
            //_context.SaveChanges();
        }

        public void Delete(T existing)
        {
            _context.Set<T>().Remove(existing);
            //_context.SaveChanges();
        }

        public void Delete(int id)
        {
            var existing = _context.Set<T>().FirstOrDefault(s => s.Id == id);

            _context.Set<T>().Remove(existing);
            //_context.SaveChanges();
        }
    }
}
