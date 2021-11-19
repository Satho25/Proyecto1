using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using ProyectoDeTitulo.Models;
using System.Security.Principal;
using ProyectoDeTitulo.CustomAuthentication;

namespace ProyectoDeTitulo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is HttpException)
            {
                switch (((HttpException)ex).GetHttpCode())
                {
                    case 404:
                        Response.Redirect("/Error");
                        break;
                    case 500:
                        Response.Redirect("/Error/Error500?err=" + ex.Message);
                        break;
                }
            }
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["AuthCookie"];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);

                CustomPrincipal principal = new CustomPrincipal(authTicket.Name);

                principal.UserId = serializeModel.UserId;
                principal.Username = serializeModel.UserName;
                //principal.FirstName = serializeModel.FirstName;
                //principal.LastName = serializeModel.LastName;
                //principal.Roles = serializeModel.RoleName.ToArray<string>();

                HttpContext.Current.User = principal;
            }

        }
    }
}
