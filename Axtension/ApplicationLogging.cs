using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Axtension
{
    public class ApplicationLogging
    {
        static string _msg = null;
        enum LEVELS {INFORMATION, WARNING, ERROR, FATAL};
        static int _counter = 0;
        static LEVELS _level = LEVELS.INFORMATION;
        static string _path = Path.GetTempPath();
        static string _instance = "";
        static string _app = "";
        static Stack<string> _module = new Stack<string>();
        static bool _gathering = false;
        static StringBuilder _gathered = new StringBuilder();
        static bool _combined = false;
        static string _combinedPath = string.Empty;
        static bool _console = false;
        static bool _appfirst = false;
        static DateTime _timer = DateTime.Now;
        static bool _timing = false;

        public ApplicationLogging()
        {
            _app = "Axtension.";
            _instance = DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
        }

        public ApplicationLogging(string appName)
        {
            _app = appName;
            _instance = DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
        }

        public ApplicationLogging(string appName, string reportPath)
        {
            _app = appName;
            _path = reportPath;
            _instance = DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
        }

        public ApplicationLogging(string appName, string moduleName, string reportPath)
        {
            _app = appName;
            _module.Push( moduleName);
            _path = reportPath;
            _instance = DateTime.Now.ToString("yyyy-MM-dd-HH-mm");
        }

        public ApplicationLogging SetReportPathTo(string path = "")
        {
            if (path != string.Empty)
            {
                _path = path;
            }
            return this;
        }

        public ApplicationLogging Combined(bool yesno = false, string path = "")
        {
            _combined = yesno;
            _combinedPath = path;
            return this;
        }

        public ApplicationLogging ElapsedTime(bool yesno = false)
        {
            _timing = yesno;
            _timer = DateTime.Now;
            return this;
        }

        public ApplicationLogging Gathering(bool gathering)
        {
            _gathering = gathering;
            return this;
        }

        public ApplicationLogging ToConsole(bool flag)
        {
            _console = flag;
            return this;
        }

        public ApplicationLogging AlsoToConsole()
        {
            _console = true;
            return this;
        }

        public ApplicationLogging WithAppFirst()
        {
            _appfirst = true;
            return this;
        }

        public ApplicationLogging Module(string module)
        {
            _module.Push(module);
            return this;
        }

        public ApplicationLogging Module()
        {
            if (_module.Count > 1) // never let the stack be empty
            {
                _module.Pop();
            } 
            else
            {
                WriteMessageAndLevelToFile("WARNING: Tried to empty the Module stack.", LEVELS.WARNING);
            }
            return this;
        }

        public ApplicationLogging Informational()
        {
            _level = LEVELS.INFORMATION;
            return this;
        }

        public ApplicationLogging Warning()
        {
            _level = LEVELS.WARNING;
            return this;
        }

        public ApplicationLogging Error()
        {
            _level = LEVELS.ERROR;
            return this;
        }

        public ApplicationLogging Fatal()
        {
            _level = LEVELS.FATAL;
            return this;
        }

        public ApplicationLogging Category(string cat)
        {
            switch (cat.Substring(0, 1).ToUpper())
            {
                case "I":
                    _level = LEVELS.INFORMATION;
                    break;
                case "W":
                    _level = LEVELS.WARNING;
                    break;
                case "F":
                    _level = LEVELS.FATAL;
                    break;
                case "E":
                    _level = LEVELS.ERROR;
                    break;
                default:
                    _level = LEVELS.INFORMATION;
                    break;
            }
            return this;
        }

        private void WriteMessageAndLevelToFile(string message, LEVELS level)
        {
            WriteMessageAndLevelToFile(message, level.ToString().ToUpper());
        }

        private void WriteMessageAndLevelToFile(string message, string level)
        {
            string[] stack = _module.ToArray();
            Array.Reverse(stack);
            string stackPath = string.Join(".", stack);
            string txt;
            if (_timing)
            {
                TimeSpan ts = DateTime.Now - _timer;
                txt = string.Format("({0})-{1}-[{2:D6}]: {3}", level.Substring(0, 1).ToUpper(), stackPath, ts.Milliseconds, message);
                _timer = DateTime.Now;
                _counter++;
            } else
            {
                txt = string.Format("({0})-{1}-[{2}]: {3}", level.Substring(0, 1).ToUpper(), stackPath, _counter++, message);
            }
            

            if (_gathering)
            {
                _gathered.Append(txt + "\r\n");
            }

            int retry = 0;
            do
            {
                try
                {
                    System.IO.File.AppendAllText(System.IO.Path.Combine(_path, (_appfirst ? _app + "-" + _instance : _instance + "-" + _app) + ".txt"), txt + "\r\n");
                    break;
                }
                catch (DirectoryNotFoundException)
                {
                    System.IO.Directory.CreateDirectory(_path);
                }
                catch (Exception)
                {
                    System.Threading.Thread.Sleep(10);
                    retry++;
                    if (retry > 3)
                    {
                        break; //forget about it
                    }
                }
            } while (true);

            if (_console)
            {
                Console.WriteLine(txt);
            }

            if (_combined)
            {
                retry = 0;
                do
                {
                    try
                    {
                        System.IO.File.AppendAllText(System.IO.Path.Combine(_combinedPath,  _instance +  "-Combined.txt"), txt + "\r\n");
                        break;
                    }
                    catch (DirectoryNotFoundException)
                    {
                        System.IO.Directory.CreateDirectory(_combinedPath);
                    }
                    catch (Exception )
                    {
                        System.Threading.Thread.Sleep(10);
                        retry++;
                        if (retry > 3)
                        {
                            break; //forget about it
                        }
                    }
                } while (true);
            }
        }

        public ApplicationLogging Send(params object[] args)
        {
            System.Collections.Generic.List<string> L = new System.Collections.Generic.List<string>();
            foreach (object arg in args)
            {
                string sArg = "";
                if (arg == null)
                {
                    sArg = "(null)";
                }
                else
                {
                    sArg = arg.ToString();
                }
                L.Add(sArg);
            }
            _msg = String.Join(" ", L.ToArray());
            WriteMessageAndLevelToFile(_msg,_level);
            return this;
        }

        public ApplicationLogging Inform(string msg)
        {
            WriteMessageAndLevelToFile(msg, LEVELS.INFORMATION);
            return this;
        }

        public ApplicationLogging Warn(string msg)
        {
            WriteMessageAndLevelToFile(msg, LEVELS.WARNING);
            return this;
        }

        public ApplicationLogging Fail(string msg)
        {
            WriteMessageAndLevelToFile(msg, LEVELS.FATAL);
            return this;
        }

        public string GetGathered()
        {
            return _gathered.ToString();
        }
    }
}
