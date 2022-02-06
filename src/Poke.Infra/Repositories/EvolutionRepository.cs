using Poke.Core.Interfaces.Repositories;
using Poke.Core.ValueObjects;
using Poke.Infra.Context;

namespace Poke.Infra.Repositories
{
    public class EvolutionRepository : EntityBaseRepository<AbstractEvolution>,
        IEvolutionRepository
    {
        private readonly DapperContext _dapperContext;

        public EvolutionRepository(
            EntityContext context,
            DapperContext dapperContext
        ) : base(context)
        {
            _dapperContext = dapperContext;
        }
    }
}
