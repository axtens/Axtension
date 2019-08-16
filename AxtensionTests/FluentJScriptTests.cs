using Microsoft.VisualStudio.TestTools.UnitTesting;
using Axtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axtension.Tests
{
    [TestClass()]
    public class FluentJScriptTests
    {
        [TestMethod()]
        public void FluentJScriptTest()
        {
            FluentJScript fj1 = new FluentJScript();
            FluentJScript fj2 = new FluentJScript();
            Assert.AreEqual(fj1.ToObject(), fj2.ToObject());
        }

        [TestMethod()]
        public void ClearTest()
        {
            FluentJScript fj1 = new FluentJScript();
            fj1.AddScript("1+1;");
            fj1.Clear();
            Assert.AreEqual(fj1.CurrentScript(),string.Empty);
        }

        [TestMethod()]
        public void AddScriptTest()
        {
            FluentJScript fj1 = new FluentJScript();
            fj1.AddScript("1+1;");
            Assert.AreEqual(fj1.CurrentScript(),"1+1;\n");
        }

        [TestMethod()]
        public void ExecuteTest()
        {
            FluentJScript fj1 = new FluentJScript();
            fj1.AddScript("1+1");
            fj1.Execute();
            Assert.AreEqual(fj1.ToString(),"2");
        }

        [TestMethod()]
        public void ExecuteTest1()
        {
            FluentJScript fj1 = new FluentJScript();
            fj1.Execute("1+1;");
            Assert.AreEqual(fj1.ToString(),"2");
        }

        [TestMethod()]
        public void ShutdownTest()
        {
            FluentJScript fj1 = new FluentJScript();
            fj1.Shutdown();
            Assert.IsNull(fj1.ToObject());
        }

        [TestMethod()]
        public void ToObjectTest()
        {
            FluentJScript fj1 = new FluentJScript();
            fj1.AddScript("1+1;");
            fj1.Execute();
            Assert.AreEqual(String.Format("{0}",fj1.ToObject()), "2");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            FluentJScript fj1 = new FluentJScript();
            fj1.Execute("1+1;");
            Assert.AreEqual(fj1.ToString(), "2");
        }
    }
}