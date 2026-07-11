using DenounceBeasts.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.Domain.Entities
{
    public class Municipality: BaseEntity
    {
        
        //public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public string PostalCode { get; set; }
        public bool IsActive { get; set; }

        public List<Sector> Sectors { get; set; }
    }
}
