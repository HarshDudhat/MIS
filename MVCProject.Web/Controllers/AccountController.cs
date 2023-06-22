//-----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="ASK E-Sqaure">
//     Copyright (c) ASK E-Sqaure 2018.
// </copyright>
//-----------------------------------------------------------------------
namespace MVCProject.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using MVCProject.ViewModel;

    /// <summary>
    /// Account related activities
    /// </summary>
    public class AccountController : BaseController
    {
        // GET: /Account/Login

        /// <summary>
        /// Redirect to login page
        /// </summary>
        /// <returns>ActionResult object</returns>
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Login()
        {
            if (this.Session["UserContext"] == null || Request.Url.PathAndQuery.Contains("noSession"))
            {
                ViewBag.IsSessionExpired = Request.QueryString["noSession"] != null ? true : false;
                return this.View();
            }
            else
            {
                return this.RedirectToAction("RedirectToDefaultUrl");
            }

            //return this.RedirectToAction("RedirectToDefaultUrl");
        }

        /// <summary>
        /// Logout and redirect user to Login screen
        /// </summary>
        /// <param name="isRedirectHome">Redirect to Home</param>
        /// <returns>ActionResult object</returns>
        public ActionResult Logout(bool isRedirectHome = false)
        {
            this.LogoutUser();
            this.Session.Clear();

            if (isRedirectHome)
            {
                return this.RedirectToAction("index", "Home");
            }
            else
            {
                if (Request.QueryString["noSession"] == null)
                {
                    return this.RedirectToAction("Login");
                }
                else
                {
                    string lastUrl = Request.UrlReferrer == null ? null : Request.UrlReferrer.OriginalString;
                    if (lastUrl.Contains("InternalAudit"))
                    {
                        lastUrl = lastUrl.Split('#')[0].Replace("InternalAudit", "MyAudit");
                    }

                    this.Session["LastUrl"] = lastUrl;
                    return this.RedirectToAction("Login", new { noSession = "y" });
                }
            }
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <param name="resetPasswordToken">reset password token</param>
        /// <returns>ActionResult object</returns>
        public ActionResult ResetPassword(int userId, string resetPasswordToken)
        {
            ViewBag.userId = userId;
            ViewBag.resetPasswordToken = resetPasswordToken;

            return this.View();
        }

        /// <summary>
        /// Redirect to home page.
        /// </summary>
        /// <returns>ActionResult object</returns>
        public ActionResult RedirectToHomePage()
        {
            return this.RedirectToAction("Index", "Home", new { area = "Public" });
        }

        /// <summary>
        /// Redirect to Default URL after login to Particular user
        /// </summary>
        /// <returns>ActionResult object</returns>
        public ActionResult RedirectToDefaultUrl()
        {
            if (this.Session["UserContext"] == null)
            {
                // No session, redirect to login                
                this.LogoutUser();
                return this.RedirectToAction("Login", "Account", new { noSession = "y" });
            }
            else
            {
                if (this.Session["LastUrl"] != null)
                {
                    string url = this.Session["LastUrl"].ToString();
                    this.Session["LastUrl"] = null;
                    Response.Redirect(url);
                }

                //UserContext userContext = (UserContext)this.Session["UserContext"];
                //UserContext.PagePermission generalPermission = userContext.PageAccess.Where(p => p.PageId == Pages.General.Designation || p.PageId == Pages.General.CommonConfiguartion).FirstOrDefault();
                //bool hasGeneralAccess = generalPermission.CanWrite || generalPermission.CanRead;

                //if (hasGeneralAccess)
                //{
                return RedirectToAction("Index", "ReviewReport", new { area = "MISReporting" });
                //}
                //else
                //{
                //    return this.RedirectToAction("ServerError", "Error", new { id = 404 });
                //}

            }

            //return RedirectToAction("Index", "Designation", new { area = "Configuration" });
        }

        /// <summary>
        /// Create session 
        /// </summary>
        /// <param name="context">all required variable for session</param>
        [HttpPost]
        public void CreateSession(UserContext context)
        {
            if (ModelState.IsValid && context != null)
            {
                this.Session["UserContext"] = context;
            }
        }

        /// <summary>
        /// Reset session 
        /// </summary>
        [HttpPut]
        public void ResetSession()
        {
            this.Session.Timeout = 60;
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }

        /// <summary>
        /// Check Session
        /// </summary>
        /// <returns>true or false</returns>
        [HttpGet]
        public bool HasSession()
        {
            return this.Session["UserContext"] != null;
        }

        /// <summary>
        /// Logout current User
        /// </summary>
        private void LogoutUser()
        {

            //try
            //{
            //    if (Request.Cookies["SAFEZydusSESSION" + Request.Url.Port] != null && Request.Cookies["SAFEZydusSESSION" + Request.Url.Port].Value.ToString() != string.Empty)
            //    {
            //        string apiBaseUrl = WebConfigurationManager.AppSettings["ApiBaseUrl"].ToString();
            //        string logoutApi = string.Format("{0}{1}", apiBaseUrl, "Account/LogOut");

            //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(logoutApi);
            //        request.Method = WebRequestMethods.Http.Post;
            //        request.ContentType = "application/json";
            //        request.ContentLength = 0;
            //        request.Headers.Add("__RequestAuthToken", Request.Cookies["SAFEZydusSESSION" + Request.Url.Port].Value.ToString());
            //        request.GetResponse();
            //        Request.Cookies["SAFEZydusSESSION" + Request.Url.Port].Expires = DateTime.Now.AddDays(-1);
            //    }
            //}
            //catch
            //{
            //}

        }
    }
}
