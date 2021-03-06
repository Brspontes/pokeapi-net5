﻿using Microsoft.AspNetCore.Mvc;
using Pokemon_Domain.Contracts.Services;
using Pokemon_Domain.Enums;
using Pokemon_Domain.PokemonContext.Entity;
using System.Collections.Generic;
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
        public async Task<ActionResult<List<PokemonRegionWithUrlImage>>> Pokemons([FromQuery] RegionEnum regionEnum, [FromServices] IPokemonsServices pokemonsServices)
        {
            var repsonse = await pokemonsServices.GetPokemonRegions(regionEnum);
            return Ok(repsonse);
        }
    }
}
