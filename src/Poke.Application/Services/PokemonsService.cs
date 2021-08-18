using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Poke.Application.Dtos.ViewModels;
using Poke.Application.Services.Interfaces;
using Poke.Core.Interfaces.Repositories;
using Poke.Core.Interfaces.UoW;

namespace Poke.Application.Services
{
    public class PokemonsService : IPokemonsService
    {
        private readonly IPokemonsRepository _pokemonsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PokemonsService(
            IPokemonsRepository pokemonsRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _pokemonsRepository = pokemonsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PokemonViewModel>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<PokemonViewModel>>(
                await _pokemonsRepository.GetAllAsync()
            );
        }
    }
}
