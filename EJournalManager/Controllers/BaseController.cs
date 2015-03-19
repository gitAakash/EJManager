using System.Web.Mvc;
using System.Web.Routing;
using EJournalManager.Entity;

namespace EJournalManager.Controllers
{
    public class BaseController : Controller
    {
        private string _currentUserName;

        public string CurrentUserName
        {
            get
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                    _currentUserName = System.Web.HttpContext.Current.User.Identity.Name;
                else
                    _currentUserName = null;
                return _currentUserName;
            }
        }

        public EJournalSession EJournalSession { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session != null)
            {
                if (Session["EJOURNAL_USER_SESSION"] != null)
                    EJournalSession = (EJournalSession) Session["EJOURNAL_USER_SESSION"];
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session == null)
                filterContext.Result = RedirectToAction("LogIn", "Account");
            else if (Session["EJOURNAL_USER_SESSION"] == null)
                filterContext.Result = RedirectToAction("LogIn", "Account");
            else
                base.OnActionExecuting(filterContext);
        }
    }
}