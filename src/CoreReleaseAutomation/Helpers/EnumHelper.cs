using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreReleaseAutomation.Helpers
{
    public static class EnumHelper
    {
        public enum Status
        {
            Open,
            WaitingApproval,
            Approved,
            Cancelled
        }
    }
}
