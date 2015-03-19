using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using EJournalManager.Data;
using EJournalManager.Entity;
using EJournalManager.Helper;
using log4net;

namespace EJournalManager.Controllers
{
    public class UserController : BaseController
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /User/
        DbUserRoles objDBUserRole = new DbUserRoles();
        DbBranch objDBBranch = new DbBranch();
        DbUsers objDBUsers = new DbUsers();
        DbRegion objDBRegion = new DbRegion();
        public ActionResult Index()
        {
            log.Info("At index screen of user");
            return View();
        }
        /// <summary>
        /// Create user roles
        /// </summary>
        /// <returns></returns>

       
        public ActionResult CreateRoles()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Message = "Role added successfully";
            }
            UserRole objUserRole = new UserRole();
            return View();
        }
        /// <summary>
        /// Post method to create user role
        /// </summary>
        /// <param name="userRoleModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateRoles(UserRole userRoleModel)
        {
            int success = objDBUserRole.InsertUserRole(userRoleModel);
            if (success == 0)
                TempData["message"] = "Role added successfully";
            return RedirectToAction("CreateRoles");
        }
        /// <summary>
        /// Check whether role with same name alredy exists or not
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult checkUserRoleExists(string name)
        {
            bool success = true;
            List<UserRole> listUserRoles = new List<UserRole>();
            listUserRoles = objDBUserRole.GetAllUserRoles();
            foreach (UserRole role in listUserRoles)
                if (role.Name == name)
                    success = false;
            return Json(success);
        }
        /// <summary>
        /// Create New User
        /// </summary>
        /// <returns></returns>
       
        public ActionResult CreateUser()
        {
            log.Info("About retrieving the list of users in database");
            UserModel objUser = new UserModel();
            objUser.RoleList = objDBUserRole.GetAllUserRoles().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            objUser.BranchList = objDBBranch.GetAllBranches().Where(a => a.IsDeleted == false).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            objUser.RegionList = objDBRegion.GetAllRegions().Where(b => b.IsDeleted == false).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(objUser);
        }

        /// <summary>
        /// save user details
        /// </summary>
        /// <param name="objUserModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUser(UserModel objUserModel)
        {
            log.Info("About retrieving the list of users in database 2");
            OTPGenerator.OTPGenerator getPass = new OTPGenerator.OTPGenerator();
            objUserModel.Password = getPass.GetOTP();
            string CreatedBy = User.Identity.Name;

            int success = objDBUsers.InsertUser(objUserModel, CreatedBy);
            if (success == 0)
            {
                int successEsc = objDBUsers.InsertEscalation(objUserModel);
                //send mail to user after successfully careting the user
                if (objUserModel.Email != null && objUserModel.Email != "")
                {
                    MailHelper ATMMailHelper = new MailHelper();
                    EmailMessage ATMEmailMessage = new EmailMessage();

                    ATMEmailMessage.Subject = "ATM Monitoring User";
                    ATMEmailMessage.To = objUserModel.Email;
                    ATMEmailMessage.From = ConfigurationManager.AppSettings["FROM_ADDR"];
                    ATMEmailMessage.Body = "";
                    ATMEmailMessage.TemplatePath = Constants.TemplatePath;
                    ATMEmailMessage.TemplateLogo = Constants.TemplateLogo;
                    ATMEmailMessage.Template = "UserCreation";
                    //ATMEmailMessage.MarkerList.Add("##PortalURL##", Constants.BasePath);
                    ATMEmailMessage.MarkerList.Add("{$firstname}", objUserModel.FirstName);
                    ATMEmailMessage.MarkerList.Add("{$lastname}", objUserModel.LastName);
                    ATMEmailMessage.MarkerList.Add("{$username}", objUserModel.Username);
                    ATMEmailMessage.MarkerList.Add("{$password}", objUserModel.Password);
                    //ATMEmailMessage.MarkerList.Add("{$url}", ConfigurationManager.AppSettings["AdminURL"]);
                    string message = ATMMailHelper.DraftMail(ATMEmailMessage);
                }
            }
            //if (success != 0)
            //    @ViewBag.Message = "This username already exists";
            //else
            //    @ViewBag.Message = "User created successfully";
            VmUserModel objVMUserModel = new VmUserModel();
            return RedirectToAction("Index", "UserManagement", objVMUserModel);

        }


        [HttpPost]
        public JsonResult GetRegionName(string BranchName)
        {
            log.Info("Get region name after selecting branch method is called");
            RegionModel objRegionModel = new RegionModel();
            objRegionModel = objDBUsers.GetRegionName(BranchName);
            return Json(objRegionModel, JsonRequestBehavior.AllowGet);
        }
    }
}