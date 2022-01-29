using FluentValidation;
using Poke.Core.Commands.Requests;

namespace Poke.Core.Validations
{
    public class CreateTrainingValidation :
        AbstractValidator<CreateTrainingRequest>
    {
        public CreateTrainingValidation()
        {
            RuleFor(x => x.EVYeld)
                .InclusiveBetween(1, 3)
                .WithMessage("Training: EV yeld must be between 1 and 3.");

            RuleFor(x => x.BaseFriendship)
                .InclusiveBetween(0, 140)
                .WithMessage("Training: base friendship must be between 0 and 140.");

            RuleFor(x => x.GrowthRate)
                .InclusiveBetween(0, 5)
                .WithMessage("Training: growth rate must be betwwen 0 and 5.");
        }
    }
}
