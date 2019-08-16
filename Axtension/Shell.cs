using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Axtension
{
	public static class Shell
	{
		public static string[] cmdShell(string path, string args)
		{
			return callShell(Environment.GetEnvironmentVariable("ComSpec"), path, args);
		}

		public static string[] exeShell(string exe, string path, string args)
		{
			return callShell(exe, path, args);
		}

		private static string[] callShell(string file, string path, string arg)
		{
			Process p = new Process();
			p.StartInfo.FileName = file;
			p.StartInfo.Arguments = arg;
			p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.WorkingDirectory = path;
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.CreateNoWindow = false;
			p.StartInfo.RedirectStandardError = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.Start();
			string errText = p.StandardError.ReadToEnd();
			string outText = p.StandardOutput.ReadToEnd();
			p.WaitForExit();
			string[] result = new String[2];
			result[0] = outText;
			result[1] = errText;
			p.Dispose();
			return result;
		}

	}
}
