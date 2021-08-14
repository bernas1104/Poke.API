using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Poke.API.Controllers.V1
{
    [ApiController]
    [Route("api/v1/{controller}")]
    [Produces("application/json")]
    public class PokemonsController : ControllerBase
    {
        //

        public PokemonsController()
        {}

        [HttpGet]
        public async Task<IActionResult> GetByNumber(
            [FromRoute] int pokemonNumber
        )
        {
            await Task.Delay(1);
            return Ok();
        }
    }
}
