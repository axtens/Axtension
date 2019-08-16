namespace Axtension
{
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;

    public class FTP
    {
        private string ftpSite = string.Empty;
        private string ftpUser = string.Empty;
        private string ftpPass = string.Empty;
        private string ftpPort = "21";
        private bool ftpPassive = true;
        private bool ftpBinary = false;
        
        public FTP()
        {
        }

        public bool SetupFtp(string site, string user, string pass, string port = "21", bool passiv = true, bool binary = false)
        {
            this.ftpSite = site;
            this.ftpUser = user;
            this.ftpPass = pass;
            this.ftpPort = port;
            this.ftpPassive = passiv;
            this.ftpBinary = binary;
            return true;
        }

        public string NthFile(string dir, string searchPattern, int num)
        {
            string result = string.Empty;
            string[] res = this.ListFiles(dir, searchPattern);
            if (res[0] == "OK") 
            {
                string[] directory = Regex.Split(res[1], "\r\n", RegexOptions.Multiline);
                int last = directory.Length - 1;
                bool fromEnd = false;
                if (num < 0)
                {
                    fromEnd = true;
                    num = num * -1;
                }

                if (directory[last] == string.Empty) 
                {
                    last--;
                }

                if (fromEnd)
                {
                    result = directory[last - num + 1];
                }
                else
                {
                    result = directory[num - 1];
                }
            }

            var x = Regex.Match(result, @"^([d-])([rwxt-]{3}){3}\s+\d{1,}\s+.*?(\d{1,})\s+(\w+\s+\d{1,2}\s+(?:\d{4})?)(\d{1,2}:\d{2})?\s+(.+?)\s?$"); // regexr.com/?36qpk
            return x.Groups[6].Captures[0].Value;
        }

        public string[] ListFiles(string dir, string searchPattern)
        {
            string[] result = new string[3];
            string site = "ftp://" + this.ftpSite + dir + searchPattern;
            FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(site);
            req.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            req.Credentials = new NetworkCredential(this.ftpUser, this.ftpPass);
            req.UsePassive = true;
            req.UseBinary = false;

            FtpWebResponse resp = default(FtpWebResponse);
            try
            {
                resp = (FtpWebResponse)req.GetResponse();
            }
            catch (WebException e)
            {
                result[0] = "FAIL";
                result[1] = string.Empty;
                result[2] = e.Message;
                return result;
            }

            Stream respStream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(respStream);

            result[0] = "OK";
            result[1] = sr.ReadToEnd();
            result[2] = resp.StatusDescription;

            sr.Close();
            resp.Close();

            return result;
        }

        public string[] DownloadFile(string dir, string file)
        {
            string site = "ftp://" + this.ftpSite + dir + file;
            string[] result = new string[3];

            FtpWebRequest req = (FtpWebRequest)FtpWebRequest.Create(site);

            req.Method = WebRequestMethods.Ftp.DownloadFile;
            req.Credentials = new NetworkCredential(this.ftpUser, this.ftpPass);
            req.UsePassive = true;
            req.UseBinary = false;

            FtpWebResponse resp = default(FtpWebResponse);
            try
            {
                resp = (FtpWebResponse)req.GetResponse();
            }
            catch (WebException e)
            {
                result[0] = "FAIL";
                result[1] = string.Empty;
                result[2] = e.Message;
                return result;
            }

            Stream respStream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(respStream);

            result[0] = "OK";
            result[1] = sr.ReadToEnd();
            result[2] = resp.StatusDescription;

            sr.Close();
            resp.Close();

            return result;
        }
    }
}
