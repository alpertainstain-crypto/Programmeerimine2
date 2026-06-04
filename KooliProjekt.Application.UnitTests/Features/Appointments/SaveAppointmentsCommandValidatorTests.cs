using FluentValidation.TestHelper;
using KooliProjekt.Application.Features.Appointments;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.Appointments
{
    public class SaveAppointmentsCommandValidatorTests
    {
        private readonly SaveAppointmentsCommandValidator _validator;

        public SaveAppointmentsCommandValidatorTests()
        {
            _validator = new SaveAppointmentsCommandValidator();
        }

        #region Title Validation Tests

        [Fact]
        public void Validator_WithEmptyTitle_ShouldFail()
        {
            // Arrange
            var command = new SaveAppointmentsCommand
            {
                title = "",
                AppointmentTime = DateTime.Now.AddDays(1),
                UserId = 1,
                DoctorId = 1
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
            var command = new SaveAppointmentsCommand
            {
                title = title,
                AppointmentTime = DateTime.Now.AddDays(1),
                UserId = 1,
                DoctorId = 1
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
            var command = new SaveAppointmentsCommand
            {
                title = new string('a', 101),
                AppointmentTime = DateTime.Now.AddDays(1),
                UserId = 1,
                DoctorId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.title)
                .WithErrorMessage("Title cannot exceed 100 characters");
        }

        [Theory]
        [InlineData("Annual Checkup")]
        [InlineData("Follow-up Appointment")]
        [InlineData("Consultation")]
        public void Validator_WithValidTitle_ShouldPass(string title)
        {
            // Arrange
            var command = new SaveAppointmentsCommand
            {
                title = title,
                AppointmentTime = DateTime.Now.AddDays(1),
                UserId = 1,
                DoctorId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.title);
        }

        #endregion

        #region AppointmentTime Validation Tests

        [Fact]
        public void Validator_WithNullAppointmentTime_ShouldFail()
        {
            // Arrange
            var command = new SaveAppointmentsCommand
            {
                title = "Annual Checkup",
                AppointmentTime = null,
                UserId = 1,
                DoctorId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.AppointmentTime)
                .WithErrorMessage("Appointment time is required");
        }

        [Fact]
        public void Validator_WithPastAppointmentTime_ShouldFail()
        {
            // Arrange
            var command = new SaveAppointmentsCommand
            {
                title = "Annual Checkup",
                AppointmentTime = DateTime.Now.AddDays(-1),
                UserId = 1,
                DoctorId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.AppointmentTime);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(30)]
        [InlineData(365)]
        public void Validator_WithFutureAppointmentTime_ShouldPass(int daysFromNow)
        {
            // Arrange
            var command = new SaveAppointmentsCommand
            {
                title = "Annual Checkup",
                AppointmentTime = DateTime.Now.AddDays(daysFromNow),
                UserId = 1,
                DoctorId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.AppointmentTime);
        }

        #endregion

        #region UserId Validation Tests

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Validator_WithInvalidUserId_ShouldFail(int userId)
        {
            // Arrange
            var command = new SaveAppointmentsCommand
            {
                title = "Annual Checkup",
                AppointmentTime = DateTime.Now.AddDays(1),
                UserId = userId,
                DoctorId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UserId)
                .WithErrorMessage("User ID must be greater than 0");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999999)]
        public void Validator_WithValidUserId_ShouldPass(int userId)
        {
            // Arrange
            var command = new SaveAppointmentsCommand
            {
                title = "Annual Checkup",
                AppointmentTime = DateTime.Now.AddDays(1),
                UserId = userId,
                DoctorId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UserId);
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
            var command = new SaveAppointmentsCommand
            {
                title = "Annual Checkup",
                AppointmentTime = DateTime.Now.AddDays(1),
                UserId = 1,
                DoctorId = doctorId
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
            var command = new SaveAppointmentsCommand
            {
                title = "Annual Checkup",
                AppointmentTime = DateTime.Now.AddDays(1),
                UserId = 1,
                DoctorId = doctorId
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.DoctorId);
        }

        #endregion

        #region Complete Command Validation Tests

        [Fact]
        public void Validator_WithValidCompleteCommand_ShouldPass()
        {
            // Arrange
            var command = new SaveAppointmentsCommand
            {
                Id = 0,
                title = "Annual Checkup",
                AppointmentTime = DateTime.Now.AddDays(7),
                UserId = 1,
                DoctorId = 1
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
            var command = new SaveAppointmentsCommand
            {
                title = "a",
                AppointmentTime = DateTime.Now.AddDays(-1),
                UserId = -1,
                DoctorId = -1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            Assert.True(result.Errors.Count >= 4, "Should have at least 4 validation errors");
            result.ShouldHaveValidationErrorFor(x => x.title);
            result.ShouldHaveValidationErrorFor(x => x.AppointmentTime);
            result.ShouldHaveValidationErrorFor(x => x.UserId);
            result.ShouldHaveValidationErrorFor(x => x.DoctorId);
        }

        #endregion
    }
}
