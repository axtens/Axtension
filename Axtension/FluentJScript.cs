using System;
using Microsoft.ClearScript;
using Microsoft.ClearScript.Windows;
using System.Diagnostics;

namespace Axtension
{
    public class FluentJScript : IDisposable
    {
        private string _script = "";
        private VBScriptEngine _engine = null;
        private object _result = null;
        private bool _withDebug = false;

        public FluentJScript()
        {
            this._engine = new VBScriptEngine(WindowsScriptEngineFlags.None);
            this._engine.AddHostObject("host", new HostFunctions());
            this._engine.AddHostObject("xHost", new ExtendedHostFunctions());
            this._withDebug = false;
        }

        public FluentJScript(bool debug)
        {
            this._engine = new VBScriptEngine(debug ? WindowsScriptEngineFlags.EnableDebugging | WindowsScriptEngineFlags.EnableJITDebugging : WindowsScriptEngineFlags.None);
            this._engine.AddHostObject("host", new HostFunctions());
            this._engine.AddHostObject("xHost", new ExtendedHostFunctions());
            this._withDebug = debug;
        }

        public string CurrentScript()
        {
            return this._script;
        }

        public FluentJScript Clear()
        {
            this._script = string.Empty;
            return this;
        }

        public FluentJScript AddScript(string script = "")
        {
            this._script = this._script + script + "\n";
            return this;
        }

        public FluentJScript Execute()
        {
            try
            {
                this._result = _engine.Evaluate(this._script);
            }
            catch (Exception e)
            {
                Debug.Print("Script Execution Failure:");
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace);
            }
            return this;
        }

        public FluentJScript Execute(string script = "")
        {
            try
            {
                this._result = _engine.Evaluate(script);
            }
            catch (Exception e)
            {
                Debug.Print("Script Execution Failure:");
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace);
            }
            return this;
        }

        public string EvaluateToString(string script = "", bool stripNewlines = false)
        {
            try
            {
                this._result = _engine.Evaluate(script);
                string result = this._result.ToString();
                if (stripNewlines)
                {
                    result = result.Replace("\r\n", "");
                    result = result.Replace("\r", "");
                    result = result.Replace("\n", "");
                }
                return result;
            }
            catch (Exception e)
            {
                Debug.Print("Script Execution Failure:");
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace);
                return "undefined";
            }

        }

        public FluentJScript Shutdown()
        {
            this._engine.Dispose();
            return this;
        }

        public object ToObject()
        {
            return this._result;
        }

        public override string ToString()
        {
            if (null == this._result)
            {
                return "null";
            }
            else
            {
                return this._result.ToString();
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
                    this._engine.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FluentJScript() {
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
