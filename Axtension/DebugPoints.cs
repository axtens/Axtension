using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axtension
{
    public static class DebugPoints
    {
        private static List<string> DEBUGPOINTS = new List<string>();

        public static void RequestDebugPoint(string pointName)
        {
            DEBUGPOINTS.Add(pointName.ToUpper());
        }

        public static void ClearDebugPoint(string pointName)
        {
            if (DEBUGPOINTS.Contains(pointName.ToUpper()))
            {
                DEBUGPOINTS.Remove(pointName);
            }
        }

        public static bool DebugPointRequested(string pointName)
        {
            return DEBUGPOINTS.Contains(pointName.ToUpper());
        }

        public static List<string> GetDebugPoints()
        {
            return DEBUGPOINTS;
        }
    }
}
