using FluentValidation;
using Poke.Core.DTOs;
using Poke.Core.Enums;

namespace Poke.Core.Validations
{
    public class EvolutionValidation : AbstractValidator<PokemonEvolutionDTO>
    {
        public static readonly int EVOLUTION = 0x1;
        public static readonly int PRE_EVOLUTION = 0x2;
        public static readonly int BOTH = 0x4;
        private readonly int BasePokemonNumber;
        private readonly int Evolution;

        public EvolutionValidation(int pokemonNumber, int evolutionType)
        {
            BasePokemonNumber = pokemonNumber;
            Evolution = evolutionType;

            RuleFor(x => x.FromNumber)
                .InclusiveBetween(1, 151)
                .WithMessage("Pokemon must evolve from a pokemon with a valid number.")
                .When(x => Evolution == PRE_EVOLUTION || Evolution == BOTH);

            RuleFor(x => x.ToNumber)
                .InclusiveBetween(1, 151)
                .WithMessage("Pokemon must evolve to a pokemon with a valid number.")
                .When(x => Evolution == EVOLUTION || Evolution == BOTH);

            RuleFor(x => x)
                .Must(ValidateDiferentPokemonNumbers)
                .WithMessage("Pokemon must evolve to/from a pokemon with a diferent number.");

            RuleFor(x => x.EvolutionType)
                .InclusiveBetween(0, 4)
                .WithMessage("Pokemon evolution type invalid.");

            RuleFor(x => x.PokemonEvolutionLevel)
                .InclusiveBetween(2, 100)
                .WithMessage("Pokemon evolution level must be between 2 and 100.")
                .When(x => (EvolutionType)x.EvolutionType == EvolutionType.Level);

            RuleFor(x => x.EvolutionStone)
                .InclusiveBetween(0, 9)
                .WithMessage("Pokemon evolution stone invalid.")
                .When(x => (EvolutionType)x.EvolutionType == EvolutionType.Stone);

            RuleFor(x => x.HeldItemName)
                .NotEmpty()
                .WithMessage("Pokemon held item invalid.")
                .When(
                    x => (EvolutionType)x.EvolutionType ==
                        EvolutionType.TradeWithItem
                );
        }

        private bool ValidateDiferentPokemonNumbers(PokemonEvolutionDTO dto)
        {
            if (Evolution == EVOLUTION)
            {
                return dto.ToNumber != BasePokemonNumber;
            } else if (Evolution == PRE_EVOLUTION)
            {
                return dto.FromNumber != BasePokemonNumber;
            }

            return dto.FromNumber != dto.ToNumber;
        }
    }
}
