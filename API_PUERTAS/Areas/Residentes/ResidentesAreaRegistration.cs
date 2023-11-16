using System.Web.Mvc;

namespace API_PUERTAS.Areas.Residentes
{
    public class ResidentesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Residentes";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Residentes_default",
                "Residentes/{controller}/{action}/{id}",
                 defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}