namespace Axtension
{
    using System.Collections;
    using System.Collections.Specialized;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;
    //using Newtonsoft.Json;
    using Microsoft.ClearScript;
    using Microsoft.ClearScript.Windows;

    public class FluentJSON
    {
        private string _json = "";
        //private JavaScriptSerializer _serialized = null;
        private object _deserialized = null;
        
        public FluentJSON()
        {

        }
        public FluentJSON(string json = "")
        {
            this._json = json;
        }
        public FluentJSON Clear()
        {
            this._json = string.Empty;
            this._deserialized = null;
            return this;
        }
        public FluentJSON Load(string json = "")
        {
            this._json = json;
            return this;
        }
        public FluentJSON Deserialize()
        {
            this._deserialized = (new JavaScriptSerializer()).DeserializeObject(this._json);
            //this._deserialized = DeserializeObject(this._json);
            return this;
        }
        public object ToObject()
        {
            return (object)this._deserialized;
        }
    }
}
