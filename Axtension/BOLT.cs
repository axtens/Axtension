using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Axtension;
using System.IO;

namespace Axtension
{
    public class BOLT
    {
        private string _guid;
        private string _category;
        private string _domain;
        private string _message;
        
        public BOLT()
        {
            this._domain = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            this._guid = System.Guid.NewGuid().ToString();
            this._category = "I";
            this._message = string.Empty;
        }

        public BOLT Domain(string dom)
        {
            this._domain = dom;
            return this;
        }

        public BOLT Category(string cat)
        {
            this._category = cat;
            return this;
        }

        public BOLT Informational()
        {
            return this.Category("I");
        }

        public BOLT Warning()
        {
            return this.Category("W");
        }

        public BOLT Error()
        {
            return this.Category("E");
        }

        public BOLT Fatal()
        {
            return this.Category("F");
        }

        public BOLT Guid(string uid)
        {
            this._guid = uid;
            return this;
        }

        public BOLT Message(string msg)
        {
            this._message = msg;
            return this;
        }

        public BOLT Send(params object[] args)
        {
            RESTful rf = new RESTful();
            rf
                .Url("http://localhost:43210/tracks")
                .Verb("PUT");

            string msg = string.Empty;

            if (args.Length == 0)
            {
                msg = this._message;
            }
            else
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
                msg = String.Join(" ", L.ToArray());
            }

            rf.Tail(String.Format("d={0}&c={1}&g={2}", this._domain, this._category, this._guid));
            rf.RawBody(msg);

            int retryCount = 0;
            do
            {
                try
                {
                    rf.Send();
                    break;
                }
                catch (Exception)
                {
                    retryCount++;
                    if (retryCount < 4)
                    {
                        continue;
                    }
                    //probably not running. Store in TSV
                    string save = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\r\n", UnixTicks(DateTime.Now), this._domain, this._guid, this._category, msg);
                    File.AppendAllText("BOLT.TSV", save);
                    break;
                }
            } while (true);
            return this;
        }
        public double UnixTicks(DateTime dt)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = dt.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return Math.Floor(ts.TotalMilliseconds);
        }
    }
}
