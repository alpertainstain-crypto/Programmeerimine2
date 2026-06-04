using FluentValidation.TestHelper;
using KooliProjekt.Application.Features.Users;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.Users
{
    public class SaveUsersCommandValidatorTests
    {
        private readonly SaveUsersCommandValidator _validator;

        public SaveUsersCommandValidatorTests()
        {
            _validator = new SaveUsersCommandValidator();
        }

        #region Name Validation Tests

        [Fact]
        public void Validator_WithEmptyName_ShouldFail()
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = "",
                Email = "test@example.com",
                Phone = "+372 5123 4567",
                Role = "Doctor"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Name is required");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("")]
        public void Validator_WithNameShorterThanMinimum_ShouldFail(string name)
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = name,
                Email = "test@example.com",
                Phone = "+372 5123 4567",
                Role = "Doctor"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Validator_WithNameLongerThanMaximum_ShouldFail()
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = new string('a', 101),
                Email = "test@example.com",
                Phone = "+372 5123 4567",
                Role = "Doctor"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Name cannot exceed 100 characters");
        }

        [Theory]
        [InlineData("John Doe")]
        [InlineData("Jane Smith")]
        [InlineData("Dr. Michael Johnson")]
        public void Validator_WithValidName_ShouldPass(string name)
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = name,
                Email = "test@example.com",
                Phone = "+372 5123 4567",
                Role = "Doctor"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        #endregion

        #region Email Validation Tests

        [Fact]
        public void Validator_WithEmptyEmail_ShouldFail()
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = "John Doe",
                Email = "",
                Phone = "+372 5123 4567",
                Role = "Doctor"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Email is required");
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("test@")]
        [InlineData("@example.com")]
        public void Validator_WithInvalidEmailFormat_ShouldFail(string email)
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = "John Doe",
                Email = email,
                Phone = "+372 5123 4567",
                Role = "Doctor"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Validator_WithEmailLongerThanMaximum_ShouldFail()
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = "John Doe",
                Email = new string('a', 100) + "@example.com",
                Phone = "+372 5123 4567",
                Role = "Doctor"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("john@example.com")]
        [InlineData("jane.smith@company.co.uk")]
        [InlineData("doctor123@hospital.org")]
        public void Validator_WithValidEmail_ShouldPass(string email)
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = "John Doe",
                Email = email,
                Phone = "+372 5123 4567",
                Role = "Doctor"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        #endregion

        #region Phone Validation Tests

        [Fact]
        public void Validator_WithEmptyPhone_ShouldFail()
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = "John Doe",
                Email = "test@example.com",
                Phone = "",
                Role = "Doctor"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Phone)
                .WithErrorMessage("Phone is required");
        }

        [Theory]
        [InlineData("123")]
        [InlineData("a1b2c")]
        [InlineData("!@#$%")]
        public void Validator_WithInvalidPhoneFormat_ShouldFail(string phone)
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = "John Doe",
                Email = "test@example.com",
                Phone = phone,
                Role = "Doctor"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Phone);
        }

        [Theory]
        [InlineData("+372 5123 4567")]
        [InlineData("555-1234-5678")]
        [InlineData("+1 (555) 123-4567")]
        [InlineData("5551234567")]
        public void Validator_WithValidPhoneNumber_ShouldPass(string phone)
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = "John Doe",
                Email = "test@example.com",
                Phone = phone,
                Role = "Doctor"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }

        #endregion

        #region Role Validation Tests

        [Fact]
        public void Validator_WithEmptyRole_ShouldFail()
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = "John Doe",
                Email = "test@example.com",
                Phone = "+372 5123 4567",
                Role = ""
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Role)
                .WithErrorMessage("Role is required");
        }

        [Theory]
        [InlineData("InvalidRole")]
        [InlineData("Nurse")]
        [InlineData("Manager")]
        public void Validator_WithInvalidRole_ShouldFail(string role)
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = "John Doe",
                Email = "test@example.com",
                Phone = "+372 5123 4567",
                Role = role
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Role);
        }

        [Theory]
        [InlineData("Doctor")]
        [InlineData("Patient")]
        [InlineData("Admin")]
        [InlineData("Staff")]
        public void Validator_WithValidRole_ShouldPass(string role)
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Name = "John Doe",
                Email = "test@example.com",
                Phone = "+372 5123 4567",
                Role = role
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Role);
        }

        #endregion

        #region Complete Command Validation Tests

        [Fact]
        public void Validator_WithValidCompleteCommand_ShouldPass()
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Id = 0,
                Name = "John Doe",
                Email = "john@example.com",
                Phone = "+372 5123 4567",
                PasswordHash = "hashed_password",
                Role = "Doctor",
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
            var command = new SaveUsersCommand
            {
                Name = "",
                Email = "invalid-email",
                Phone = "",
                Role = "InvalidRole"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            Assert.True(result.Errors.Count >= 3, "Should have at least 3 validation errors");
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Email);
            result.ShouldHaveValidationErrorFor(x => x.Phone);
            result.ShouldHaveValidationErrorFor(x => x.Role);
        }

        #endregion
    }
}
