using Pokemon_Domain.PokemonContext.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Domain.PokemonContext.Adapters.Outputs
{
    public class PokemonDetailsOutput : BaseAdaptarOutput
    {
        public string Name { get; set; }
        public List<PokemonStats> Stats{ get; set; }
        public List<PokemonTypes> Types { get; set; }
        public List<PokemonWeaknesses> Weaknesses { get; set; }
    }
}
