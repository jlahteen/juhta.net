
using Juhta.Net.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Juhta.Net.Tests.Common
{
    [TestClass]
    public class ClassIdTests : TestClassBase
    {
        #region Test Methods

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_EmptyFragmentPart_ShouldThrowArgumentException()
        {
            ClassId classId;

            try
            {
                classId = new ClassId("file:///" + ToOSPath("C:\\Temp\\MyLibrary.dll#"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error101034]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_InvalidFragmentPart_ShouldThrowArgumentException()
        {
            ClassId classId;

            try
            {
                classId = new ClassId("file:///" + ToOSPath("C:\\Temp\\MyLibrary.dll#Class-Name"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error101036]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_InvalidLibraryDirectory_ShouldThrowArgumentException()
        {
            ClassId classId;

            try
            {
                classId = new ClassId("file:///" + ToOSPath("C:\\TempX\\MyLibrary.dll#Class"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error101038]"));

                Assert.IsTrue(ex.InnerException.Message.StartsWith("Illegal characters in path."));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_InvalidLibraryFileName_ShouldThrowArgumentException()
        {
            ClassId classId;

            try
            {
                classId = new ClassId("file:///" + ToOSPath("C:\\Temp\\MyLibXrary.dll#Class"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error101038]"));

                Assert.IsTrue(ex.InnerException.Message.StartsWith("Illegal characters in path."));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_InvalidLibraryFileType_ShouldThrowArgumentException()
        {
            ClassId classId;

            try
            {
                classId = new ClassId("file:///" + ToOSPath("C:\\Temp\\MyLibrary.bin#Class"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error101035]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_NoFragmentPart_ShouldThrowArgumentException()
        {
            ClassId classId;

            try
            {
                classId = new ClassId("file:///" + ToOSPath("C:\\Temp\\MyLibrary.dll"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error101034]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_NonFileScheme_ShouldThrowArgumentException()
        {
            ClassId classId;

            try
            {
                classId = new ClassId("http://" + ToOSPath("C:\\Temp\\MyLibrary.dll#MyFirstClasses.Shared.MyClass"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error101048]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_NonLocalhostFileUri_ShouldThrowArgumentException()
        {
            ClassId classId;

            try
            {
                classId = new ClassId("file://Something/Temp/MyLibrary.dll#MyFirstClasses.Shared.MyClass");
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error101048]"));

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewInstance_InvalidValue_SeparateLibraryDirectory_Directory_ShouldThrowArgumentException()
        {
            ClassId classId;

            try
            {
                classId = new ClassId(ToOSPath("Temp\\MyLibrary.dll#MyFirstClasses.Shared.MyClass"), ToOSPath("C:\\"));
            }

            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("[Juhta.Net.Error101024]"));

                throw;
            }
        }

        [TestMethod]
        public void NewInstance_ValidValue_Directory_ClassNamespace_ShouldReturn()
        {
            ClassId classId;

            classId = new ClassId(ToOSPath("C:\\Temp\\MyLibrary.dll#MyFirstClasses.Shared.MyClass"));

            Assert.AreEqual<string>(ToOSPath("C:\\Temp\\MyLibrary.dll"), classId.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath("C:\\Temp"), classId.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classId.LibraryFileName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.MyClass", classId.FullClassName);

            Assert.AreEqual<string>("MyFirstClasses.Shared", classId.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classId.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_Directory_DefaultClassNamespace_ShouldReturn()
        {
            ClassId classId;

            classId = new ClassId(ToOSPath("C:\\Temp\\MyLibrary.dll#~.MyClass"));

            Assert.AreEqual<string>(ToOSPath("C:\\Temp\\MyLibrary.dll"), classId.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath("C:\\Temp"), classId.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classId.LibraryFileName);

            Assert.AreEqual<string>("MyLibrary.MyClass", classId.FullClassName);

            Assert.AreEqual<string>("MyLibrary", classId.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classId.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_Directory_NoClassNamespace_ShouldReturn()
        {
            ClassId classId;

            classId = new ClassId(ToOSPath("C:\\Temp\\MyLibrary.dll#MyFirstClass"));

            Assert.AreEqual<string>(ToOSPath("C:\\Temp\\MyLibrary.dll"), classId.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath("C:\\Temp"), classId.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classId.LibraryFileName);

            Assert.AreEqual<string>("MyFirstClass", classId.FullClassName);

            Assert.AreEqual<string>(null, classId.ClassNamespace);

            Assert.AreEqual<string>("MyFirstClass", classId.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_FileScheme_Directory_ClassNamespace_ShouldReturn()
        {
            ClassId classId;

            classId = new ClassId(ToOSPath("file:///C:\\MyApp\\MyLibrary.dll#MyFirstClasses.Shared.Web.MyClass"));

            Assert.AreEqual<string>(ToOSPath("C:\\MyApp\\MyLibrary.dll"), classId.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath("C:\\MyApp"), classId.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classId.LibraryFileName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web.MyClass", classId.FullClassName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web", classId.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classId.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_FileScheme_NoDirectory_ClassNamespace_ShouldReturn()
        {
            ClassId classId;

            classId = new ClassId(ToOSPath("file:///MyLibrary.dll#MyFirstClasses.Shared.Web.MyClass"));

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory + "\\MyLibrary.dll"), classId.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory), classId.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classId.LibraryFileName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web.MyClass", classId.FullClassName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web", classId.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classId.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_FileScheme_NoDirectory_NoClassNamespace_ShouldReturn()
        {
            ClassId classId;

            classId = new ClassId(ToOSPath("file:///MyLibrary.dll#MyFirstClass"));

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory + "\\MyLibrary.dll"), classId.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory), classId.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classId.LibraryFileName);

            Assert.AreEqual<string>("MyFirstClass", classId.FullClassName);

            Assert.AreEqual<string>(null, classId.ClassNamespace);

            Assert.AreEqual<string>("MyFirstClass", classId.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_NoDirectory_ClassNamespace_ShouldReturn()
        {
            ClassId classId;

            classId = new ClassId(ToOSPath("MyLibrary.dll#MyFirstClasses.Shared.Web.MyClass"));

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory + "\\MyLibrary.dll"), classId.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory), classId.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classId.LibraryFileName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web.MyClass", classId.FullClassName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web", classId.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classId.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_NoDirectory_DefaultClassNamespace_ShouldReturn()
        {
            ClassId classId;

            classId = new ClassId("MyLibrary.dll#~.MyClass");

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory + "\\MyLibrary.dll"), classId.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory), classId.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classId.LibraryFileName);

            Assert.AreEqual<string>("MyLibrary.MyClass", classId.FullClassName);

            Assert.AreEqual<string>("MyLibrary", classId.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classId.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_NoDirectory_NoClassNamespace_ShouldReturn()
        {
            ClassId classId;

            classId = new ClassId("MyLibrary.dll#MyClass");

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory + "\\MyLibrary.dll"), classId.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath(Environment.CurrentDirectory), classId.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classId.LibraryFileName);

            Assert.AreEqual<string>("MyClass", classId.FullClassName);

            Assert.AreEqual<string>(null, classId.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classId.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_SeparateLibraryDirectory_NoDirectory_ClassNamespace_ShouldReturn()
        {
            ClassId classId;

            classId = new ClassId(ToOSPath("MyLibrary.dll#MyFirstClasses.Shared.Web.MyClass"), ToOSPath("C:\\WebApps\\Shared\\Bin"));

            Assert.AreEqual<string>(ToOSPath("C:\\WebApps\\Shared\\Bin\\MyLibrary.dll"), classId.LibraryFilePath);

            Assert.AreEqual<string>(ToOSPath("C:\\WebApps\\Shared\\Bin"), classId.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classId.LibraryFileName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web.MyClass", classId.FullClassName);

            Assert.AreEqual<string>("MyFirstClasses.Shared.Web", classId.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classId.ClassName);
        }

        #endregion
    }
}
