using MusicStore.Filters;
using System.Web;
using System.Web.Mvc;

namespace MusicStore
{
 
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
       
            filters.Add(new ActionAndResultFilterAttribute());
            filters.Add(new FilterExceptionAttribute());
        }
    }
}
