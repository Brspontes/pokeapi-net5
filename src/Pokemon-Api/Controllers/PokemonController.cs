using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemon_Domain.Enums;
using Pokemon_Domain.PokemonContext.Adapters;
using Pokemon_Domain.PokemonContext.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {

        /// <summary>
        /// Get All Pokemons with Region
        /// </summary>
        /// <param name="regionEnum"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<PokemonRegionWithUrlImage> Pokemons([FromQuery]PokemonsApiInput regionEnum)
        {
            return Ok();
        }
    }
}
