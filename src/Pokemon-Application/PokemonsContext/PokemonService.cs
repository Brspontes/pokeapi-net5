using Microsoft.Extensions.Options;
using Pokemon_Domain.Configs;
using Pokemon_Domain.Contracts.Infraestruture;
using Pokemon_Domain.Contracts.Services;
using Pokemon_Domain.Enums;
using Pokemon_Domain.Helpers;
using Pokemon_Domain.PokemonContext.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Application.PokemonsContext
{
    public class PokemonService : IPokemonsServices
    {
        private readonly IPokemonsRepository pokemonsRepository;
        private readonly IOptions<PokeApi> options;

        public PokemonService(IPokemonsRepository pokemonsRepository, IOptions<PokeApi> options)
        {
            this.pokemonsRepository = pokemonsRepository;
            this.options = options;
        }

        public async Task<List<PokemonRegionWithUrlImage>> GetPokemonRegions(RegionEnum regionEnum)
        {
            var pokeList = new List<PokemonRegionWithUrlImage>();

            var response = await pokemonsRepository.GetPokemonRegions(UrlHelper.UrlOffSet(regionEnum));

            response.ForEach(delegate (PokemonRegion pokemonRegion)
            {
                var numberImage = pokemonRegion.Url.Replace(options.Value.PokeApiUrl, string.Empty)
                                       .Replace("/", string.Empty).Trim();

                pokeList.Add(new PokemonRegionWithUrlImage
                {
                    Name = pokemonRegion.Name,
                    Url = pokemonRegion.Url,
                    UrlImage = $"{options.Value.ImgUrl}/{urlImageHelper(numberImage)}"
                });
            });

            return pokeList;
        }

        private string urlImageHelper(string number)
        {
            if (number.Length < 3)
                return number.PadLeft(3, '0');

            return number;
        }
    }
}
