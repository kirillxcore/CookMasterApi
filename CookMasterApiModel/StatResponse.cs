using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMasterApiModel
{
    /// <summary>
    /// Response for /Cook/Stat/{days} request
    /// List of all available dishes
    /// </summary>
    public class StatResponse
    {
        List<DishItemStat> Stat { get; set; }

    }
}
