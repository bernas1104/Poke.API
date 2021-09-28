using System.Collections.Generic;
using System.Threading.Tasks;
using Poke.Application.Dtos.InputModels;
using Poke.Application.Dtos.ViewModels;

namespace Poke.Application.Services.Interfaces
{
    public interface IPokemonsService
    {
        Task<IEnumerable<PokemonViewModel>> GetAllAsync();
        Task<PokemonViewModel> CreatePokemonAsync(PokemonInputModel pokemon);
    }
}
