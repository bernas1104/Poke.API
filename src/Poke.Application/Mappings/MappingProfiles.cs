using AutoMapper;
using Poke.Application.Dtos.ViewModels;
using Poke.Core.Entities;

namespace Poke.Application.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonViewModel>();
            CreateMap<Training, TrainingViewModel>();
            CreateMap<BaseStats, BaseStatsViewModel>()
                .ForMember(
                    x => x.HitPoints,
                    opt => opt.MapFrom(y => y.HitPoints.Value)
                )
                .ForMember(
                    x => x.Attack,
                    opt => opt.MapFrom(y => y.Attack.Value)
                )
                .ForMember(
                    x => x.Defense,
                    opt => opt.MapFrom(y => y.Defense.Value)
                )
                .ForMember(
                    x => x.SpecialAttack,
                    opt => opt.MapFrom(y => y.SpecialAttack.Value)
                )
                .ForMember(
                    x => x.SpecialDefense,
                    opt => opt.MapFrom(y => y.SpecialDefense.Value)
                )
                .ForMember(
                    x => x.Speed,
                    opt => opt.MapFrom(y => y.Speed.Value)
                );
        }
    }
}
