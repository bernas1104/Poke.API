using AutoMapper;
using EnumsNET;
using Poke.Core.Commands.Requests;
using Poke.Core.Commands.Responses;
using Poke.Core.DTOs;
using Poke.Core.Entities;
using Poke.Core.Queries.Response;
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
                    x => x.SpecialAttack,
                    y => y.MapFrom(x => x.SpecialAttack.Value)
                )
                .ForMember(
                    x => x.SpecialDefense,
                    y => y.MapFrom(x => x.SpecialDefense.Value)
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

            CreateMap<Pokemon, PokemonQueryResponse>()
                .ForMember(
                    x => x.FirstTypeDescription,
                    x => x.MapFrom(
                        y => y.FirstType.AsString(EnumFormat.Description)
                    )
                )
                .ForMember(
                    x => x.SecondTypeDescription,
                    x => x.MapFrom(
                        y => y.SecondType.AsString(EnumFormat.Description)
                    )
                );

            CreateMap<Training, TrainingQueryResponse>()
                .ForMember(
                    x => x.GrowthRateDescription,
                    x => x.MapFrom(
                        y => y.GrowthRate.AsString(EnumFormat.Description)
                    )
                );

            CreateMap<BaseStats, BaseStatsQueryResponse>()
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
                    x => x.SpecialAttack,
                    y => y.MapFrom(x => x.SpecialAttack.Value)
                )
                .ForMember(
                    x => x.SpecialDefense,
                    y => y.MapFrom(x => x.SpecialDefense.Value)
                )
                .ForMember(
                    x => x.Speed, y => y.MapFrom(x => x.Speed.Value)
                );

            CreateMap<CreatePokemonRequest, PokemonDTO>();
            CreateMap<BaseStatsRequest, BaseStatsDTO>();
            CreateMap<TrainingRequest, TrainingDTO>();
            CreateMap<UpdatePokemonByNumberRequest, PokemonDTO>();
            CreateMap<CreatePokemonEvolutionRequest, PokemonEvolutionDTO>()
                .ForMember(
                    x => x.FromNumber,
                    x => x.NullSubstitute(0)
                )
                .ForMember(
                    x => x.ToNumber,
                    x => x.NullSubstitute(0)
                );
        }
    }
}
