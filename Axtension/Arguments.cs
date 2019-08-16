namespace Axtension
{
    using System;
    using System.Collections.Generic;

    public static class CommandLineArguments
    {
        public static Dictionary<string, string> Arguments()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int c = 0;
            foreach (string arg in Environment.GetCommandLineArgs())
            {
                if (arg.StartsWith("/", StringComparison.CurrentCulture))
                {
                    int index = arg.IndexOf(":", c);
                    string name;
                    string value;
                    if (index != -1)
                    {
                        name = arg.Substring(1, index - 1);
                        value = arg.Substring(index + 1);
                    }
                    else
                    {
                        name = arg.Substring(1);
                        value = string.Empty;
                    }

                    dict.Add(name, value);
                }
            }

            return dict;
        }
    }
}
