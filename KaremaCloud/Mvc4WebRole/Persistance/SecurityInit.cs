using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Security;
using Mvc4WebRole.Models;
using WebMatrix.WebData;

namespace Mvc4WebRole
{
    public static class SercurityInit
    {

        public static void Init()
        {
            using ( var context = new UsersContext() )
            {
                if ( !context.Database.Exists() )
                {
                    SessionLogger.AddLogInit("Creating WebSecurity UserDB");

                    // Create the SimpleMembership database without Entity Framework migration schema
                    ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();

                    SessionLogger.AddLogFinished("Creating WebSecurity UserDB");
                }
            }

            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            SimpleRoleProvider srp = new SimpleRoleProvider(Roles.Provider);

            try
            {
                if (WebSecurity.UserExists("Administrator"))
                {
                    using (var ctx = new UsersContext())
                    {
                        var admin = ctx.UserProfiles.FirstOrDefault(t => t.UserName == "Administrator");
                        if (admin != null)
                            ctx.UserProfiles.Remove(admin);
                    }
                }

                //if ( srp.RoleExists("Admin") )
                //{
                //    srp.DeleteRole("Admin",false);
                //}

                //CreateRole(srp, "Administrator");
                //CreateRole(srp, "Editor");
                //CreateRole(srp, "Author");
                //CreateRole(srp, "Reader");
            }
            catch (Exception e)
            {
                SessionLogger.AddLog(e.Message);
            }
        }


        private static void CreateRole(SimpleRoleProvider srp, string roleName)
        {
            if ( !srp.RoleExists(roleName) )
            {
                srp.CreateRole(roleName);
            }
        }
    }
}