using FluentValidation.TestHelper;
using KooliProjekt.Application.Features.doctors;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.Doctors
{
    public class SaveDoctorsCommandValidatorTests
    {
        private readonly SaveDoctorsCommandValidator _validator;

        public SaveDoctorsCommandValidatorTests()
        {
            _validator = new SaveDoctorsCommandValidator();
        }

        #region Title Validation Tests

        [Fact]
        public void Validator_WithEmptyTitle_ShouldFail()
        {
            // Arrange
            var command = new SaveDoctorsCommand
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
            var command = new SaveDoctorsCommand
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
            var command = new SaveDoctorsCommand
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
        [InlineData("Dr. Smith")]
        [InlineData("Cardiologist")]
        [InlineData("General Practitioner")]
        [InlineData("Dr. Michael Johnson MD")]
        public void Validator_WithValidTitle_ShouldPass(string title)
        {
            // Arrange
            var command = new SaveDoctorsCommand
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
            var command = new SaveDoctorsCommand
            {
                title = "Dr. Smith"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion
    }
}
