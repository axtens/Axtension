using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Axtension
{
    public static class XML
    {

        public static string PrettyXml(string xml, bool ansi = true, bool omitXMLDeclaration = true, bool indent = true, bool newLineOnAttributes = true)
        {

            var stringBuilder = new StringBuilder();

            var element = XElement.Parse(xml);

            var settings = new XmlWriterSettings();
            settings.Encoding = ansi ? Encoding.ASCII : Encoding.Default;
            settings.OmitXmlDeclaration = omitXMLDeclaration;
            settings.Indent = indent;
            settings.NewLineOnAttributes = newLineOnAttributes;

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                element.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }

        public static XmlDocument ConvertHtmlToXml(TextReader reader)
        {
            // setup SgmlReader
            Sgml.SgmlReader sgmlReader = new Sgml.SgmlReader();
            sgmlReader.DocType = "HTML";
            sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
            sgmlReader.CaseFolding = Sgml.CaseFolding.ToLower;
            sgmlReader.InputStream = reader;

            // create document
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.XmlResolver = null;
            doc.Load(sgmlReader);
            return doc;
        }
        public static XmlDocument ConvertHtmlToXml(StreamReader reader)
        {
            // setup SgmlReader
            Sgml.SgmlReader sgmlReader = new Sgml.SgmlReader();
            sgmlReader.DocType = "HTML";
            sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
            sgmlReader.CaseFolding = Sgml.CaseFolding.ToLower;
            sgmlReader.InputStream = reader;

            // create document
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.XmlResolver = null;
            doc.Load(sgmlReader);
            return doc;
        }
    }
}
