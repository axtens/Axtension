using System;
using System.Diagnostics;
using System.Management;

namespace Axtension
{
    public static class Processes
    {
        public static int KillProcessAndChildren(int pid)
        {
            int _count = 0;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
                _count++;
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
            catch (Exception)
            {
                // Other exception. Already dead? Undeadable?
            }
            return _count;
        }

        public static bool ProcessExists(int id)
        {
            try
            {
                Process process = Process.GetProcessById(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IntPtr ProcessHwnd(int id)
        {
            try
            {
                Process process = Process.GetProcessById(id);
                return process.MainWindowHandle;
            }
            catch (Exception) 
            {
                return IntPtr.Zero;
            }
        }
    }
}
