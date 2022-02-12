using System;
using System.Collections.Generic;
using Poke.Core.DTOs;
using Poke.Core.Entities;
using Poke.Core.Enums;

namespace Poke.Core.ValueObjects
{
    public abstract class AbstractEvolution : ValueObject
    {
        public int FromNumber { get; private set; }
        public Pokemon PokemonEvolvesFrom { get; private set; }
        public int ToNumber { get; private set; }
        public Pokemon PokemonEvolvesTo { get; private set; }
        public EvolutionType EvolutionType { get; private set; }
        public int? PokemonEvolutionLevel { get; private set; }
        public EvolutionStone? EvolutionStone { get; private set; }
        public Guid? HeldItemId { get; private set; }
        public Item HeldItem { get; private set; }

        public AbstractEvolution()
        {
            //
        }

        protected AbstractEvolution(PokemonEvolutionDTO dto) :
            base(dto.Id ?? Guid.NewGuid())
        {
            FromNumber = dto.FromNumber;
            ToNumber = dto.ToNumber;
            EvolutionType = (EvolutionType)dto.EvolutionType;
            PokemonEvolutionLevel = dto.PokemonEvolutionLevel;
            EvolutionStone = (EvolutionStone?)dto.EvolutionStone;
        }

        public void SetPokemonEvolvesFrom(Pokemon pokemonEvolvesFrom)
        {
            PokemonEvolvesFrom = pokemonEvolvesFrom;
        }

        public void SetPokemonEvolvesTo(Pokemon pokemonEvolvesTo)
        {
            PokemonEvolvesTo = pokemonEvolvesTo;
        }

        public abstract override string ToString();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FromNumber;
            yield return ToNumber;
        }
    }
}
