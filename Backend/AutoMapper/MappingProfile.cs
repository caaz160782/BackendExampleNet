using AutoMapper;
using Backend.DTOs;
using Backend.Models;

namespace Backend.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<BeerInsertDto, Beer>().
                 ForMember(dto => dto.BeerName, m => m.MapFrom(b => b.Name));

            CreateMap<Beer, BeerDto>()
               .ForMember(dto => dto.Id, m => m.MapFrom(b => b.BeerID))
               .ForMember(dto => dto.Name, m => m.MapFrom(b => b.BeerName));

            CreateMap<BeerUpdateDto,Beer>()
                .ForMember(dto => dto.BeerName, m => m.MapFrom(b => b.Name));
        }
    }    
}
