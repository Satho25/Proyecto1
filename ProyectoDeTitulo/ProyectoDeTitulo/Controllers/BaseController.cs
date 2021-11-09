using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoDeTitulo.Models;
using System.Web.Mvc;

namespace ProyectoDeTitulo.Controllers
{
    public class BaseController : Controller { 
    //{
    //    private const string _keyUserLogin = "UserLogin";
    //    public string UserLogin
    //    {
    //        get
    //        {
    //            return Session[_keyUserLogin] as string;
    //        }
    //        set
    //        {
    //            Session[_keyUserLogin] = value;
    //        }
    //    }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            ViewBag.Message = "Your application description saasdsadpage.";
        }

    }
}