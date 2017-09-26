using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMasterApiModel
{
    /// <summary>
    /// Response for /Cook/Dishes request
    /// List of all available dishes
    /// </summary>
    public class DishesResponse
    {
        public List<DishItem> Dishes { get; set; }
    }
}
