using DenounceBeasts.Domain.Entities;
using DenunciaUnaBestia.Api.Controllers;

namespace DenounceBeasts.Infraestructure.Repository
{
    public class UnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context,
          SectorRepository sectorRepository, 
          ComplaintTypeRepository complaintType, 
          GenericRespository<Status> status,
          GenericRespository<Municipality> municipalities)
        {
            _context = context;
            Sector = sectorRepository;
            ComplaintType = complaintType;
            Status = status;
            Municipality = municipalities;
        }

        public SectorRepository Sector { get; set; }
        public ComplaintTypeRepository ComplaintType { get; set; }
        public GenericRespository<Status> Status { get; set; }
        public GenericRespository<Municipality> Municipality{ get; set; }


        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
