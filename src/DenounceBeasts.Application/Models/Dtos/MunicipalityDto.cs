using System.ComponentModel.DataAnnotations;

namespace DenounceBeasts.Application.Models.Dtos
{
    public class MunicipalityDto
    {
        
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public string PostalCode { get; set; }
        public bool IsActive { get; set; }

    }
}
