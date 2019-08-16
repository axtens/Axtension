using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Axtension
{
    public class SMS : IDisposable
    {
        /// static ApplicationLogging AL = new ApplicationLogging("Axtension.SMS", ".\\").SetGathering(false).SetSyslogging(false);



        public void Send(string recipient, string message)
        {
            Axtension.Mail m = new Mail();
            m.Postoffice = "mail.westnet.com.au";
            m.Port = 25;
            m.SSL = false;
            m.Account = "sms@strapper.net";
            m.Password = "d3edfr0gcaf3";
            try
            {
                m.Shriek("sms@strapper.net", recipient + "@directsms.com.au", string.Empty, message);
            }
            catch (Exception exc)
            {
                /// AL.Warning().Send("Could not send SMS", exc.Message);
                Axtension.Track T = new Track(Path.GetTempPath());
                T.SetTrackType(Track.TrackTypes.UNBUFFERED);
                T.Write(exc.Message, exc.StackTrace);
                T.Commit();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SMS() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
