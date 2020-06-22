using System.Web;
using System.Web.Mvc;

namespace Lab4_EliasHaloun
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
