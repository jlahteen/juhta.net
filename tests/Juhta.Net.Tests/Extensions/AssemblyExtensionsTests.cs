
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
        public void GetCompany_AppXLibrary_ShouldReturnCompanyString()
        {
            Assembly assembly = typeof(AppXLibrary.LibraryHandle).Assembly;

            Assert.AreEqual<string>("Juha Lähteenmäki", assembly.GetCompany());
        }

        [TestMethod]
        public void GetCopyright_AppXLibrary_ShouldReturnCopyrightString()
        {
            Assembly assembly = typeof(AppXLibrary.LibraryHandle).Assembly;

            Assert.AreEqual<string>("Copyright © 2018 Juha Lähteenmäki", assembly.GetCopyright());
        }

        [TestMethod]
        public void GetProduct_AppXLibrary_ShouldReturnProductString()
        {
            Assembly assembly = typeof(AppXLibrary.LibraryHandle).Assembly;

            Assert.AreEqual<string>("AppXLibrary for Testing Purposes", assembly.GetProduct());
        }

        [TestMethod]
        public void GetProductVersion_AppXLibrary_ShouldReturnProductVersionString()
        {
            Assembly assembly = typeof(AppXLibrary.LibraryHandle).Assembly;

            Assert.AreEqual<string>("1.2.3-eternal-beta", assembly.GetProductVersion());
        }

        [TestMethod]
        public void GetVersion_AppXLibrary_ShouldReturnVersionString()
        {
            Assembly assembly = typeof(AppXLibrary.LibraryHandle).Assembly;

            Assert.AreEqual<string>("10.2.31.4", assembly.GetVersion());
        }

        #endregion
    }
}
