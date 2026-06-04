using FluentValidation.TestHelper;
using KooliProjekt.Application.Features.Services;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.Services
{
    public class SaveServicesCommandValidatorTests
    {
        private readonly SaveServicesCommandValidator _validator;

        public SaveServicesCommandValidatorTests()
        {
            _validator = new SaveServicesCommandValidator();
        }

        #region Code Validation Tests

        [Fact]
        public void Validator_WithEmptyCode_ShouldFail()
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Code = "",
                Description = "Service description",
                UnitPrice = 99.99m
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Code)
                .WithErrorMessage("Code is required");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("")]
        public void Validator_WithCodeShorterThanMinimum_ShouldFail(string code)
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Code = code,
                Description = "Service description",
                UnitPrice = 99.99m
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Code)
                .WithErrorMessage("Code must be at least 2 characters long");
        }

        [Fact]
        public void Validator_WithCodeLongerThanMaximum_ShouldFail()
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Code = new string('a', 21),
                Description = "Service description",
                UnitPrice = 99.99m
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Code)
                .WithErrorMessage("Code cannot exceed 20 characters");
        }

        [Theory]
        [InlineData("SVC001")]
        [InlineData("MED-LAB-001")]
        [InlineData("SVC")]
        [InlineData("AB")]
        public void Validator_WithValidCode_ShouldPass(string code)
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Code = code,
                Description = "Service description",
                UnitPrice = 99.99m
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Code);
        }

        #endregion

        #region Description Validation Tests

        [Fact]
        public void Validator_WithEmptyDescription_ShouldFail()
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Code = "SVC001",
                Description = "",
                UnitPrice = 99.99m
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                .WithErrorMessage("Description is required");
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("a")]
        public void Validator_WithDescriptionShorterThanMinimum_ShouldFail(string description)
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Code = "SVC001",
                Description = description,
                UnitPrice = 99.99m
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                .WithErrorMessage("Description must be at least 3 characters long");
        }

        [Fact]
        public void Validator_WithDescriptionLongerThanMaximum_ShouldFail()
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Code = "SVC001",
                Description = new string('a', 501),
                UnitPrice = 99.99m
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                .WithErrorMessage("Description cannot exceed 500 characters");
        }

        [Theory]
        [InlineData("Medical Consultation")]
        [InlineData("Lab Analysis Services")]
        [InlineData("X-Ray Imaging")]
        public void Validator_WithValidDescription_ShouldPass(string description)
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Code = "SVC001",
                Description = description,
                UnitPrice = 99.99m
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        #endregion

        #region UnitPrice Validation Tests

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100.50)]
        public void Validator_WithInvalidUnitPrice_ShouldFail(decimal unitPrice)
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Code = "SVC001",
                Description = "Service description",
                UnitPrice = unitPrice
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UnitPrice)
                .WithErrorMessage("Unit price must be greater than 0");
        }

        [Theory]
        [InlineData(0.01)]
        [InlineData(50)]
        [InlineData(99.99)]
        [InlineData(9999.99)]
        public void Validator_WithValidUnitPrice_ShouldPass(decimal unitPrice)
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Code = "SVC001",
                Description = "Service description",
                UnitPrice = unitPrice
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UnitPrice);
        }

        #endregion

        #region Complete Command Validation Tests

        [Fact]
        public void Validator_WithValidCompleteCommand_ShouldPass()
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Id = 0,
                Code = "SVC001",
                Description = "Medical Consultation",
                UnitPrice = 100.00m
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
            var command = new SaveServicesCommand
            {
                Code = "a",
                Description = "ab",
                UnitPrice = -50
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            Assert.True(result.Errors.Count >= 3, "Should have at least 3 validation errors");
            result.ShouldHaveValidationErrorFor(x => x.Code);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
        }

        #endregion
    }
}
