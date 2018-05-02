
using Juhta.Net.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Juhta.Net.Tests.Common
{
    [TestClass]
    public class ClassFileUriTests
    {
        #region Test Methods

        [TestMethod]
        public void NewInstance_ValidValue_FragmentStartsWithDot_ShouldReturn()
        {
            ClassFileUri classFileUri;

            classFileUri = new ClassFileUri("C:\\Temp\\MyLibrary.dll#.MyClass");

            Assert.AreEqual<string>("C:\\Temp\\MyLibrary.dll", classFileUri.LibraryFilePath);

            Assert.AreEqual<string>("C:\\Temp", classFileUri.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classFileUri.LibraryFileName);

            Assert.AreEqual<string>("MyLibrary.MyClass", classFileUri.FullClassName);

            Assert.AreEqual<string>("MyLibrary", classFileUri.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classFileUri.ClassName);
        }

        [TestMethod]
        public void NewInstance_ValidValue_NoDirectory_FragmentStartsWithDot_ShouldReturn()
        {
            ClassFileUri classFileUri;

            classFileUri = new ClassFileUri("MyLibrary.dll#.MyClass");

            Assert.AreEqual<string>(Environment.CurrentDirectory + "\\MyLibrary.dll", classFileUri.LibraryFilePath);

            Assert.AreEqual<string>(Environment.CurrentDirectory, classFileUri.LibraryDirectory);

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

            Assert.AreEqual<string>(Environment.CurrentDirectory + "\\MyLibrary.dll", classFileUri.LibraryFilePath);

            Assert.AreEqual<string>(Environment.CurrentDirectory, classFileUri.LibraryDirectory);

            Assert.AreEqual<string>("MyLibrary.dll", classFileUri.LibraryFileName);

            Assert.AreEqual<string>("MyClass", classFileUri.FullClassName);

            Assert.AreEqual<string>(null, classFileUri.ClassNamespace);

            Assert.AreEqual<string>("MyClass", classFileUri.ClassName);
        }

        #endregion
    }
}
