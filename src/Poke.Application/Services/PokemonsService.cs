using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Poke.Application.Dtos.InputModels;
using Poke.Application.Dtos.ViewModels;
using Poke.Application.Services.Interfaces;
using Poke.Core.Entities;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Interfaces.UoW;
using Poke.Core.ValueObjects;

namespace Poke.Application.Services
{
    public class PokemonsService : IPokemonsService
    {
        private readonly IPokemonsRepository _pokemonsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PokemonsService(
            IPokemonsRepository pokemonsRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _pokemonsRepository = pokemonsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PokemonViewModel>> GetAllAsync()
        {
            var pokemon = await _pokemonsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PokemonViewModel>>(
                // await _pokemonsRepository.GetAllAsync()
                pokemon
            );
        }

        public async Task<PokemonViewModel> CreatePokemonAsync(
            PokemonInputModel pokemon
        )
        {
            var pokemonExists = await _pokemonsRepository.PokemonExists(
                pokemon.Number
            );

            if (pokemonExists)
            {
                // TODO DomainNotification
                return null;
            }

            var trainingData = pokemon.Training;

            var training = new Training(
                trainingData.EVYeld,
                trainingData.BaseFriendship,
                trainingData.GrowthRate
            );

            var baseStatsData = pokemon.BaseStats;

            var baseStats = new BaseStats(
                new Point(baseStatsData.HitPoints), new Point(baseStatsData.Attack),
                new Point(baseStatsData.Defense), new Point(baseStatsData.SpecialAttack),
                new Point(baseStatsData.SpecialDefense), new Point(baseStatsData.Speed)
            );

            // var newPokemon = new Pokemon(
            //     pokemon.Number, pokemon.Name, pokemon.Species, pokemon.Height,
            //     pokemon.Weight, pokemon.ImageUrl, pokemon.FirstType, training,
            //     baseStats, pokemon.SecondType
            // );
            var newPokemon = new Pokemon(
                pokemon.Number, pokemon.Name, pokemon.Species, pokemon.Height,
                pokemon.Weight, pokemon.ImageUrl, pokemon.FirstType, pokemon.SecondType
            );

            if (!newPokemon.IsValid)
            {
                // TODO DomainNotification
                return null;
            }

            _pokemonsRepository.Add(newPokemon);

            _unitOfWork.Commit();

            return _mapper.Map<PokemonViewModel>(newPokemon);
        }
    }
}
