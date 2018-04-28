
using Juhta.Net.Common;
using Juhta.Net.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Juhta.Net.Tests.Validators
{
    [TestClass]
    public class FilePathValidatorTests
    {
        #region Test Methods

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_DirectoryNameEndsWithDot_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(OSConvert(@"C:\My Documents.\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_DirectoryNameEndsWithSpace_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(OSConvert(@"C:\My Documents \Shopping\MyDoc.docx"));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_FileNameEndsWithDot_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(OSConvert(@"C:\My Documents\Shopping\MyDoc.docx."));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_FileNameEndsWithSpace_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(OSConvert(@"C:\My Documents\Shopping\MyDoc.docx "));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_IllegalCharacterInDirectoryName_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(OSConvert(@"C:\My DocumenXts\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath_IllegalCharacterInFileName_ShouldThrowValidationException()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(OSConvert(@"C:\My Documents\Shopping\MyDXoc.docx"));
        }

        [TestMethod]
        public void Validate_ValidFilePath1_ShouldReturn()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(OSConvert(@"C:\My Documents\Shopping"));
        }

        [TestMethod]
        public void Validate_ValidFilePath2_ShouldReturn()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(OSConvert(@"C:\My Documents\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        public void Validate_ValidFilePath3_ShouldReturn()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(OSConvert(@"C:\My Documents\.\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        public void Validate_ValidFilePath4_ShouldReturn()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(OSConvert(@"C:\My Documents\..\Shopping\MyDoc.docx"));
        }

        [TestMethod]
        public void Validate_ValidFilePath5_ShouldReturn()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(OSConvert(@"MyDoc.docx"));
        }

        [TestMethod]
        public void Validate_ValidFilePath6_ShouldReturn()
        {
            FilePathValidator validator = new FilePathValidator();

            validator.Validate(OSConvert(@"MyDoc"));
        }

        #endregion

        #region Private Methods

        private static string OSConvert(string path)
        {
            if (OSInfo.IsWindows)
                path = path.Replace('X', '<');
            else
            {
                path = path.Replace("C:", "");

                path = path.Replace('/', Path.DirectorySeparatorChar);

                path = path.Replace('X', '\0');
            }

            return(path);
        }

        #endregion
    }
}
