using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMasterApiModel
{
    /// <summary>
    /// Request data for /Cook/Login
    /// 
    /// Expected responses:
    /// 200 Ok - Login successful
    /// 404 NotFound - Ids not in db
    /// </summary>
    public class PublishRequest
    {
        public List<string> Ids { get; set; }
    }
}
