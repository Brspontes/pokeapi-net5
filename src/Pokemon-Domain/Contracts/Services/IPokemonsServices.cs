using Pokemon_Domain.Enums;
using Pokemon_Domain.PokemonContext.Adapters.Outputs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokemon_Domain.Contracts.Services
{
    public interface IPokemonsServices
    {
        Task<List<PokemonWithUrlOutput>> GetPokemonRegions(RegionEnum regionEnum);
        Task<PokemonDetailsOutput> GetPokemonDetails(string name);
    }
}
