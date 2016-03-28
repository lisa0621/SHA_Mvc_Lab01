using System.Web;
using System.Web.Mvc;

namespace SHA_Mvc_Lab01
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
