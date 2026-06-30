using AutoMapper;
using DenounceBeasts.API.Models.Dtos;
using DenounceBeasts.API.Models.Entities;

namespace DenounceBeasts.API.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Municipality, MunicipalityDto>().ReverseMap();
            //CreateMap<MunicipalityDto, Municipality>()  ;
            CreateMap<Sector, SectorDto>()
                .ForMember(dest => dest.MunicipalityName, 
                opt => opt.MapFrom(src => src.Municipality.Name))
                //.ForMember(dest => dest.Namex, opt=> opt.MapFrom(src=> src.Name))  ;

            CreateMap<SectorDto, Sector>();
            CreateMap<CreateSectorDto, Sector>();
            //CreateMap<UpdateSectorDto, Sector>();
            //CreateMap<DeleteSectorDto, Sector>();
        }
    }
}
