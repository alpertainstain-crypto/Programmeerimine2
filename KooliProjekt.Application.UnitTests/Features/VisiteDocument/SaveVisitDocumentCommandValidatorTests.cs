using FluentValidation.TestHelper;
using KooliProjekt.Application.Features.VisiteDocument;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.VisiteDocument
{
    public class SaveVisitDocumentCommandValidatorTests
    {
        private readonly SaveVisitDocumentCommandValidator _validator;

        public SaveVisitDocumentCommandValidatorTests()
        {
            _validator = new SaveVisitDocumentCommandValidator();
        }

        #region AppointmentId Validation Tests

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Validator_WithInvalidAppointmentId_ShouldFail(int appointmentId)
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                AppointmentId = appointmentId,
                FilePath = "/documents/visit_001.pdf",
                FileType = "pdf",
                UploadedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.AppointmentId)
                .WithErrorMessage("Appointment ID must be greater than 0");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999999)]
        public void Validator_WithValidAppointmentId_ShouldPass(int appointmentId)
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                AppointmentId = appointmentId,
                FilePath = "/documents/visit_001.pdf",
                FileType = "pdf",
                UploadedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.AppointmentId);
        }

        #endregion

        #region FilePath Validation Tests

        [Fact]
        public void Validator_WithEmptyFilePath_ShouldFail()
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                AppointmentId = 1,
                FilePath = "",
                FileType = "pdf",
                UploadedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FilePath)
                .WithErrorMessage("File path is required");
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("a")]
        public void Validator_WithFilePathShorterThanMinimum_ShouldFail(string filePath)
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                AppointmentId = 1,
                FilePath = filePath,
                FileType = "pdf",
                UploadedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FilePath)
                .WithErrorMessage("File path must be at least 3 characters long");
        }

        [Fact]
        public void Validator_WithFilePathLongerThanMaximum_ShouldFail()
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                AppointmentId = 1,
                FilePath = new string('a', 261),
                FileType = "pdf",
                UploadedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FilePath)
                .WithErrorMessage("File path cannot exceed 260 characters");
        }

        [Theory]
        [InlineData("/documents/visit_001.pdf")]
        [InlineData("C:\\docs\\patient_file.doc")]
        [InlineData("/data/uploads/scan.png")]
        public void Validator_WithValidFilePath_ShouldPass(string filePath)
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                AppointmentId = 1,
                FilePath = filePath,
                FileType = "pdf",
                UploadedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.FilePath);
        }

        #endregion

        #region FileType Validation Tests

        [Fact]
        public void Validator_WithEmptyFileType_ShouldFail()
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                AppointmentId = 1,
                FilePath = "/documents/visit_001.pdf",
                FileType = "",
                UploadedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FileType)
                .WithErrorMessage("File type is required");
        }

        [Theory]
        [InlineData("exe")]
        [InlineData("zip")]
        [InlineData("bat")]
        [InlineData("html")]
        public void Validator_WithInvalidFileType_ShouldFail(string fileType)
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                AppointmentId = 1,
                FilePath = "/documents/visit_001.pdf",
                FileType = fileType,
                UploadedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FileType);
        }

        [Theory]
        [InlineData("pdf")]
        [InlineData("PDF")]
        [InlineData("doc")]
        [InlineData("docx")]
        [InlineData("jpg")]
        [InlineData("png")]
        [InlineData("txt")]
        public void Validator_WithValidFileType_ShouldPass(string fileType)
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                AppointmentId = 1,
                FilePath = "/documents/visit_001.pdf",
                FileType = fileType,
                UploadedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.FileType);
        }

        #endregion

        #region UploadedBy Validation Tests

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Validator_WithInvalidUploadedBy_ShouldFail(int uploadedBy)
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                AppointmentId = 1,
                FilePath = "/documents/visit_001.pdf",
                FileType = "pdf",
                UploadedBy = uploadedBy
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UploadedBy)
                .WithErrorMessage("Uploaded by (User ID) must be greater than 0");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999999)]
        public void Validator_WithValidUploadedBy_ShouldPass(int uploadedBy)
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                AppointmentId = 1,
                FilePath = "/documents/visit_001.pdf",
                FileType = "pdf",
                UploadedBy = uploadedBy
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UploadedBy);
        }

        #endregion

        #region Complete Command Validation Tests

        [Fact]
        public void Validator_WithValidCompleteCommand_ShouldPass()
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                Id = 0,
                AppointmentId = 1,
                FilePath = "/documents/visit_001.pdf",
                FileType = "pdf",
                UploadedBy = 1,
                CreatedAt = DateTime.Now
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validator_WithMultipleValidationErrors_ShouldFailForAllErrors()
        {
            // Arrange
            var command = new SaveVisitDocumentCommand
            {
                AppointmentId = -1,
                FilePath = "ab",
                FileType = "exe",
                UploadedBy = -1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            Assert.True(result.Errors.Count >= 4, "Should have at least 4 validation errors");
            result.ShouldHaveValidationErrorFor(x => x.AppointmentId);
            result.ShouldHaveValidationErrorFor(x => x.FilePath);
            result.ShouldHaveValidationErrorFor(x => x.FileType);
            result.ShouldHaveValidationErrorFor(x => x.UploadedBy);
        }

        #endregion
    }
}
