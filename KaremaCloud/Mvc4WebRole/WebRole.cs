using Microsoft.WindowsAzure.ServiceRuntime;

namespace Mvc4WebRole
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            SessionLogger.AddLogInit("OnStart Role");

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
