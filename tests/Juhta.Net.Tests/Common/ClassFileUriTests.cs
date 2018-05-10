
using Juhta.Net.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Juhta.Net.Tests.Common
{
    [TestClass]
    public class ClassFileUriTests : TestClassBase
    {
        #region Test Methods

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_EmptyFragmentPart_ShouldThrowArgumentException()
        {
            ClassFileUri classFileUri;

            try
            {
                classFileUri = new ClassFileUri("file:///" + ToOSPath("C:\\Temp\\MyLibrary.dll#"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error10034]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_InvalidFragmentPart_ShouldThrowArgumentException()
        {
            ClassFileUri classFileUri;

            try
            {
                classFileUri = new ClassFileUri("file:///" + ToOSPath("C:\\Temp\\MyLibrary.dll#Class-Name"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error10036]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_InvalidLibraryDirectory_ShouldThrowArgumentException()
        {
            ClassFileUri classFileUri;

            try
            {
                classFileUri = new ClassFileUri("file:///" + ToOSPath("C:\\TempX\\MyLibrary.dll#Class"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error10039]"));

                Assert.IsTrue(ex.InnerException.Message.StartsWith("[Juhta.Net.Error10038]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_InvalidLibraryFileName_ShouldThrowArgumentException()
        {
            ClassFileUri classFileUri;

            try
            {
                classFileUri = new ClassFileUri("file:///" + ToOSPath("C:\\Temp\\MyLibXrary.dll#Class"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error10039]"));

                Assert.IsTrue(ex.InnerException.Message.StartsWith("[Juhta.Net.Error10038]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_InvalidLibraryFileType_ShouldThrowArgumentException()
        {
            ClassFileUri classFileUri;

            try
            {
                classFileUri = new ClassFileUri("file:///" + ToOSPath("C:\\Temp\\MyLibrary.bin#Class"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error10035]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_LocalhostFileUri_ShouldThrowArgumentException()
        {
            ClassFileUri classFileUri;

            try
            {
                classFileUri = new ClassFileUri("file://" + ToOSPath("C:\\Temp\\MyLibrary.dll#MyFirstClasses.Shared.MyClass"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error10048]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_NoFragmentPart_ShouldThrowArgumentException()
        {
            ClassFileUri classFileUri;

            try
            {
                classFileUri = new ClassFileUri("file:///" + ToOSPath("C:\\Temp\\MyLibrary.dll"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error10034]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_NonFileScheme_ShouldThrowArgumentException()
        {
            ClassFileUri classFileUri;

            try
            {
                classFileUri = new ClassFileUri("http://" + ToOSPath("C:\\Temp\\MyLibrary.dll#MyFirstClasses.Shared.MyClass"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error10033]"));

                throw;
            }
        }

        [TestMethod]
        public void NewInstance_ValidValue_Directory_ClassNamespace_ShouldReturn()
        {
            ClassFileUri classFileUri;

            classFileUri = new ClassFileUri(ToOSPath("C:\\Temp\\MyLibrary.dll#MyFirstClasses.Shared.MyClass"));

            Assert.AreEqual<string>(ToOSPath("C:\\Temp\\MyLibrary.dll"), classFileUri.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath("C:\\Temp"), classFileUri.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classFileUri.LibraryFileName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.MyClass", classFileUri.FullClassName);

            Assert.AreEqual<string>("MyFirstClasses.Shared", classFileUri.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classFileUri.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_Directory_DefaultClassNamespace_ShouldReturn()
        {
            ClassFileUri classFileUri;

            classFileUri = new ClassFileUri(ToOSPath("C:\\Temp\\MyLibrary.dll#.MyClass"));

            Assert.AreEqual<string>(ToOSPath("C:\\Temp\\MyLibrary.dll"), classFileUri.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath("C:\\Temp"), classFileUri.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classFileUri.LibraryFileName);

            Assert.AreEqual<string>("MyLibrary.MyClass", classFileUri.FullClassName);

            Assert.AreEqual<string>("MyLibrary", classFileUri.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classFileUri.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_Directory_NoClassNamespace_ShouldReturn()
        {
            ClassFileUri classFileUri;

            classFileUri = new ClassFileUri(ToOSPath("C:\\Temp\\MyLibrary.dll#MyFirstClass"));

            Assert.AreEqual<string>(ToOSPath("C:\\Temp\\MyLibrary.dll"), classFileUri.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath("C:\\Temp"), classFileUri.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classFileUri.LibraryFileName);

            Assert.AreEqual<string>("MyFirstClass", classFileUri.FullClassName);

            Assert.AreEqual<string>(null, classFileUri.ClassNamespace);

            Assert.AreEqual<string>("MyFirstClass", classFileUri.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_FileScheme_Directory_ClassNamespace_ShouldReturn()
        {
            ClassFileUri classFileUri;

            classFileUri = new ClassFileUri(ToOSPath("file:///C:\\MyApp\\MyLibrary.dll#MyFirstClasses.Shared.Web.MyClass"));

            Assert.AreEqual<string>(ToOSPath("C:\\MyApp\\MyLibrary.dll"), classFileUri.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath("C:\\MyApp"), classFileUri.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classFileUri.LibraryFileName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web.MyClass", classFileUri.FullClassName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web", classFileUri.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classFileUri.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_FileScheme_NoDirectory_ClassNamespace_ShouldReturn()
        {
            ClassFileUri classFileUri;

            classFileUri = new ClassFileUri(ToOSPath("file:///MyLibrary.dll#MyFirstClasses.Shared.Web.MyClass"));

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory + "\\MyLibrary.dll"), classFileUri.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory), classFileUri.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classFileUri.LibraryFileName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web.MyClass", classFileUri.FullClassName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web", classFileUri.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classFileUri.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_FileScheme_NoDirectory_NoClassNamespace_ShouldReturn()
        {
            ClassFileUri classFileUri;

            classFileUri = new ClassFileUri(ToOSPath("file:///MyLibrary.dll#MyFirstClass"));

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory + "\\MyLibrary.dll"), classFileUri.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory), classFileUri.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classFileUri.LibraryFileName);

            Assert.AreEqual<string>("MyFirstClass", classFileUri.FullClassName);

            Assert.AreEqual<string>(null, classFileUri.ClassNamespace);

            Assert.AreEqual<string>("MyFirstClass", classFileUri.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_NoDirectory_ClassNamespace_ShouldReturn()
        {
            ClassFileUri classFileUri;

            classFileUri = new ClassFileUri(ToOSPath("MyLibrary.dll#MyFirstClasses.Shared.Web.MyClass"));

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory + "\\MyLibrary.dll"), classFileUri.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory), classFileUri.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classFileUri.LibraryFileName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web.MyClass", classFileUri.FullClassName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web", classFileUri.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classFileUri.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_NoDirectory_DefaultClassNamespace_ShouldReturn()
        {
            ClassFileUri classFileUri;

            classFileUri = new ClassFileUri("MyLibrary.dll#.MyClass");

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory + "\\MyLibrary.dll"), classFileUri.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory), classFileUri.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classFileUri.LibraryFileName);

            Assert.AreEqual<string>("MyLibrary.MyClass", classFileUri.FullClassName);

            Assert.AreEqual<string>("MyLibrary", classFileUri.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classFileUri.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_NoDirectory_NoClassNamespace_ShouldReturn()
        {
            ClassFileUri classFileUri;

            classFileUri = new ClassFileUri("MyLibrary.dll#MyClass");

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory + "\\MyLibrary.dll"), classFileUri.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory), classFileUri.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classFileUri.LibraryFileName);

            Assert.AreEqual<string>("MyClass", classFileUri.FullClassName);

            Assert.AreEqual<string>(null, classFileUri.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classFileUri.ClassName);
        }

        #endregion
    }
}
