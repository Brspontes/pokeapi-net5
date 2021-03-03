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
                RegionEnum.Kanto => "?limit=151",
                RegionEnum.Jotho => "?limit=100&offset=151",
                RegionEnum.Hoen => "?limit=135&offset=251",
                _ => "?limit=151"
            };

            return url;
        }
    }
}
