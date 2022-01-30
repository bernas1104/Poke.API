using FluentValidation;
using Poke.Core.DTOs;

namespace Poke.Core.Validations
{
    public class PokemonValidation :
        AbstractValidator<PokemonDTO>
    {
        public PokemonValidation()
        {
            RuleFor(x => x.Number)
                .InclusiveBetween(1, 151)
                .WithMessage("Pokemon: number must be between 1 and 151.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Pokemon: name must not be empty or null.");

            RuleFor(x => x.Species)
                .NotEmpty()
                .WithMessage("Pokemon: species must not be empty or null.");

            RuleFor(x => x.Height)
                .GreaterThan(0)
                .WithMessage("Pokemon: height must be greater than zero (0).");

            RuleFor(x => x.Weight)
                .GreaterThan(0)
                .WithMessage("Pokemon: weight must be greater than zero (0).");

            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .WithMessage("Pokemon: must have a image URL.");

            RuleFor(x => x.FirstType)
                .InclusiveBetween(1, 15)
                .WithMessage("Pokemon: first type must be between 1 and 15.");

            RuleFor(x => x.SecondType)
                .InclusiveBetween(0, 15)
                .WithMessage("Pokemon: second type must be between 0 and 15.");

            RuleFor(x => x.Training).SetValidator(new TrainingValidation());

            RuleFor(x => x.BaseStats).SetValidator(new BaseStatsValidation());
        }
    }
}
