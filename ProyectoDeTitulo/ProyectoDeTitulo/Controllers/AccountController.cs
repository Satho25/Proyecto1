using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProyectoDeTitulo.CustomAuthentication;
using ProyectoDeTitulo.Models;

namespace ProyectoDeTitulo.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult LogIn(string returnUrl){
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginViewModel loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginView.Usuario, loginView.Password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(loginView.Usuario, false);
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new Models.CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            UserName = loginView.Usuario
                            //FirstName = user.FirstName,
                            //LastName = user.LastName,
                            //RoleName = user.Roles.Select(r => r.RoleName).ToList()
                        };

                        string userData = Newtonsoft.Json.JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, loginView.Usuario, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("AuthCookie", enTicket);
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                        //return RedirectToAction("/");
                    }
                }
            }
            ModelState.AddModelError("", "Something Wrong : Username or Password invalid ^_^ ");
            return View(loginView);
        }
        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("AuthCookie", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Account", null);
        }
    }
}