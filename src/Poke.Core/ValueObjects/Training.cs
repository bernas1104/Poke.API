using Poke.Core.Commands.Requests;
using Poke.Core.DTOs;
using Poke.Core.Entities;
using Poke.Core.Enums;

namespace Poke.Core.ValueObjects
{
    public class Training : ValueObject
    {
        public int EVYeld { get; protected set; }
        public int BaseFriendship { get; protected set; }
        public GrowthRate GrowthRate { get; protected set; }
        public int PokemonNumber { get; protected set; }
        public Pokemon Pokemon { get; protected set; }

        public Training()
        {}

        public Training(TrainingRequest request)
        {
            EVYeld = request.EVYeld;
            BaseFriendship = request.BaseFriendship;
            GrowthRate = (GrowthRate)request.GrowthRate;
        }

        public Training(
            int evYeld, int baseFrindship,
            GrowthRate growthRate, int pokemonNumber
        )
        {
            EVYeld = evYeld;
            BaseFriendship = baseFrindship;
            GrowthRate = growthRate;
            PokemonNumber = pokemonNumber;
        }

        private Training(TrainingDTO dto) : base(dto.Id)
        {
            EVYeld = dto.EVYeld;
            BaseFriendship = dto.BaseFriendship;
            GrowthRate = (GrowthRate)dto.GrowthRate;
        }

        public void UpdatePokemonTrainingData(TrainingRequest request)
        {
            EVYeld = request.EVYeld;
            BaseFriendship = request.BaseFriendship;
            GrowthRate = (GrowthRate)request.GrowthRate;
        }

        public static Training FromTrainingDTO(TrainingDTO dto)
        {
            return new Training(dto);
        }
    }
}
