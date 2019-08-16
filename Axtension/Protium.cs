namespace Axtension
{
    using System.Diagnostics;
    using System.IO;

    public static class Protium
    {
        public static string PExe(string path, string file, string commandLine)
        {
            return Exe(@"C:\protium\bin\p.exe", path, file, "/Q=0 " + commandLine);
        }

        public static string PdbExe(string path, string file, string commandLine)
        {
            return Exe(@"C:\protium\bin\pdb.exe", path, file, "/Q=0 " + commandLine);
        }

        public static string P(params object[] opargs)
        {
            switch (opargs.Length)
            {
                case 0:
                    return "</@>";
                case 1:
                    return "<@ " + opargs[0].ToString() + ">";
                default:
                    string str = string.Empty;
                    for (int i = 1; i < opargs.Length; i++)
                    {
                        str = str + opargs[i].ToString();
                        if (i < opargs.Length - 1)
                        {
                            str = str + "|";
                        }
                    }

                    return "<@ " + opargs[0] + ">" + str + "</@>";
            }
        }

        private static string Exe(string exeName, string path, string file, string commandLine)
        {
            Process p = new Process();
            p.StartInfo.FileName = exeName;
            p.StartInfo.Arguments = file + " " + commandLine;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WorkingDirectory = path;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
            string outFile = Path.ChangeExtension(file, ".out");
            p.Dispose();
            return File.ReadAllText(outFile);
        }
    }
}
