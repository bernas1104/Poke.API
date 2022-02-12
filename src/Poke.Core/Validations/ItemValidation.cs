using FluentValidation;
using Poke.Core.DTOs;

namespace Poke.Core.Validations
{
    public class ItemValidation : AbstractValidator<ItemDTO>
    {
        public ItemValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Item: name must not be empty or null.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Item: description must not be empty or null.");

            RuleFor(x => x.ItemType)
                .InclusiveBetween(0, 7)
                .WithMessage("Item: item type must be between 0 and 7.");
        }
    }
}
