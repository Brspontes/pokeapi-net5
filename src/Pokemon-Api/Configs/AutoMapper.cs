using AutoMapper;
using Pokemon_Domain.PokemonContext.Adapters.Outputs;
using Pokemon_Domain.PokemonContext.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon_Api.Configs
{
    public class AutoMapperProfille : Profile
    {
        public AutoMapperProfille()
        {
            CreateMap<PokemonRegionWithUrlImage, PokemonWithUrlOutput>();
        }
    }
}
