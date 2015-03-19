using System;
using System.Web;

namespace EJournalManager.Entity
{
    public static class EJournalSessionManager
    {
        public static void SetSessionInformation(EJournalSession sessionInformation)
        {
            if (HttpContext.Current.Session["EJOURNAL_USER_SESSION"] == null)
            {
                HttpContext.Current.Session.Add("EJOURNAL_USER_SESSION", sessionInformation);
            }
        }

        public static void RemoveSessionInformation()
        {
            HttpContext.Current.Session["EJOURNAL_USER_SESSION"] = null;
        }

        public static EJournalSession GetSessionInformation()
        {
            if (HttpContext.Current.Session["EJOURNAL_USER_SESSION"] as EJournalSession != null)
            {
                var session = HttpContext.Current.Session["EJOURNAL_USER_SESSION"] as EJournalSession;
                return session;
            }
            return null;
        }

        public static Int64 GetCurrentlyLoggedInUserId()
        {
            Int64 loggedInUserId = 0;

            if (GetSessionInformation() == null)
            {
                return loggedInUserId;
            }
            loggedInUserId = GetSessionInformation().UserId;
            return loggedInUserId;
        }

        /*public static UserRoleEnum? GetUserRoleOfCurrentlyLoggedInUser()
        {
            if (HttpContext.Current.Session["EJOURNAL_USER_SESSION"] as EJournalSession != null)
            {
                var sessionInfo = HttpContext.Current.Session["EJOURNAL_USER_SESSION"] as EJournalSession;
                if (sessionInfo != null)
                    return sessionInfo.UserRole;
            }
            return null;
        }

        public static Int64? GetUserRoleIdOfCurrentlyLoggedInUser()
        {
            if (HttpContext.Current.Session["EJOURNAL_USER_SESSION"] as EJournalSession != null)
            {
                var sessionInfo = HttpContext.Current.Session["EJOURNAL_USER_SESSION"] as EJournalSession;
                if (sessionInfo != null)
                    return sessionInfo.UserRoleId;
            }
            return null;
        }*/
    }
}