using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using EJournalManager.Data;
using EJournalManager.Entity;
using EJournalManager.Helper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using log4net;

namespace EJournalManager.Controllers
{
    public class UserManagementController : BaseController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index(string page, string sortdir, string sortColumn, VmUserModel objUserModel)
        {
            Log.Info("At index screen of user");
            var objDbUserRole = new DbUserRoles();
            var objDbBranch = new DbBranch();
            var objDbUsers = new DbUsers();
            List<VmUserModel> listUserModel = objDbUsers.GetAllUsers(page, sortdir, sortColumn, objUserModel);
            ViewBag.RoleList = objDbUserRole.GetAllUserRoles().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(CultureInfo.InvariantCulture)
            }).ToList();

            ViewBag.BranchList =
                objDbBranch.GetAllBranches().Where(x => x.IsDeleted == false).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(CultureInfo.InvariantCulture)
                }).ToList();

            return View(listUserModel);
        }

        /// <summary>
        ///     Results will be repopulated in grid after search
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sortdir"></param>
        /// <param name="sortColumn"></param>
        /// <param name="objUserModel"></param>
        /// <returns></returns>
        public PartialViewResult SearchUser(string page, string sortdir, string sortColumn, VmUserModel objUserModel)
        {
            Log.Info("At index screen of user");
            ViewBag.isrecordfound = false;
            var objDbUsers = new DbUsers();
            List<VmUserModel> listUserModel = objDbUsers.GetAllUsers(page, sortdir, sortColumn, objUserModel);

            //return View(ListAgentKiosk);
            return PartialView("_searchUserResult", listUserModel);
        }

        /// <summary>
        ///     Edit user details
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public ActionResult EditUser(string UserId)
        {
            Log.Info("At index screen of user");
            var objDbUser = new DbUsers();
            UserModel objUser = objDbUser.GetUserByID(UserId);

            var objDalRole = new DbCustRoleProvider();
            string systemUserRole = objDalRole.GetUserRolesByUserId(User.Identity.Name);
            var objDbUserRole = new DbUserRoles();
            var objDbBranch = new DbBranch();
            var objDbRegion = new DbRegion();

            objUser.RoleList = objDbUserRole.GetAllUserRoles().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(CultureInfo.InvariantCulture),
                Selected = x.Id == objUser.RoleId
            }).ToList();

            objUser.BranchList =
                objDbBranch.GetAllBranches().Where(a => a.IsDeleted == false).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(CultureInfo.InvariantCulture),
                    Selected = x.Id == objUser.BranchId
                }).ToList();


            string region =
                objDbRegion.GetAllRegions().Where(y => y.Id == objUser.RegionId).Select(x => x.Name).FirstOrDefault();
            objUser.RegionName = region;
            return View(objUser);
        }

        /// <summary>
        ///     Save user details after edit
        /// </summary>
        /// <param name="objUserModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUser(UserModel objUserModel)
        {
            EJournalSession objATm = EJournalSession;
            string updatedBy = User.Identity.Name;
            var objUser = new DbUsers();
            int success = objUser.UpdateUser(objUserModel, updatedBy);
            return RedirectToAction("Index", "UserManagement");
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult ExportUsersListToExcel()
        {
            const string fileName = "Users List";
            DataTable table = CreateDatatable();
            var arr = new[]
            {"First Name", "Last Name", "Branch", "Role", "Status", "Employee Id", "Email", "Phone Number"};
            var objExportData = new ExportDataController();
            objExportData.ExportToExcel(table, fileName, arr);
            return View();
        }

        public ActionResult ExportUsersListToPDF()
        {
            const string fileName = "Users List";
            DataTable table = CreateDatatable();
            var arr = new[]
            {"First Name", "Last Name", "Branch", "Role", "Status", "Employee Id", "Email", "Phone Number"};
            var objExportData = new ExportDataController();
            objExportData.ExportToPDF(table, fileName, arr);

            return View();
        }

        private void ExportToPDF(DataTable dt, string fileName)
        {
            // 
            // For PDF export we are using the free open-source iTextSharp library.
            //

            var pdfDoc = new Document();
            var pdfStream = new MemoryStream();
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, pdfStream);

            pdfDoc.Open(); //Open Document to write
            pdfDoc.NewPage();

            Font font8 = FontFactory.GetFont("ARIAL", 7);

            var pdfTable = new PdfPTable(dt.Columns.Count);
            PdfPCell pdfPCell = null;

            //Add Header of the pdf table
            for (int column = 0; column < dt.Columns.Count; column++)
            {
                pdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Columns[column].Caption, font8)));
                pdfTable.AddCell(pdfPCell);
            }

            //How add the data from datatable to pdf table
            for (int rows = 0; rows < dt.Rows.Count; rows++)
            {
                for (int column = 0; column < dt.Columns.Count; column++)
                {
                    pdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Rows[rows][column].ToString(), font8)));
                    pdfTable.AddCell(pdfPCell);
                }
            }

            pdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table            
            pdfDoc.Add(pdfTable); // add pdf table to the document
            pdfDoc.Close();
            pdfWriter.Close();

            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
            Response.BinaryWrite(pdfStream.ToArray());
            Response.End();
        }

        public ActionResult ExportToText()
        {
            const string fileName = "Users List";
            DataTable table = CreateDatatable();
            var arr = new[]
            {"First Name", "Last Name", "Branch", "Role", "Status", "Employee Id", "Email", "Phone Number"};
            var objExportData = new ExportDataController();
            objExportData.ExportToText(table, fileName, arr);
            return View();
        }

        /// <summary>
        ///     Export users list to excel
        /// </summary>
        /// <returns></returns>
        /// <summary>
        ///     Export users list to pdf
        /// </summary>
        /// <returns></returns>
        /// <summary>
        ///     Export users list to text file
        /// </summary>
        /// <returns></returns>
        /// <summary>
        ///     Create Datatable which will be used for exporting Excel,Pdf and text
        /// </summary>
        /// <returns></returns>
        public DataTable CreateDatatable()
        {
            var objDbUsers = new DbUsers();

            List<VmUserModel> listUserModel = objDbUsers.GetAllUsersForExport();


            var table = new DataTable();
            table.Columns.Add("First Name", typeof (string));
            table.Columns.Add("Last Name", typeof (string));
            table.Columns.Add("Branch", typeof (string));
            table.Columns.Add("Role", typeof (string));
            table.Columns.Add("Status", typeof (string));
            table.Columns.Add("Employee Id", typeof (string));
            table.Columns.Add("Email", typeof (string));
            table.Columns.Add("Phone Number", typeof (string));

            for (int i = 1; i < listUserModel.Count; i++)
            {
                DataRow row = table.NewRow();

                row["First Name"] = listUserModel[i].FirstName ?? "";
                row["Last Name"] = listUserModel[i].LastName ?? "";
                row["Branch"] = listUserModel[i].BranchName ?? "";
                row["Role"] = listUserModel[i].RoleName ?? "";
                row["Status"] = listUserModel[i].Status ?? "";
                row["Employee Id"] = listUserModel[i].EmployeeId == 0 ? "" : listUserModel[i].EmployeeId.ToString();
                row["Email"] = listUserModel[i].Email ?? "";
                row["Phone Number"] = listUserModel[i].PhoneNumber ?? "";
                table.Rows.Add(row);
            }
            return table;
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult ForgotPassword(int UserId)
        {
            var objDbUsers = new DbUsers();
            var getPass = new OTPGenerator.OTPGenerator();
            UserModel objUserModel = objDbUsers.GetUserByID(UserId.ToString(CultureInfo.InvariantCulture));
            objUserModel.Password = getPass.GetOTP();
            string createdBy = User.Identity.Name;

            int success = objDbUsers.UpdateUserPassword(objUserModel, createdBy);
            if (success != 0)
            {
                //send mail to user after successfully careting the user
                if (!string.IsNullOrEmpty(objUserModel.Email))
                {
                    var atmMailHelper = new MailHelper();
                    var atmEmailMessage = new EmailMessage();

                    atmEmailMessage.Subject = "ATM Monitoring User Password Reset";
                    atmEmailMessage.To = objUserModel.Email;
                    atmEmailMessage.From = "tejal.satre@wwindia.com";
                    atmEmailMessage.Body = "";
                    atmEmailMessage.TemplatePath = Constants.TemplatePath;
                    atmEmailMessage.TemplateLogo = Constants.TemplateLogo;
                    atmEmailMessage.Template = "ResetPassword";
                    //ATMEmailMessage.MarkerList.Add("##PortalURL##", Constants.BasePath);
                    atmEmailMessage.MarkerList.Add("{$firstname}", objUserModel.FirstName);
                    atmEmailMessage.MarkerList.Add("{$lastname}", objUserModel.LastName);
                    atmEmailMessage.MarkerList.Add("{$password}", objUserModel.Password);
                    //ATMEmailMessage.MarkerList.Add("{$url}", ConfigurationManager.AppSettings["AdminURL"]);
                    string message = atmMailHelper.DraftMail(atmEmailMessage);
                }
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}