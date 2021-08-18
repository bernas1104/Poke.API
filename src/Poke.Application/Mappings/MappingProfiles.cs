using AutoMapper;
using Poke.Application.Dtos.ViewModels;
using Poke.Core.Entities;

namespace Poke.Application.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PokemonViewModel, Pokemon>().ReverseMap();
        }
    }
}
