using System;
using System.Collections.Generic;
using Poke.Core.Commands.Requests;
using Poke.Core.DTOs;
using Poke.Core.Enums;
using Poke.Core.ValueObjects;
using Poke.Core.ValueObjects.Evolutions;

namespace Poke.Core.Entities
{
    public class Pokemon : Aggregate
    {
        public int Number { get; protected set; }
        public string Name { get; protected set; }
        public string Species { get; protected set; }
        public double Height { get; protected set; }
        public double Weight { get; protected set; }
        public string ImageUrl { get; protected set; }
        public PokemonType FirstType { get; protected set; }
        public PokemonType SecondType { get; protected set; }
        public Training Training { get; protected set; }
        public BaseStats BaseStats { get; protected set; }
        public IReadOnlyCollection<AbstractEvolution> PokemonsEvolveFrom { get => _pokemonsEvolveFrom; }
        public IReadOnlyCollection<AbstractEvolution> PokemonsEvolveTo { get => _pokemonsEvolveTo; }
        private List<AbstractEvolution> _pokemonsEvolveFrom;
        private List<AbstractEvolution> _pokemonsEvolveTo;

        public Pokemon()
        {
            //
        }

        public Pokemon(CreatePokemonRequest request) : base()
        {
            Id = Guid.NewGuid();
            Number = request.Number;
            Name = request.Name;
            Species = request.Species;
            Height = request.Height;
            Weight = request.Weight;
            ImageUrl = request.ImageUrl;
            FirstType = (PokemonType)request.FirstType;
            SecondType = (PokemonType)request.SecondType;

            Training = new Training(request.Training);
            BaseStats = new BaseStats(request.BaseStats);

            _pokemonsEvolveFrom = new List<AbstractEvolution>();
            _pokemonsEvolveTo = new List<AbstractEvolution>();

            SetEvolutionsFromRequest(request.Evolutions);
            SetPreEvolutionsFromRequest(request.PreEvolutions);
        }

        private void SetEvolutionsFromRequest(
            List<CreatePokemonEvolutionRequest> requests
        )
        {
            if (requests is not null)
            {
                foreach (var evolutionRequest in requests)
                {
                    _pokemonsEvolveTo.Add(
                        Evolution.FromCreatePokemonEvolutionRequest(
                            evolutionRequest,
                            Number
                        )
                    );
                }
            }
        }

        private void SetPreEvolutionsFromRequest(
            List<CreatePokemonEvolutionRequest> requests
        )
        {
            if (requests is not null)
            {
                foreach (var preEvolutionRequest in requests)
                {
                    _pokemonsEvolveFrom.Add(
                        PreEvolution.FromCreatePokemonEvolutionRequest(
                            preEvolutionRequest,
                            Number
                        )
                    );
                }
            }
        }

        public Pokemon(
            Guid id, int number, string name, string species,
            double height, double weight, string image_url,
            PokemonType first_type, PokemonType second_type
        ) : base(id)
        {
            Number = number;
            Name = name;
            Species = species;
            Height = height;
            Weight = weight;
            ImageUrl = image_url;
            FirstType = first_type;
            SecondType = second_type;

            _pokemonsEvolveFrom = new List<AbstractEvolution>();
            _pokemonsEvolveTo = new List<AbstractEvolution>();
        }

        private Pokemon(PokemonDTO dto)
        {
            Id = dto.Id;
            Number = dto.Number;
            Name = dto.Name;
            Species = dto.Species;
            Height = dto.Height;
            Weight = dto.Weight;
            ImageUrl = dto.ImageUrl;
            FirstType = (PokemonType)dto.FirstType;
            SecondType = (PokemonType)dto.SecondType;

            Training = Training.FromTrainingDTO(dto.Training);
            BaseStats = BaseStats.FromBaseStatsDTO(dto.BaseStats);

            _pokemonsEvolveFrom = new List<AbstractEvolution>();
            _pokemonsEvolveTo = new List<AbstractEvolution>();
        }

        public void AddTrainingInformation(Training training)
        {
            Training = training;
        }

        public void AddBaseStatsInformation(BaseStats baseStats)
        {
            BaseStats = baseStats;
        }

        public void UpdatePokemonData(UpdatePokemonByNumberRequest request)
        {
            Name = request.Name;
            Species = request.Species;
            Height = request.Height;
            Weight = request.Weight;
            ImageUrl = request.ImageUrl;
            FirstType = (PokemonType)request.FirstType;
            SecondType = (PokemonType)request.SecondType;

            Training.UpdatePokemonTrainingData(request.Training);
            BaseStats.UpdatePokemonBaseStatsData(request.BaseStats);
        }

        public void SetPokemonEvolutions(List<AbstractEvolution> evolutions)
        {
            _pokemonsEvolveTo = evolutions;
        }

        public void SetPokemonPreEvolutions(
            List<AbstractEvolution> preEvolutions
        )
        {
            _pokemonsEvolveFrom = preEvolutions;
        }

        public void AddPokemonEvolution(Evolution evolution)
        {
            _pokemonsEvolveTo.Add(evolution);
        }

        public void AddPokemonPreEvolution(PreEvolution preEvolution)
        {
            _pokemonsEvolveFrom.Add(preEvolution);
        }

        public static Pokemon FromPokemonDTO(PokemonDTO dto)
        {
            return new Pokemon(dto);
        }
    }
}
