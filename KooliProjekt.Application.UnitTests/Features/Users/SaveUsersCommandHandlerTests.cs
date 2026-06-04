using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Users;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.Users
{
    public class SaveUsersCommandHandlerTests : ServiceTestBase
    {
        private readonly SaveUsersCommandHandler _handler;

        public SaveUsersCommandHandlerTests()
        {
            _handler = new SaveUsersCommandHandler(DbContext);
        }

        [Fact]
        public async Task SaveUsersCommandHandler_WithNewUser_ShouldCreateUser()
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
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SaveUsersCommandHandler_WithNewUser_ShouldSaveToDatabase()
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Id = 0,
                Name = "Jane Smith",
                Email = "jane@example.com",
                Phone = "+372 5234 5678",
                PasswordHash = "hashed_password",
                Role = "Patient",
                CreatedAt = DateTime.Now
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var usersCount = DbContext.Users.Count();

            // Assert
            Assert.False(result.HasErrors);
            Assert.True(usersCount > 0);
        }

        [Fact]
        public async Task SaveUsersCommandHandler_WithExistingUser_ShouldUpdateUser()
        {
            // Arrange
            var user = new User
            {
                FirstName = "Original",
                LastName = "Name",
                Email = "original@example.com",
                Phone = "+372 5345 6789",
                Role = "Doctor",
                CreatedAt = DateTime.Now
            };

            await DbContext.Users.AddAsync(user);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveUsersCommand
            {
                Id = user.Id,
                Name = "Updated Name",
                Email = "updated@example.com",
                Phone = "+372 5456 7890",
                PasswordHash = "new_hashed_password",
                Role = "Admin",
                CreatedAt = DateTime.Now
            };

            // Act
            var result = await _handler.Handle(updateCommand, CancellationToken.None);
            var updatedUser = await DbContext.Users.FindAsync(user.Id);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(updatedUser);
            Assert.Equal("Updated Name", updatedUser.FirstName);
            Assert.Equal("updated@example.com", updatedUser.Email);
            Assert.Equal("+372 5456 7890", updatedUser.Phone);
            Assert.Equal("Admin", updatedUser.Role);
        }

        [Fact]
        public async Task SaveUsersCommandHandler_WithNonExistentUserId_ShouldReturnError()
        {
            // Arrange
            var command = new SaveUsersCommand
            {
                Id = 9999,
                Name = "Non Existent",
                Email = "nonexistent@example.com",
                Phone = "+372 5567 8901",
                PasswordHash = "password",
                Role = "Doctor",
                CreatedAt = DateTime.Now
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.HasErrors);
            Assert.Contains("User not found", result.Errors);
        }

        [Fact]
        public async Task SaveUsersCommandHandler_UpdateUserProperties_ShouldPersistAllProperties()
        {
            // Arrange
            var createdAtOriginal = new DateTime(2025, 1, 1, 9, 0, 0);
            var createdAtUpdated = new DateTime(2025, 1, 15, 10, 0, 0);

            var user = new User
            {
                FirstName = "Initial",
                LastName = "User",
                Email = "initial@example.com",
                Phone = "+372 5678 9012",
                Role = "Patient",
                CreatedAt = createdAtOriginal
            };

            await DbContext.Users.AddAsync(user);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveUsersCommand
            {
                Id = user.Id,
                Name = "Modified User",
                Email = "modified@example.com",
                Phone = "+372 5789 0123",
                PasswordHash = "new_password_hash",
                Role = "Receptionist",
                CreatedAt = createdAtUpdated
            };

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);
            var result = await DbContext.Users.FindAsync(user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Modified User", result.FirstName);
            Assert.Equal("Modified User", result.LastName);
            Assert.Equal("modified@example.com", result.Email);
            Assert.Equal("+372 5789 0123", result.Phone);
            Assert.Equal("Receptionist", result.Role);
        }

        [Fact]
        public async Task SaveUsersCommandHandler_MultipleUsers_ShouldHandleMultipleOperations()
        {
            // Arrange
            var command1 = new SaveUsersCommand
            {
                Id = 0,
                Name = "User One",
                Email = "user1@example.com",
                Phone = "+372 5890 1234",
                PasswordHash = "password1",
                Role = "Doctor",
                CreatedAt = DateTime.Now
            };

            var command2 = new SaveUsersCommand
            {
                Id = 0,
                Name = "User Two",
                Email = "user2@example.com",
                Phone = "+372 5901 2345",
                PasswordHash = "password2",
                Role = "Patient",
                CreatedAt = DateTime.Now
            };

            // Act
            var result1 = await _handler.Handle(command1, CancellationToken.None);
            var result2 = await _handler.Handle(command2, CancellationToken.None);
            var usersCount = DbContext.Users.Count();

            // Assert
            Assert.False(result1.HasErrors);
            Assert.False(result2.HasErrors);
            Assert.True(usersCount >= 2);
        }
    }
}
