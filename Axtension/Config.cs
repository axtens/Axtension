namespace Axtension
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using System;

    public class Config
    {
        /// static ApplicationLogging AL = new ApplicationLogging("Axtension.Config", ".\\").SetGathering(false).SetSyslogging(false);
        private string text = string.Empty;
        private string path = string.Empty;
        private bool loaded = false;
        private bool retrieved = false;
        private bool errOnNull = false;

        public Config()
        {
            this.loaded = false;
            this.text = string.Empty;
            this.path = Path.ChangeExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, ".cfg");
            if (File.Exists(this.path))
            {
                this.text = File.ReadAllText(this.path);
                this.text = this.text.Replace("\r\n", "\n");
                this.loaded = true;
            }
            else
            {
                this.text = string.Empty;
                this.loaded = false;
            }

            return;
        }

        public Config(string filename)
        {
            this.loaded = false;
            this.path = filename;
            if (File.Exists(this.path))
            {
                this.text = File.ReadAllText(this.path);
                this.text = this.text.Replace("\r\n", "\n");
                this.loaded = true;
            }
            else
            {
                this.text = string.Empty;
                this.loaded = false;
            }

            return;
        }

        public string Retrieve(string symbol)
        {
            this.retrieved = false;
            Regex regex = new Regex(@"^" + symbol + "=(.*?)$", RegexOptions.Multiline|RegexOptions.CultureInvariant);
            Match match = regex.Match(this.text);
            if (match.Success)
            {
                this.retrieved = true;
                return match.Groups[1].Value;
            }
            else
            {
                if (this.errOnNull)
                {
                    Console.WriteLine("Error: Cannot find symbol '{0}' in {1}", symbol, this.path);
                    //Environment.Exit(1);
                    return string.Empty;            
                } else
                {
                    return null;
                }

            }
        }

        public Dictionary<object, object> RetrieveAll()
        {
            Dictionary<object, object> dict = new Dictionary<object, object>();
            Regex regex = new Regex(@"^(.*?\=.*?)$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(this.text);
            foreach (Match m in matches)
            {
                string[] parts = m.Value.Split(new char[] { '=' }, 2);
                dict[parts[0]] = parts[1];
            }

            return dict;
        }

        public int Retrieve(string symbol, int defaultValue)
        {
            string result = this.Retrieve(symbol, defaultValue.ToString());
            return int.Parse(result);
        }

        public bool Retrieve(string symbol, bool defaultValue)
        {
            string result = this.Retrieve(symbol, defaultValue.ToString());
            return bool.Parse(result);
        }

        public string Retrieve(string symbol, string defaultValue)
        {
            this.retrieved = false;
            Regex regex = new Regex(@"^" + symbol + "=(.*?)$", RegexOptions.Multiline);
            Match match = regex.Match(this.text);
            if (match.Success)
            {
                this.retrieved = true;
                return match.Groups[1].Value;
            }
            else
            {
                return defaultValue;
            }
        }

        public void Define(string symbol, int value)
        {
            string symbolsValue = value.ToString();
            this.Define(symbol, symbolsValue);
        }

        public void Define(string symbol, string value)
        {
            string symbolValuePair = symbol + "=" + value;
            Regex regex = new Regex(@"^" + symbol + "=(.*?)$", RegexOptions.Multiline);
            Match match = regex.Match(this.text);
            if (match.Success)
            {
                this.text = regex.Replace(this.text, symbolValuePair);
            }
            else
            {
                this.text = this.text + "\n" + symbolValuePair;
            }
        }

        public void Save()
        {
            File.WriteAllText(this.path, this.text);
        }

        public void Save(string filename)
        {
            File.WriteAllText(filename, this.text);
        }

        public string GetFilename()
        {
            return this.path;
        }

        public bool WasLoaded()
        {
            return this.loaded;
        }

        public bool WasRetrieved()
        {
            return this.retrieved;
        }

        public void SetErrorOnNull(bool f = false)
        {
            this.errOnNull = f;
            return;
        }
    }
}
