using Pokemon_Domain.PokemonContext.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Domain.PokemonContext.Adapters.Outputs
{
    public class PokemonWithUrlOutput : BaseAdaptarOutput
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string UrlImage { get; set; }
        public List<PokemonTypes> Types { get; set; }
    }
}
