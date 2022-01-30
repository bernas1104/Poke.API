using System;
using Poke.Core.DTOs;
using Poke.Core.Entities;
using Poke.Core.Enums;

namespace Poke.Core.ValueObjects
{
    public abstract class AbstractEvolution : ValueObject
    {
        public Guid FromId { get; private set; }
        public Pokemon PokemonEvolvesFrom { get; private set; }
        public Guid ToId { get; private set; }
        public Pokemon PokemonEvolvesTo { get; private set; }
        public EvolutionType EvolutionType { get; private set; }
        public int? PokemonEvolutionLevel { get; private set; }
        public EvolutionStone? EvolutionStone { get; private set; }

        public AbstractEvolution()
        {
            //
        }

        public AbstractEvolution(PokemonEvolutionDTO dto)
        {
            FromId = dto.FromId;
            ToId = dto.ToId;
            EvolutionType = (EvolutionType)dto.EvolutionType;
            PokemonEvolutionLevel = dto.PokemonEvolutionLevel;
            EvolutionStone = (EvolutionStone)dto.EvolutionStone;
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
    }
}
