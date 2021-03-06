using Pokemon_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Domain.Helpers
{
    public static class UrlHelper
    {
        public static string UrlOffSet(RegionEnum regionEnum)
        {
            var url = regionEnum switch
            {
                RegionEnum.kanto => "?limit=151",
                RegionEnum.jotho => "?limit=100&offset=151",
                RegionEnum.hoen => "?limit=135&offset=251",
                _ => "?limit=151"
            };

            return url;
        }
    }
}
