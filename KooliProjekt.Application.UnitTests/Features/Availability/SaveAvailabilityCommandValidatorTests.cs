using FluentValidation.TestHelper;
using KooliProjekt.Application.Features.Availability;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.Availability
{
    public class SaveAvailabilityCommandValidatorTests
    {
        private readonly SaveAvailabilityCommandValidator _validator;

        public SaveAvailabilityCommandValidatorTests()
        {
            _validator = new SaveAvailabilityCommandValidator();
        }

        #region Title Validation Tests

        [Fact]
        public void Validator_WithEmptyTitle_ShouldFail()
        {
            // Arrange
            var command = new SaveAvailabilityCommand
            {
                title = ""
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.title)
                .WithErrorMessage("Title is required");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("")]
        public void Validator_WithTitleShorterThanMinimum_ShouldFail(string title)
        {
            // Arrange
            var command = new SaveAvailabilityCommand
            {
                title = title
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.title);
        }

        [Fact]
        public void Validator_WithTitleLongerThanMaximum_ShouldFail()
        {
            // Arrange
            var command = new SaveAvailabilityCommand
            {
                title = new string('a', 101)
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.title)
                .WithErrorMessage("Title cannot exceed 100 characters");
        }

        [Theory]
        [InlineData("Monday Morning")]
        [InlineData("Available 9-5")]
        [InlineData("Office Hours")]
        [InlineData("Dr. Smith - Available")]
        public void Validator_WithValidTitle_ShouldPass(string title)
        {
            // Arrange
            var command = new SaveAvailabilityCommand
            {
                title = title
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.title);
        }

        #endregion

        #region Complete Command Validation Tests

        [Fact]
        public void Validator_WithValidCompleteCommand_ShouldPass()
        {
            // Arrange
            var command = new SaveAvailabilityCommand
            {
                title = "Monday Morning"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion
    }
}
