using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pokemon_Domain.Configs;
using Pokemon_Domain.Contracts.Infraestruture;
using Pokemon_Domain.CustomException;
using Pokemon_Domain.PokemonContext.Adapters.Outputs;
using Pokemon_Domain.PokemonContext.Entity;
using RestSharp;
using System.Collections.Generic;
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

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new NotFoundException(response.Content);

            var jsonObject = JObject.Parse(response.Content);

            var pokemons = JsonConvert.DeserializeObject<List<PokemonRegion>>
                (jsonObject.SelectToken("results").ToString());

            return pokemons;
        }

        public async Task<List<PokemonWeaknesses>> GetPokemonWeaknesses(string nameWeaknesses)
        {
            var client = new RestClient(options.Value.Weaknesses);
            var request = new RestRequest(nameWeaknesses);

            var response = await client.ExecuteAsync(request);

            var jsonObject = JObject.Parse(response.Content);

            var weaknesses = JsonConvert.DeserializeObject<List<PokemonWeaknesses>>
                (jsonObject["damage_relations"].SelectToken("double_damage_from").ToString());

            return weaknesses;
        }

        public async Task<PokemonDetailsOutput> GetPokemoStats(string name)
        {
            var client = new RestClient(options.Value.PokeApiUrl);
            var request = new RestRequest(name);

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
