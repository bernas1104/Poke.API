using AutoMapper;
using EnumsNET;
using Poke.Core.Commands.Responses;
using Poke.Core.Entities;
using Poke.Core.ValueObjects;

namespace Poke.API.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, CreatePokemonResponse>()
                .ForMember(
                    x => x.FirstType,
                    y => y.MapFrom(
                        x => x.FirstType.AsString(EnumFormat.Description)
                    )
                )
                .ForMember(
                    x => x.SecondType,
                    y => y.MapFrom(
                        x => x.SecondType.AsString(EnumFormat.Description)
                    )
                );

            CreateMap<BaseStats, CreateBaseStatsResponse>()
                .ForMember(
                    x => x.HitPoints, y => y.MapFrom(x => x.HitPoints.Value)
                )
                .ForMember(
                    x => x.Attack, y => y.MapFrom(x => x.Attack.Value)
                )
                .ForMember(
                    x => x.Defense, y => y.MapFrom(x => x.Defense.Value)
                )
                .ForMember(
                    x => x.SpecialAttack, y => y.MapFrom(x => x.SpecialAttack.Value)
                )
                .ForMember(
                    x => x.SpecialDefense, y => y.MapFrom(x => x.SpecialDefense.Value)
                )
                .ForMember(
                    x => x.Speed, y => y.MapFrom(x => x.Speed.Value)
                );

            CreateMap<Training, CreateTrainingResponse>()
                .ForMember(
                    x => x.GrowthRate,
                    y => y.MapFrom(
                        x => x.GrowthRate.AsString(EnumFormat.Description)
                    )
                );
        }
    }
}
