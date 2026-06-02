namespace DenounceBeasts.API.Models.Dtos
{
    public class CreateSectorDto
    { 
        public string Name { get; set; } = string.Empty;
        public int MunicipalityId { get; set; } 
    } 
}
