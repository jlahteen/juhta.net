
using Juhta.Net.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhta.Net.Tests.Validators
{
    [TestClass]
    public class FilePathValidatorTests
    {
        #region Test Methods

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath1_ShouldThrowValidationException()
        {
            FilePathValidator filePathValidator = new FilePathValidator();

            filePathValidator.Validate(@"C:\My Documents\Shopping\MyD<oc.docx");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath2_ShouldThrowValidationException()
        {
            FilePathValidator filePathValidator = new FilePathValidator();

            filePathValidator.Validate(@"C:\My Documents\Shopping\MyDoc.docx.");
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Validate_InvalidFilePath3_ShouldThrowValidationException()
        {
            FilePathValidator filePathValidator = new FilePathValidator();

            filePathValidator.Validate(@"C:\My Documents \Shopping\MyDoc.docx.");
        }

        [TestMethod]
        public void Validate_ValidFilePath1_ShouldReturn()
        {
            FilePathValidator filePathValidator = new FilePathValidator();

            filePathValidator.Validate(@"C:\My Documents\Shopping");
        }

        [TestMethod]
        public void Validate_ValidFilePath2_ShouldReturn()
        {
            FilePathValidator filePathValidator = new FilePathValidator();

            filePathValidator.Validate(@"C:\My Documents\Shopping\MyDoc.docx");
        }

        [TestMethod]
        public void Validate_ValidFilePath3_ShouldReturn()
        {
            FilePathValidator filePathValidator = new FilePathValidator();

            filePathValidator.Validate(@"C:\My Documents\.\Shopping\MyDoc.docx");
        }

        [TestMethod]
        public void Validate_ValidFilePath4_ShouldReturn()
        {
            FilePathValidator filePathValidator = new FilePathValidator();

            filePathValidator.Validate(@"C:\My Documents\..\Shopping\MyDoc.docx");
        }

        #endregion
    }
}
