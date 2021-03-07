using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;
using Pokemon_Domain.Configs;
using Pokemon_Infra.PokemonsContext;
using RestSharp;
using System.Threading;

namespace Pokemon_Tests.PokemonContext
{
    [TestClass]
    public class PokemonRepositoryTest
    {
        private string expectedReturn = "{'results':[{'name':'bulbasaur','url':'https://pokeapi.co/api/v2/pokemon/1/'}]}";
        private string expectedReturnStats = "{'stats':[{'base_stat':39,'effort':0,'stat':{'name':'hp','url':'https://pokeapi.co/api/v2/stat/1/'}}],'types':[{'slot':1,'type':{'name':'fire','url':'https://pokeapi.co/api/v2/type/10/'}}]}";
        private string expectedReturnWeakenesses = "{'damage_relations':{'double_damage_from':[{'name':'ground'}]}}";
        private AutoMocker Mocker;
        private PokeApi optionsValues;

        [TestInitialize]
        public void Setup()
        {
            Mocker = new AutoMocker();

            optionsValues = new PokeApi
            {
                ImgUrl = "urlImg.com",
                PokeApiUrl = "https://pokeapi.co/api/v2/pokemon/",
                Weaknesses = "https://pokeapi.co/api/v2/pokemon/"
            };
        }

        [TestMethod]
        public void should_returns_pokemon_list_with_region()
        {
            var optionsMock = Mocker.GetMock<IOptions<PokeApi>>();
            optionsMock.Setup(c => c.Value).Returns(optionsValues);

            var restClientMock = Mocker.GetMock<IRestClient>();
            restClientMock.Setup(c => c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse { Content = expectedReturn });

            var repository = Mocker.CreateInstance<PokemonRepository>();

            var retorno = repository.GetPokemonRegions("hoen");
            Assert.AreEqual("bulbasaur", retorno.Result[0].Name);
            Assert.AreEqual("https://pokeapi.co/api/v2/pokemon/1/", retorno.Result[0].Url);
        }

        [TestMethod]
        public void should_returns_not_found_list()
        {
            var optionsMock = Mocker.GetMock<IOptions<PokeApi>>();
            optionsMock.Setup(c => c.Value).Returns(optionsValues);

            var restClientMock = Mocker.GetMock<IRestClient>();
            restClientMock.Setup(c => c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse { StatusCode = System.Net.HttpStatusCode.NotFound, Content = "Not Found" });

            var repository = Mocker.CreateInstance<PokemonRepository>();

            var retorno = repository.GetPokemonRegions("hoen");
            Assert.IsTrue(retorno.Exception.InnerExceptions[0].ToString().Contains("Not Found"));
        }

        [TestMethod]
        public void should_returns_stats_node_and_type()
        {
            var optionsMock = Mocker.GetMock<IOptions<PokeApi>>();
            optionsMock.Setup(c => c.Value).Returns(optionsValues);

            var restClientMock = Mocker.GetMock<IRestClient>();
            restClientMock.Setup(c => c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse { StatusCode = System.Net.HttpStatusCode.OK, Content = expectedReturnStats });

            var repository = Mocker.CreateInstance<PokemonRepository>();

            var retorno = repository.GetPokemoStats("hoen");
            Assert.IsTrue(retorno.Result.Stats[0].Stat.Name.Equals("hp"));
            Assert.IsTrue(retorno.Result.Stats[0].BaseStat.Equals("39"));
        }

        [TestMethod]
        public void should_returns_weakenesses()
        {
            var optionsMock = Mocker.GetMock<IOptions<PokeApi>>();
            optionsMock.Setup(c => c.Value).Returns(optionsValues);

            var restClientMock = Mocker.GetMock<IRestClient>();
            restClientMock.Setup(c => c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RestResponse { StatusCode = System.Net.HttpStatusCode.OK, Content = expectedReturnWeakenesses });

            var repository = Mocker.CreateInstance<PokemonRepository>();

            var retorno = repository.GetPokemonWeaknesses("hoen");
            Assert.IsTrue(retorno.Result[0].Name.Equals("ground"));
        }
    }
}
