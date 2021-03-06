using Pokemon_Domain.PokemonContext.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Domain.Contracts.Infraestruture
{
    public interface IPokemonsRepository
    {
        Task<List<PokemonRegion>> GetPokemonRegions(string urlCompose);
    }
}
