using AutoMapper;
using Microsoft.Extensions.Options;
using Pokemon_Domain.Configs;
using Pokemon_Domain.Contracts.Infraestruture;
using Pokemon_Domain.Contracts.Services;
using Pokemon_Domain.Enums;
using Pokemon_Domain.Helpers;
using Pokemon_Domain.PokemonContext.Adapters.Outputs;
using Pokemon_Domain.PokemonContext.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon_Application.PokemonsContext
{
    public class PokemonService : IPokemonsServices
    {
        private readonly IPokemonsRepository pokemonsRepository;
        private readonly IOptions<PokeApi> options;
        private readonly IMapper mapper;

        public PokemonService(IPokemonsRepository pokemonsRepository, IOptions<PokeApi> options, IMapper mapper)
        {
            this.pokemonsRepository = pokemonsRepository;
            this.options = options;
            this.mapper = mapper;
        }

        public async Task<PokemonDetailsOutput> GetPokemonDetails(string name)
        {
            try
            {
                var response = await pokemonsRepository.GetPokemoStats(name);

                response.Weaknesses = await pokemonsRepository.GetPokemonWeaknesses(response.Types[0].Type.Name);

                return response;
            }
            catch (Exception ex)
            {
                return new PokemonDetailsOutput
                {
                    Error = ex.Message,
                    Message = MessageHelper.ErrorMessage,
                };
            }
        }

        public async Task<List<PokemonWithUrlOutput>> GetPokemonRegions(RegionEnum regionEnum)
        {
            try
            {
                var pokeList = new List<PokemonRegionWithUrlImage>();

                var response = await pokemonsRepository.GetPokemonRegions(UrlHelper.UrlOffSet(regionEnum));

                response.ForEach(delegate (PokemonRegion pokemonRegion)
                {
                    var numberImage = pokemonRegion.Url
                                            .Replace(options.Value.PokeApiUrl, string.Empty)
                                            .Replace("/", string.Empty)
                                            .Trim();

                    pokeList.Add(new PokemonRegionWithUrlImage
                    {
                        Name = pokemonRegion.Name,
                        Url = pokemonRegion.Url,
                        UrlImage = $"{options.Value.ImgUrl}/{urlImageHelper(numberImage)}.png"
                    });
                });

                var AllPokemonsWithDetails = pokeList.Select(c => pokemonsRepository.GetPokemoStats(c.Name));
                var responseDetails = Task.WhenAll(AllPokemonsWithDetails);

                responseDetails.Result.ToList().ForEach(delegate (PokemonDetailsOutput detail)
                {
                    var index = pokeList.FindIndex(c => c.Name.ToLower().Equals(detail.Name));
                    pokeList[index].Types = detail.Types;
                });

                return mapper.Map<List<PokemonWithUrlOutput>>(pokeList);
            }
            catch (Exception ex)
            {
                return new List<PokemonWithUrlOutput>
                {
                    new PokemonWithUrlOutput
                    {
                        Message = MessageHelper.ErrorMessage,
                        Error = ex.Message
                    }
                };
            }
        }

        private string urlImageHelper(string number)
        {
            if (number.Length < 3) return number.PadLeft(3, '0');

            return number;
        }
    }
}
