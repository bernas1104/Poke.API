using Poke.API.Controllers.V1;

namespace Poke.Unit.Tests.Controllers
{
    public class PokemonControllerTest
    {
        private readonly PokemonsController _controller;

        public PokemonControllerTest()
        {
            _controller = new PokemonsController();
        }
    }
}
