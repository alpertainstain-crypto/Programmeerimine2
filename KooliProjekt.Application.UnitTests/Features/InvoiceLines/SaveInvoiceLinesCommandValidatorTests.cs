using FluentValidation.TestHelper;
using KooliProjekt.Application.Features.InvoiceLines;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.InvoiceLines
{
    public class SaveInvoiceLinesCommandValidatorTests
    {
        private readonly SaveInvoiceLinesCommandValidator _validator;

        public SaveInvoiceLinesCommandValidatorTests()
        {
            _validator = new SaveInvoiceLinesCommandValidator();
        }

        #region InvoiceId Validation Tests

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Validator_WithInvalidInvoiceId_ShouldFail(int invoiceId)
        {
            // Arrange
            var command = new SaveInvoiceLinesCommand
            {
                InvoiceId = invoiceId,
                Description = "Service description",
                Amount = 150.00m
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.InvoiceId)
                .WithErrorMessage("Invoice ID must be greater than 0");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999999)]
        public void Validator_WithValidInvoiceId_ShouldPass(int invoiceId)
        {
            // Arrange
            var command = new SaveInvoiceLinesCommand
            {
                InvoiceId = invoiceId,
                Description = "Service description",
                Amount = 150.00m
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.InvoiceId);
        }

        #endregion

        #region Description Validation Tests

        [Fact]
        public void Validator_WithEmptyDescription_ShouldFail()
        {
            // Arrange
            var command = new SaveInvoiceLinesCommand
            {
                InvoiceId = 1,
                Description = "",
                Amount = 150.00m
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
            var command = new SaveInvoiceLinesCommand
            {
                InvoiceId = 1,
                Description = description,
                Amount = 150.00m
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
            var command = new SaveInvoiceLinesCommand
            {
                InvoiceId = 1,
                Description = new string('a', 501),
                Amount = 150.00m
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
        [InlineData("Prescription refill")]
        public void Validator_WithValidDescription_ShouldPass(string description)
        {
            // Arrange
            var command = new SaveInvoiceLinesCommand
            {
                InvoiceId = 1,
                Description = description,
                Amount = 150.00m
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        #endregion

        #region Amount Validation Tests

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100.50)]
        public void Validator_WithInvalidAmount_ShouldFail(decimal amount)
        {
            // Arrange
            var command = new SaveInvoiceLinesCommand
            {
                InvoiceId = 1,
                Description = "Service description",
                Amount = amount
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Amount)
                .WithErrorMessage("Amount must be greater than 0");
        }

        [Theory]
        [InlineData(0.01)]
        [InlineData(50)]
        [InlineData(150.00)]
        [InlineData(9999.99)]
        public void Validator_WithValidAmount_ShouldPass(decimal amount)
        {
            // Arrange
            var command = new SaveInvoiceLinesCommand
            {
                InvoiceId = 1,
                Description = "Service description",
                Amount = amount
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Amount);
        }

        #endregion

        #region Complete Command Validation Tests

        [Fact]
        public void Validator_WithValidCompleteCommand_ShouldPass()
        {
            // Arrange
            var command = new SaveInvoiceLinesCommand
            {
                Id = 0,
                InvoiceId = 1,
                Description = "Medical Consultation",
                Amount = 150.00m
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
            var command = new SaveInvoiceLinesCommand
            {
                InvoiceId = -1,
                Description = "ab",
                Amount = -50
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            Assert.True(result.Errors.Count >= 3, "Should have at least 3 validation errors");
            result.ShouldHaveValidationErrorFor(x => x.InvoiceId);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.Amount);
        }

        #endregion
    }
}
