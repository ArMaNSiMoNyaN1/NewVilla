using AutoMapper;
using Villa.Models.Dto;

namespace Villa.Properties;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Models.Villa, VillaDTO>();
        CreateMap<VillaDTO, Models.Villa>();
        
        CreateMap<VillaDTO, VillaCreatedDTO>().ReverseMap();
        CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();
    }
}