using Pokemon_Domain.Enums;
using Pokemon_Domain.PokemonContext.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Domain.Contracts.Services
{
    public interface IPokemonsServices
    {
        Task<List<PokemonRegionWithUrlImage>> GetPokemonRegions(RegionEnum regionEnum);
    }
}
