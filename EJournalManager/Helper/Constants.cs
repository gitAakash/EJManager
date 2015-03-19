using System.Collections.Generic;
using System.Web;
using System.Web.WebPages.Html;

namespace EJournalManager.Helper
{
    public static class Constants
    {
        #region Path

        public static string TemplatePath = HttpRuntime.AppDomainAppPath + @"EmailTemplates\";
        public static string TemplateLogo = HttpRuntime.AppDomainAppPath + @"assets\img\logo.jpg";

        #endregion

        public enum Intervals
        {
            Interval_1 = 0,
            Interval_2 = 20,
            Interval_3 = 40,
            Interval_4 = 60,
            Interval_5 = 80

        }

        public static IEnumerable<SelectListItem> GetIntervals()
        {
            List<SelectListItem> Intervals = new List<SelectListItem>();
            Intervals.Add(new SelectListItem() { Text = "0", Value = "0" });
            Intervals.Add(new SelectListItem() { Text = "20", Value = "20" });
            Intervals.Add(new SelectListItem() { Text = "40", Value = "40" });
            Intervals.Add(new SelectListItem() { Text = "60", Value = "60" });
            Intervals.Add(new SelectListItem() { Text = "80", Value = "80" });
            return Intervals;
        }

        public static IEnumerable<SelectListItem> GetResolvedStatus()
        {
            List<SelectListItem> ResolvedStatus = new List<SelectListItem>();
            ResolvedStatus.Add(new SelectListItem() { Text = "Open", Value = "0" });
            ResolvedStatus.Add(new SelectListItem() { Text = "Closed", Value = "1" });

            return ResolvedStatus;
        }

        public enum TransactionTypes
        {
            Pending = 00,
            Wrong_Pin = 32,
            Success = 39,
            incomplete = 91,
            complete = 93,
            test = 21,
            test1 = 52,
            Balance_enquiry = 31,
            cash_withdrawal = 01,
            Mini_Statement = 38,
            Funds_Transfer = 40,
            payment_from_account = 50,
            Card_accounts_enquiry = 92

        }

    }
}