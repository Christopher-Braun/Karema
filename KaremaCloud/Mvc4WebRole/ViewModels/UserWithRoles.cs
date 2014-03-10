using System;
using System.Collections.Generic;

namespace Mvc4WebRole.ViewModels
{
    public class UserWithRoles
    {
        public List<RoleInfo> RoleInfos
        {
            get;
            set;
        }

        public int UserID { get; set; }

        public String UserName { get; set; }

    }
}