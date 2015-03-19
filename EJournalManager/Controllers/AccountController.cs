using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EJournalManager.CustomMembership;
using EJournalManager.Data;
using EJournalManager.Entity;

namespace EJournalManager.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        //  [ValidateAntiForgeryToken]
        public ActionResult LogIn(User model, string returnUrl)
        {
            var customMembershipProvider = new CustomMembershipProvider();
            // try the default membership auth if active directory fails
            if (ModelState.IsValid && Membership.ValidateUser(model.UserName, model.Password))
            {
                var objDalRole = new DbCustRoleProvider();
                string userRole = objDalRole.GetUserRolesByUserId(model.UserName);
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                EJournalSession sessionInfo = EJournalSessionManager.GetSessionInformation();
                //Navigate to Client list page for Admin Client
                return RedirectToLocal(userRole, sessionInfo.RoleName);
            }

            // If we got this far, something failed, redisplay form
            //  ModelState.AddModelError("", "The user name or password provided is incorrect.");
            ModelState.AddModelError("UsrnamePassword", "The Username or password you entered is incorrect.");
            model.Password = "";
            return View(model);
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            EJournalSessionManager.RemoveSessionInformation();
            FormsAuthentication.SignOut();
            if (Request.Cookies["ST"] != null)
            {
                var cookie = new HttpCookie("ST") {Expires = DateTime.Now.AddDays(-1)};
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Login");
        }

        private ActionResult RedirectToLocal(string userRole, string role)
        {
            if (userRole == role)
            {
                return RedirectToAction("Index", "UserManagement");
            }
            if (userRole == role)
            {
                //return RedirectToAction("Index", "UserManagement");
                return RedirectToAction("Dashboard", "Atms");
            }
            if (userRole == role)
            {
                //return RedirectToAction("Index", "UserManagement");
                return RedirectToAction("Dashboard", "Atms");
            }
            if (userRole == role)
            {
                //return RedirectToAction("Index", "UserManagement");
                return RedirectToAction("Dashboard", "Atms");
            }
            if (userRole == role)
            {
                //return RedirectToAction("Index", "UserManagement");
                return RedirectToAction("Dashboard", "Atms");
            }
            if (userRole == role)
            {
                //return RedirectToAction("Index", "UserManagement");
                return RedirectToAction("Dashboard", "Atms");
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult ChangePassword()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                DbCustMembership objUser = new DbCustMembership();
                var currentUserName = User.Identity.Name;
                if (string.IsNullOrEmpty(currentUserName))
                    return RedirectToAction("LogIn", "Account");
                if (!(objUser.ChangePassword(currentUserName, model.Password, model.NewPassword)))
                {
                    ModelState.AddModelError("Password", "Password supplied was invalid");
                    return View(model);
                }
                return RedirectToAction("LogIn", "Account");
                //if (bResult)
                //    return RedirectToLocal(SystemUserRole);
            }
            return View();
        }
    }
}