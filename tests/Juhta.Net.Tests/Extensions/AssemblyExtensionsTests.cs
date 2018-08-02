
using Juhta.Net.Common;
using Juhta.Net.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Juhta.Net.Tests.Extensions
{
    [TestClass]
    public class AssemblyExtensionsTests
    {
        #region Test Methods

        [TestMethod]
        public void GetCompany_JuhtaNetAssembly_ShouldReturnCompanyString()
        {
            Assembly assembly = typeof(CommonMessages).Assembly;

            Assert.AreEqual<string>("Juha Lähteenmäki", assembly.GetCompany());
        }

        [TestMethod]
        public void GetCopyright_JuhtaNetAssembly_ShouldReturnCopyrightString()
        {
            Assembly assembly = typeof(CommonMessages).Assembly;

            Assert.AreEqual<string>("Copyright © 2018 Juha Lähteenmäki", assembly.GetCopyright());
        }

        [TestMethod]
        public void GetProduct_JuhtaNetAssembly_ShouldReturnProductString()
        {
            Assembly assembly = typeof(CommonMessages).Assembly;

            Assert.AreEqual<string>("Juhta.NET", assembly.GetProduct());
        }

        [TestMethod]
        public void GetProductVersion_JuhtaNetAssembly_ShouldReturnProductVersionString()
        {
            Assembly assembly = typeof(CommonMessages).Assembly;

            Assert.AreEqual<string>("1.0.0-rc", assembly.GetProductVersion());
        }

        [TestMethod]
        public void GetVersion_JuhtaNetAssembly_ShouldReturnVersionString()
        {
            Assembly assembly = typeof(CommonMessages).Assembly;

            Assert.AreEqual<string>("1.0.0.1", assembly.GetVersion());
        }

        #endregion
    }
}
