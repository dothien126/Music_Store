using log4net;
using MusicStore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ILog logger = LogManager.GetLogger("MvcApplication");
        protected void Application_Start()
        {
         
            System.Data.Entity.Database.SetInitializer(new MusicStore.EntityContext.SampleData());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if(ex is HttpException)
            {
           
                HttpException httpException=ex as HttpException;
        
                if (httpException.GetHttpCode() == 400)
                {
                    Response.Redirect("/Home/BadRequest");
                }

                if (httpException.GetHttpCode() == 403)
                {
                    Response.Redirect("/Home/NoAuth");
                }
        
                if (httpException.GetHttpCode() == 404)
                {
                    Response.Redirect("/Home/NoFound");
                }

            }
         

        }
    }
}
