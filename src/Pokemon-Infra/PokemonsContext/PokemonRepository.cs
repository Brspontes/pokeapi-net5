using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pokemon_Domain.Configs;
using Pokemon_Domain.Contracts.Infraestruture;
using Pokemon_Domain.CustomException;
using Pokemon_Domain.PokemonContext.Adapters.Outputs;
using Pokemon_Domain.PokemonContext.Entity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokemon_Infra.PokemonsContext
{
    public class PokemonRepository : IPokemonsRepository
    {
        private readonly IOptions<PokeApi> options;
        private readonly IRestRequest request;
        private readonly IRestClient client;

        public PokemonRepository(IOptions<PokeApi> options, IRestRequest request, IRestClient client)
        {
            this.options = options;
            this.request = request;
            this.client = client;
        }

        public async Task<List<PokemonRegion>> GetPokemonRegions(string urlCompose)
        {
            client.BaseUrl = new Uri(options.Value.PokeApiUrl);
            request.Resource = urlCompose;

            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new NotFoundException(response.Content);

            var jsonObject = JObject.Parse(response.Content);

            var pokemons = JsonConvert.DeserializeObject<List<PokemonRegion>>
                (jsonObject.SelectToken("results").ToString());

            return pokemons;
        }

        public async Task<List<PokemonWeaknesses>> GetPokemonWeaknesses(string nameWeaknesses)
        {
            client.BaseUrl = new Uri(options.Value.Weaknesses);
            request.Resource = nameWeaknesses;

            var response = await client.ExecuteAsync(request);

            var jsonObject = JObject.Parse(response.Content);

            var weaknesses = JsonConvert.DeserializeObject<List<PokemonWeaknesses>>
                (jsonObject["damage_relations"].SelectToken("double_damage_from").ToString());

            return weaknesses;
        }

        public async Task<PokemonDetailsOutput> GetPokemoStats(string name)
        {
            client.BaseUrl = new Uri(options.Value.PokeApiUrl);
            request.Resource = name;

            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new NotFoundException(response.Content);

            var jsonObject = JObject.Parse(response.Content);

            var stats = JsonConvert.DeserializeObject<List<PokemonStats>>
                (jsonObject.SelectToken("stats").ToString());

            var types = JsonConvert.DeserializeObject<List<PokemonTypes>>
                (jsonObject.SelectToken("types").ToString());

            return new PokemonDetailsOutput
            {
                Name = name,
                Stats = stats,
                Types = types
            };
        }
    }
}
