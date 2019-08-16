namespace Axtension
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    public class Track : IDisposable
    {
        private TrackTypes trackType;
        private string trackFile = string.Empty;
        private bool trackOpen = false;
        private bool trackFlag = false;
        private FileStream trackHandle = null;
        private List<string> trackHold = new List<string>();

        public Track()
        {
            string tag = DateTime.Now.ToString("yyyy'-'MM'-'dd'-'HH'-'mm'-'ss") + ".track";
            this.trackFile = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)), tag);
            return;
        }

        public Track(string trackDir = @".\")
        {
            string tag = DateTime.Now.ToString("yyyy'-'MM'-'dd'-'HH'-'mm'-'ss") + ".track";
            this.trackFile = Path.Combine(trackDir, tag);
            return;
        }

        public enum TrackTypes
        {
            UNBUFFERED,
            BUFFERED,
            WITHHELD
        }

        public void DeleteCurrent()
        {
            if (File.Exists(this.trackFile))
            {
                File.Delete(this.trackFile);
            }

            return;
        }

        public string GetPath()
        {
            return this.trackFile;
        }

        public void SetPath(string path)
        {
            this.trackFile = path;
        }

        public void StartTrack()
        {
            this.trackFlag = true;
        }

        public void StopTrack()
        {
            this.trackFlag = false;
        }

        public void SetTrackType(string str)
        {
            if (!this.trackFlag)
            {
                return;
            }

            if (str.ToLower() == "unbuffered")
            {
                this.trackType = TrackTypes.UNBUFFERED;
            }
            else
            {
                if (str.ToLower() == "buffered")
                {
                    this.trackType = TrackTypes.BUFFERED;
                }
                else
                {
                    if (str.ToLower() == "withheld")
                    {
                        this.trackType = TrackTypes.WITHHELD;
                    }
                }
            }
        }

        public void SetTrackType(TrackTypes tt)
        {
            this.trackType = tt;
            return;
        }

        public void Write(params object[] args)
        {
            if (null == args[0])
            {
                args = new object[] { string.Empty };
            }

            if (!this.trackFlag)
            {
                return;
            }

            string theCaller = new StackFrame(1, true).GetMethod().Name + ":";
            string theArgs = string.Empty;
            for (int i = 0; i < args.Length; i++)
            {
                if (null == args[i])
                {
                    theArgs = theArgs + "NULL";
                }
                else
                {
                    theArgs = theArgs + args[i].ToString();
                }

                if (i < (args.Length - 1))
                {
                    theArgs += " ";
                }
            }

            if (this.trackType == TrackTypes.WITHHELD)
            {
                this.trackHold.Add(theCaller + theArgs);
            }
            else
            {
                if (!this.trackOpen)
                {
                    this.trackHandle = new FileStream(this.trackFile, FileMode.Append, FileAccess.Write);
                    this.trackOpen = true;
                }

                StreamWriter sw = new StreamWriter(this.trackHandle);
                sw.WriteLine(theCaller + theArgs);
                sw.Flush();
                if (this.trackType == TrackTypes.UNBUFFERED)
                {
                    this.trackHandle.Close();
                    this.trackOpen = false;
                }
            }
        }

        public void Commit()
        {
            if (!this.trackFlag)
            {
                return;
            }

            if (this.trackType == TrackTypes.WITHHELD)
            {
                this.trackHandle = new FileStream(this.trackFile, FileMode.Create, FileAccess.Write);
                string data = string.Join("\n", this.trackHold.ToArray());
                StreamWriter sw = new StreamWriter(this.trackHandle);
                sw.WriteLine(data);
                sw.Flush();
                this.trackHandle.Close();
            }
            else
            {
                if (this.trackType == TrackTypes.BUFFERED)
                {
                    this.trackHandle.Close();
                }
            }
        }

        public string Recall()
        {
            return string.Join("\n", this.trackHold.ToArray());
        }

        public void Dispose()
        {
            ((IDisposable)trackHandle).Dispose();
        }

        /*


Track.prototype.pad = function (n, pad) {
        var d = Math.pow(10, pad), c = Math.abs(n);
        return c < d ? (0 > n ? "-" : "") + (d + c).toString().substring(1) : n;
};

Track.prototype.stamp = function() {
       var d = new Date();
       return ( d.getYear() + '-' + this.pad(d.getMonth() + 1, 2) + '-' + this.pad(d.getDate(),2) + '-' + this.pad(d.getHours(),2) + '-' + this.pad(d.getMinutes(),2) );
}
        
var T = new Track();
T.define("withheld");
T.Write();
T.Write("foo");
function foo(x) {
    T.Write(x);
}
foo("bar");
WScript.Echo(T.echo("splat"));
T.commit();
 
         */
    }
}
