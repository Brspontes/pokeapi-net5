using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Domain.PokemonContext.Entity
{
    public class PokemonStats
    {
        [JsonProperty("base_stat")]
        public string BaseStat { get; set; }
        public Stat Stat { get; set; }
    }

    public class Stat
    {
        public string Name { get; set; }
    }
}
