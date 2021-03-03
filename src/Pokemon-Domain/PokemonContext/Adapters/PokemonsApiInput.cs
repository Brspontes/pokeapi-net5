using Newtonsoft.Json.Converters;
using Pokemon_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pokemon_Domain.PokemonContext.Adapters
{
    public class PokemonsApiInput
    {
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public RegionEnum regionEnum { get; set; }
    }
}
