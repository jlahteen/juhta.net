
using Juhta.Net;
using System;
using System.IO;
using System.Xml.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class StartupTests : TestClassBase
    {
        #region Test Methods

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CloseFramework_FrameworkNotInitialized_ShouldThrowInvalidOperationException()
        {
            Startup.CloseFramework();
        }

        [TestMethod]
        public void InitializeFramework_NoParameters_NoConfigFiles_ShouldReturn()
        {
            Startup.InitializeFramework();

            Startup.CloseFramework();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InitializeFramework_TwoSubsequentCalls_ShouldThrowInvalidOperationException()
        {
            try
            {
                Startup.InitializeFramework();

                Startup.InitializeFramework();
            }

            finally
            {
                Startup.CloseFramework();
            }
        }




        //[TestMethod]
        //public void InitializeFramework_EmptyNodesUnderInitializationNodeInRootConfig_ShouldReturn()
        //{
        //    try
        //    {
        //        BusinessFrameworkManager.InitializeFramework(SetCurrentConfig("Root", "EmptyNodesUnderInitializationNodeInRootConfig_"));
        //    }

        //    finally
        //    {
        //        BusinessFrameworkManager.CloseFramework();
        //    }
        //}

        //[TestMethod]
        //[ExpectedException(typeof(LibraryInitializationException))]
        //public void InitializeFramework_InvalidBuiltInLibraryConfig_ShouldThrowLibraryInitializationException()
        //{
        //    try
        //    {
        //        BusinessFrameworkManager.InitializeFramework(SetCurrentConfig("Root", "InvalidBuiltInLibraryConfig_"));
        //    }

        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex.InnerException, typeof(FileNotFoundException));

        //        throw;
        //    }

        //    finally
        //    {
        //        BusinessFrameworkManager.CloseFramework();
        //    }
        //}

        //[TestMethod]
        //public void InitializeFramework_LoadedExternalLibraryInRootConfig_ShouldReturn()
        //{
        //    try
        //    {
        //        BusinessFrameworkManager.InitializeFramework(SetCurrentConfig("Root", "LoadedExternalLibraryInRootConfig_"));
        //    }

        //    finally
        //    {
        //        BusinessFrameworkManager.CloseFramework();
        //    }
        //}

        //[TestMethod]
        //[ExpectedException(typeof(LibraryCreationException))]
        //public void InitializeFramework_NotAvailableCustomPlugInLibraryInRootConfig_ShouldThrowLibraryCreationException()
        //{
        //    try
        //    {
        //        BusinessFrameworkManager.InitializeFramework(SetCurrentConfig("Root", "NotAvailableCustomPlugInLibraryInRootConfig_"));
        //    }

        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex.InnerException, typeof(FileNotFoundException));

        //        throw;
        //    }

        //    finally
        //    {
        //        BusinessFrameworkManager.CloseFramework();
        //    }
        //}

        //[TestMethod]
        //[ExpectedException(typeof(LibraryCreationException))]
        //public void InitializeFramework_NotAvailablePlugInLibraryInRootConfig_ShouldThrowLibraryCreationException()
        //{
        //    try
        //    {
        //        BusinessFrameworkManager.InitializeFramework(SetCurrentConfig("Root", "NotAvailablePlugInLibraryInRootConfig_"));
        //    }

        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex.InnerException, typeof(FileNotFoundException));

        //        throw;
        //    }

        //    finally
        //    {
        //        BusinessFrameworkManager.CloseFramework();
        //    }
        //}

        //[TestMethod]
        //[ExpectedException(typeof(LibraryCreationException))]
        //public void InitializeFramework_NotLoadedExternalLibraryInRootConfig_ShouldThrowLibraryCreationException()
        //{
        //    try
        //    {
        //        BusinessFrameworkManager.InitializeFramework(SetCurrentConfig("Root", "NotLoadedExternalLibraryInRootConfig_"));
        //    }

        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex.InnerException, typeof(AssemblyNotLoadedException));

        //        throw;
        //    }

        //    finally
        //    {
        //        BusinessFrameworkManager.CloseFramework();
        //    }
        //}



        //[TestMethod]
        //[ExpectedException(typeof(InvalidConfigFileException))]
        //public void InitializeFramework_UnknownPlugInLibraryInRootConfig_ShouldThrowXmlSchemaValidationException()
        //{
        //    try
        //    {
        //        BusinessFrameworkManager.InitializeFramework(SetCurrentConfig("Root", "UnknownPlugInLibraryInRootConfig_"));
        //    }

        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex.InnerException, typeof(XmlSchemaValidationException));

        //        throw;
        //    }

        //    finally
        //    {
        //        BusinessFrameworkManager.CloseFramework();
        //    }
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InvalidConfigFileException))]
        //public void InitializeFramework_UnknownReferenceLibraryInRootConfig_ShouldThrowXmlSchemaValidationException()
        //{
        //    try
        //    {
        //        BusinessFrameworkManager.InitializeFramework(SetCurrentConfig("Root", "UnknownReferenceLibraryInRootConfig_"));
        //    }

        //    catch (Exception ex)
        //    {
        //        Assert.IsInstanceOfType(ex.InnerException, typeof(XmlSchemaValidationException));

        //        throw;
        //    }

        //    finally
        //    {
        //        BusinessFrameworkManager.CloseFramework();
        //    }
        //}

        #endregion
    }
}
