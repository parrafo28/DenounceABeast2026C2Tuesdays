using AutoMapper;
using DenounceBeasts.Application.Models.Dtos;
using DenounceBeasts.Domain.Entities;

namespace DenounceBeasts.Application.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Municipality, MunicipalityDto>().ReverseMap();
            //CreateMap<MunicipalityDto, Municipality>()  ;
            CreateMap<Sector, SectorDto>()
                .ForMember(dest => dest.MunicipalityName,
                opt => opt.MapFrom(src => src.Municipality.Name));
                //.ForMember(dest => dest.Namex, opt=> opt.MapFrom(src=> src.Name))  ;

            CreateMap<SectorDto, Sector>();
            CreateMap<CreateSectorDto, Sector>();
            //CreateMap<UpdateSectorDto, Sector>();
            //CreateMap<DeleteSectorDto, Sector>();
        }
    }
}
