using FluentValidation.TestHelper;
using KooliProjekt.Application.Features.AdminOverrideList;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.AdminOverride
{
    public class SaveAdminOverrideCommandValidatorTests
    {
        private readonly SaveAdminOverrideCommandValidator _validator;

        public SaveAdminOverrideCommandValidatorTests()
        {
            _validator = new SaveAdminOverrideCommandValidator();
        }

        #region Title Validation Tests

        [Fact]
        public void Validator_WithEmptyTitle_ShouldFail()
        {
            // Arrange
            var command = new SaveAdminOverrideCommand
            {
                Title = "",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(1),
                DoctorId = 1,
                CreatedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title)
                .WithErrorMessage("Title is required");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("")]
        public void Validator_WithTitleShorterThanMinimum_ShouldFail(string title)
        {
            // Arrange
            var command = new SaveAdminOverrideCommand
            {
                Title = title,
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(1),
                DoctorId = 1,
                CreatedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Validator_WithTitleLongerThanMaximum_ShouldFail()
        {
            // Arrange
            var command = new SaveAdminOverrideCommand
            {
                Title = new string('a', 201),
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(1),
                DoctorId = 1,
                CreatedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title)
                .WithErrorMessage("Title cannot exceed 200 characters");
        }

        [Theory]
        [InlineData("Emergency Leave")]
        [InlineData("Vacation Override")]
        [InlineData("System Maintenance Window")]
        public void Validator_WithValidTitle_ShouldPass(string title)
        {
            // Arrange
            var command = new SaveAdminOverrideCommand
            {
                Title = title,
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(1),
                DoctorId = 1,
                CreatedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
        }

        #endregion

        #region Start Date Validation Tests

        [Fact]
        public void Validator_WithNullStartDate_ShouldFail()
        {
            // Arrange
            var command = new SaveAdminOverrideCommand
            {
                Title = "Emergency Leave",
                Start = null,
                End = DateTime.Now.AddDays(1),
                DoctorId = 1,
                CreatedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Start)
                .WithErrorMessage("Start date is required");
        }

        [Fact]
        public void Validator_WithValidStartDate_ShouldPass()
        {
            // Arrange
            var now = DateTime.Now;
            var command = new SaveAdminOverrideCommand
            {
                Title = "Emergency Leave",
                Start = now,
                End = now.AddDays(1),
                DoctorId = 1,
                CreatedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Start);
        }

        #endregion

        #region End Date Validation Tests

        [Fact]
        public void Validator_WithNullEndDate_ShouldFail()
        {
            // Arrange
            var command = new SaveAdminOverrideCommand
            {
                Title = "Emergency Leave",
                Start = DateTime.Now,
                End = null,
                DoctorId = 1,
                CreatedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.End)
                .WithErrorMessage("End date is required");
        }

        [Fact]
        public void Validator_WithEndDateBeforeStartDate_ShouldFail()
        {
            // Arrange
            var today = DateTime.Now;
            var command = new SaveAdminOverrideCommand
            {
                Title = "Emergency Leave",
                Start = today,
                End = today.AddDays(-1),
                DoctorId = 1,
                CreatedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.End)
                .WithErrorMessage("End date must be after start date");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(30)]
        public void Validator_WithValidEndDate_ShouldPass(int daysAfter)
        {
            // Arrange
            var start = DateTime.Now;
            var command = new SaveAdminOverrideCommand
            {
                Title = "Emergency Leave",
                Start = start,
                End = start.AddDays(daysAfter),
                DoctorId = 1,
                CreatedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.End);
        }

        #endregion

        #region DoctorId Validation Tests

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Validator_WithInvalidDoctorId_ShouldFail(int doctorId)
        {
            // Arrange
            var today = DateTime.Now;
            var command = new SaveAdminOverrideCommand
            {
                Title = "Emergency Leave",
                Start = today,
                End = today.AddDays(1),
                DoctorId = doctorId,
                CreatedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DoctorId)
                .WithErrorMessage("Doctor ID must be greater than 0");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999999)]
        public void Validator_WithValidDoctorId_ShouldPass(int doctorId)
        {
            // Arrange
            var today = DateTime.Now;
            var command = new SaveAdminOverrideCommand
            {
                Title = "Emergency Leave",
                Start = today,
                End = today.AddDays(1),
                DoctorId = doctorId,
                CreatedBy = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.DoctorId);
        }

        #endregion

        #region CreatedBy Validation Tests

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Validator_WithInvalidCreatedBy_ShouldFail(int createdBy)
        {
            // Arrange
            var today = DateTime.Now;
            var command = new SaveAdminOverrideCommand
            {
                Title = "Emergency Leave",
                Start = today,
                End = today.AddDays(1),
                DoctorId = 1,
                CreatedBy = createdBy
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CreatedBy)
                .WithErrorMessage("Created by (User ID) must be greater than 0");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999999)]
        public void Validator_WithValidCreatedBy_ShouldPass(int createdBy)
        {
            // Arrange
            var today = DateTime.Now;
            var command = new SaveAdminOverrideCommand
            {
                Title = "Emergency Leave",
                Start = today,
                End = today.AddDays(1),
                DoctorId = 1,
                CreatedBy = createdBy
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.CreatedBy);
        }

        #endregion

        #region Complete Command Validation Tests

        [Fact]
        public void Validator_WithValidCompleteCommand_ShouldPass()
        {
            // Arrange
            var today = DateTime.Now;
            var command = new SaveAdminOverrideCommand
            {
                Id = 0,
                Title = "Emergency Leave",
                Start = today,
                End = today.AddDays(7),
                DoctorId = 1,
                CreatedBy = 1
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
            var today = DateTime.Now;
            var command = new SaveAdminOverrideCommand
            {
                Title = "a",
                Start = null,
                End = today.AddDays(-1),
                DoctorId = -1,
                CreatedBy = -1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            Assert.True(result.Errors.Count >= 5, "Should have at least 5 validation errors");
            result.ShouldHaveValidationErrorFor(x => x.Title);
            result.ShouldHaveValidationErrorFor(x => x.Start);
            result.ShouldHaveValidationErrorFor(x => x.End);
            result.ShouldHaveValidationErrorFor(x => x.DoctorId);
            result.ShouldHaveValidationErrorFor(x => x.CreatedBy);
        }

        #endregion
    }
}
