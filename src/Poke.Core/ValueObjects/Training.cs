using System;
using Poke.Core.Commands.Requests;
using Poke.Core.Entities;
using Poke.Core.Enums;

namespace Poke.Core.ValueObjects
{
    public class Training : ValueObject
    {
        public int EVYeld { get; protected set; }
        public int BaseFriendship { get; protected set; }
        public GrowthRate GrowthRate { get; protected set; }
        public Guid PokemonId { get; protected set; }
        public Pokemon Pokemon { get; protected set; }

        public Training()
        {}

        public Training(CreateTrainingRequest request)
        {
            EVYeld = request.EVYeld;
            BaseFriendship = request.BaseFriendship;
            GrowthRate = (GrowthRate)request.GrowthRate;
        }

        public Training(
            int evYeld, int baseFrindship,
            GrowthRate growthRate, Guid pokemonId
        )
        {
            EVYeld = evYeld;
            BaseFriendship = baseFrindship;
            GrowthRate = growthRate;
            PokemonId = pokemonId;
        }
    }
}
