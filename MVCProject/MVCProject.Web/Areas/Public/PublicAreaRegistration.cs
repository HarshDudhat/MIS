namespace MVCProject.Areas.Public
{
    using System.Web.Mvc;
    using System.Web.Optimization;

    public class PublicAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Landing name string object.
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "Public";
            }
        }

        /// <summary>
        /// Register area for landing page.
        /// </summary>
        /// <param name="context">area registration context object.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            this.RegisterRoutes(context);
            this.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Register routes for landing page.
        /// </summary>
        /// <param name="context">area registration context object.</param>
        public void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapRoute(
                 "Public_default",
                 "{controller}/{action}/{id}",
                 new { action = "Index", id = UrlParameter.Optional }
                 ,new { controller = "(Home|Event|News)" }
             );

            context.MapRoute(
                 "Public_All",
                 "public/{controller}/{action}/{id}",
                 new { action = "Index", id = UrlParameter.Optional }
             );
        }

        /// <summary>
        /// Register Audit Bundles
        /// </summary>
        /// <param name="bundles">Bundle Collection</param>
        private void RegisterBundles(BundleCollection bundles)
        {
            //Landing Master
            //bundles.Add(new ScriptBundle("~/bundles/Public/Master")
            //  .Include("~/Areas/Public/Scripts/angular/services/HomeService.js")
            //  .Include("~/Areas/Public/Scripts/angular/controllers/PublicMasterCtrl.js")
            //  );

        }
    }
}
