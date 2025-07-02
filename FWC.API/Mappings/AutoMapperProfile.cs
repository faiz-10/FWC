using AutoMapper;
using FWC.API.Models;

namespace FWC.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Team, TeamDto>().ReverseMap();
            CreateMap<Player, PlayerDto>().ReverseMap();
        }
    }
}
