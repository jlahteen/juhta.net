
using Juhta.Net.Common;
using Juhta.Net.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Juhta.Net.Tests.Validators
{
    [TestClass]
    public class DirectoryPathValidatorTests : TestClassBase
    {
        #region Test Methods

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidDirectoryPath_DirectoryNameEndsWithDot_ShouldThrowValidationException()
        {
            DirectoryPathValidator validator = new DirectoryPathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents\Shopping."));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidDirectoryPath_DirectoryNameEndsWithSpace_ShouldThrowValidationException()
        {
            DirectoryPathValidator validator = new DirectoryPathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents\Shopping\MyDoc.docx "));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidDirectoryPath_EmptyDirectoryName1_ShouldThrowValidationException()
        {
            DirectoryPathValidator validator = new DirectoryPathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents\Shopping\\MyDoc.docx"));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidDirectoryPath_EmptyDirectoryName2_ShouldThrowValidationException()
        {
            DirectoryPathValidator validator = new DirectoryPathValidator();

            validator.Validate(ToOSPath(@"C:\My Documents\Shopping\MyDoc.docx\"));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidDirectoryPath_IllegalCharacterInDirectoryName_ShouldThrowValidationException()
        {
            DirectoryPathValidator filePathValidator = new DirectoryPathValidator();

            filePathValidator.Validate(ToOSPath(@"C:\My Documents\Shopping\MyDocX"));
        }

        [TestMethod]
        public void Validate_ValidDirectoryPath1_ShouldReturn()
        {
            DirectoryPathValidator filePathValidator = new DirectoryPathValidator();

            filePathValidator.Validate(ToOSPath(@"C:\My Documents\Shopping"));
        }

        [TestMethod]
        public void Validate_ValidDirectoryPath2_ShouldReturn()
        {
            DirectoryPathValidator filePathValidator = new DirectoryPathValidator();

            filePathValidator.Validate(ToOSPath(@"C:\My Documents\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        public void Validate_ValidDirectoryPath3_ShouldReturn()
        {
            DirectoryPathValidator filePathValidator = new DirectoryPathValidator();

            filePathValidator.Validate(ToOSPath(@"C:\My Documents\.\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        public void Validate_ValidDirectoryPath4_ShouldReturn()
        {
            DirectoryPathValidator filePathValidator = new DirectoryPathValidator();

            filePathValidator.Validate(ToOSPath(@"C:\My Documents\..\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        public void Validate_ValidDirectoryPath5_ShouldReturn()
        {
            DirectoryPathValidator validator = new DirectoryPathValidator();

            validator.Validate(ToOSPath(@"MyDoc.docx"));
        }

        [TestMethod]
        public void Validate_ValidDirectoryPath6_ShouldReturn()
        {
            DirectoryPathValidator validator = new DirectoryPathValidator();

            validator.Validate(ToOSPath(@"MyDoc"));
        }

        #endregion
    }
}
