using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pokemon_Domain.Configs;
using Pokemon_Domain.Contracts.Infraestruture;
using Pokemon_Domain.PokemonContext.Entity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pokemon_Infra.PokemonsContext
{
    public class PokemonRepository : IPokemonsRepository
    {
        private readonly IOptions<PokeApi> options;

        public PokemonRepository(IOptions<PokeApi> options)
        {
            this.options = options;
        }

        public async Task<List<PokemonRegion>> GetPokemonRegions(string urlCompose)
        {
            var client = new RestClient(options.Value.PokeApiUrl);
            var request = new RestRequest(urlCompose);

            var response = await client.ExecuteAsync(request);
            var jsonObject = JObject.Parse(response.Content);

            var pokemons = JsonConvert.DeserializeObject<List<PokemonRegion>>
                (jsonObject.SelectToken("results").ToString());

            return pokemons;
        }
    }
}
