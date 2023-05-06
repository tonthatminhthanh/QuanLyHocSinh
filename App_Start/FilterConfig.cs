using System.Web;
using System.Web.Mvc;

namespace QLHocSinhHVT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
