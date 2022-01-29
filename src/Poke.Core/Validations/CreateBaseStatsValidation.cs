using FluentValidation;
using Poke.Core.Commands.Requests;

namespace Poke.Core.Validations
{
    public class CreateBaseStatsValidation :
        AbstractValidator<CreateBaseStatsRequest>
    {
        public CreateBaseStatsValidation()
        {
            RuleFor(x => x.HitPoints)
                .InclusiveBetween(1, 255)
                .WithMessage("Base stats: hit points must be between 1 and 255.");

            RuleFor(x => x.Attack)
                .InclusiveBetween(1, 255)
                .WithMessage("Base stats: attack must be between 1 and 255.");

            RuleFor(x => x.Defense)
                .InclusiveBetween(1, 255)
                .WithMessage("Base stats: defense must be between 1 and 255.");

            RuleFor(x => x.SpecialAttack)
                .InclusiveBetween(1, 255)
                .WithMessage("Base stats: special attack must be between 1 and 255.");

            RuleFor(x => x.SpecialDefense)
                .InclusiveBetween(1, 255)
                .WithMessage("Base stats: special defense must be between 1 and 255.");

            RuleFor(x => x.Speed)
                .InclusiveBetween(1, 255)
                .WithMessage("Base stats: speed must be between 1 and 255.");
        }
    }
}
