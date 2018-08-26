
using Juhta.Net.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Juhta.Net.Validation.Tests
{
    [TestClass]
    public class FilePathValidatorTests : TestClassBase
    {
        #region Test Methods

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_DirectoryNameEndsWithDot_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents.\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_DirectoryNameEndsWithSpace_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents \Shopping\MyDoc.docx"));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_EmptyDirectoryName_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents\\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_EmptyFileName_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents\Shopping\MyDoc.docx\"));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_EmptyFilePath_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate("");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_FileNameEndsWithDot_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents\Shopping\MyDoc.docx."));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_FileNameEndsWithSpace_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents\Shopping\MyDoc.docx "));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_IllegalCharacterInDirectoryName_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"C:\My DocumenXts\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_IllegalCharacterInFileName_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents\Shopping\MyDXoc.docx"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Validate_InvalidFilePath_NullFilePath_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(null);
        }

        [TestMethod]
        public void Validate_ValidFilePath1_ShouldReturn()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents\Shopping"));
        }

        [TestMethod]
        public void Validate_ValidFilePath2_ShouldReturn()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        public void Validate_ValidFilePath3_ShouldReturn()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents\.\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        public void Validate_ValidFilePath4_ShouldReturn()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents\..\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        public void Validate_ValidFilePath5_ShouldReturn()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"MyDoc.docx"));
        }

        [TestMethod]
        public void Validate_ValidFilePath6_ShouldReturn()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(ToOSPath(@"MyDoc"));
        }

        #endregion
    }
}
