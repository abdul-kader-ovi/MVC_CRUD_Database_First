using MVCDatabaseFirstProject_TrainingAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCDatabaseFirstProject_TrainingAcademy
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // GetRolesForUser();
            CerationInititalUserRole();
        }
        private void CerationInititalUserRole()
        {
            DBFirstTrainingDBEntities dbObj = new DBFirstTrainingDBEntities();
            using (var dbOb = new DBFirstTrainingDBEntities())
            {
                int count = dbOb.tblRoles.Count();
                if (count == 0)
                {
                    tblUser user = new tblUser();
                    user.UserName = "Admin";
                    user.Password = "123";
                }
            }
        }
        public class FilterConfig
        {
            public static void RegisterGlobalFilters(GlobalFilterCollection filters)
            {
                //filters.Add(new HandleErrorAttribute());
            }
        }
    }
}
