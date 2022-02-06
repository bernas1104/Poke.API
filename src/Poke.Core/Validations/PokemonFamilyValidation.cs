using System.Collections.Generic;
using FluentValidation;
using Poke.Core.DTOs;

namespace Poke.Core.Validations
{
    public class PokemonFamilyValidation : AbstractValidator<List<PokemonDTO>>
    {
        public PokemonFamilyValidation()
        {
            RuleForEach(x => x).SetValidator(new PokemonValidation());
        }
    }
}
