using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;
using Pokemon_Api.Configs;
using Pokemon_Application.PokemonsContext;
using Pokemon_Domain.Configs;
using Pokemon_Domain.Contracts.Infraestruture;
using Pokemon_Domain.CustomException;
using Pokemon_Domain.Enums;
using Pokemon_Domain.Helpers;
using Pokemon_Domain.PokemonContext.Adapters.Outputs;
using Pokemon_Domain.PokemonContext.Entity;
using System.Collections.Generic;

namespace Pokemon_Tests.PokemonContext
{
    [TestClass]
    public class PokemonServiceTests
    {
        private AutoMocker Mocker;
        private PokeApi optionsValues;
        private static IMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            Mocker = new AutoMocker();

            optionsValues = new PokeApi
            {
                ImgUrl = "https://assets.pokemon.com/assets/cms2/img/pokedex/full",
                PokeApiUrl = "https://pokeapi.co/api/v2/pokemon/",
                Weaknesses = "https://pokeapi.co/api/v2/pokemon/"
            };
        }

        [TestMethod]
        public void should_return_error_not_found_get_pokemon_detail()
        {
            var service = Mocker.CreateInstance<PokemonService>();
            var repository = Mocker.GetMock<IPokemonsRepository>();

            repository.Setup(c => c.GetPokemoStats(It.IsAny<string>())).Throws(new NotFoundException("Not Found"));
            var returns = service.GetPokemonDetails(string.Empty);

            Assert.AreEqual(MessageHelper.ErrorMessage, returns.Result.Message);
            Assert.AreEqual("Not Found", returns.Result.Error);
        }

        [TestMethod]
        public void should_return_pokemon_detail()
        {
            var service = Mocker.CreateInstance<PokemonService>();
            var repository = Mocker.GetMock<IPokemonsRepository>();

            repository.Setup(c => c.GetPokemoStats(It.IsAny<string>())).ReturnsAsync(new PokemonDetailsOutput
            {
                Name = "charmander",
                Types = new List<PokemonTypes>
                {
                    new PokemonTypes { Type = new Pokemon_Domain.PokemonContext.Entity.Type { Name = "fire" } }
                },
                Stats = new List<PokemonStats>
                {
                    new PokemonStats { BaseStat = "39", Stat = new Stat { Name = "hp" }  }
                }
            });

            repository.Setup(c => c.GetPokemonWeaknesses(It.IsAny<string>())).ReturnsAsync(new List<PokemonWeaknesses>
            {
                 new PokemonWeaknesses
                 {
                     Name = "ground"
                 }
            });

            var returns = service.GetPokemonDetails(string.Empty);

            Assert.AreEqual("charmander", returns.Result.Name);
            Assert.AreEqual("ground", returns.Result.Weaknesses[0].Name);
        }

        [TestMethod]
        public void should_return_error_not_found_get_pokemons_region()
        {
            var service = Mocker.CreateInstance<PokemonService>();
            var repository = Mocker.GetMock<IPokemonsRepository>();

            repository.Setup(c => c.GetPokemonRegions(It.IsAny<string>())).Throws(new NotFoundException("Not Found"));
            var returns = service.GetPokemonRegions(RegionEnum.hoen);

            Assert.AreEqual(MessageHelper.ErrorMessage, returns.Result[0].Message);
            Assert.AreEqual("Not Found", returns.Result[0].Error);
        }

        [TestMethod]
        public void should_return_get_pokemons_region()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfille());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            var options = Mocker.GetMock<IOptions<PokeApi>>();
            var repository = Mocker.GetMock<IPokemonsRepository>();

            options.Setup(c => c.Value).Returns(optionsValues);

            repository.Setup(c => c.GetPokemonRegions(It.IsAny<string>())).ReturnsAsync(new List<PokemonRegion>
            {
                new PokemonRegion
                {
                    Name = "charmander",
                    Url = "https://pokeapi.co/api/v2/pokemon/4/"
                }
            });

            var service = new PokemonService(repository.Object, options.Object, mapper);
            
            var returns = service.GetPokemonRegions(RegionEnum.hoen);

            Assert.AreEqual("https://assets.pokemon.com/assets/cms2/img/pokedex/full/004", returns.Result[0].UrlImage);
        }
    }
}
