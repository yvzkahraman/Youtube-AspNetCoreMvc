using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Constraints
{
    public class Programlama : IRouteConstraint
    {
        public List<string> diller = new List<string>() { "c", "java", "csharp" }; 
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return diller.Contains(values[routeKey].ToString().ToLower());
        }
    }
}
