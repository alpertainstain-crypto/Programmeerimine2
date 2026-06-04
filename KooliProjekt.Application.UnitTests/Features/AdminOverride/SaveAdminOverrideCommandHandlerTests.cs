using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features;
using KooliProjekt.Application.Features.AdminOverrideList;
using Xunit;
using AdminOverrideData = AdminOverride;

namespace KooliProjekt.Application.UnitTests.Features.AdminOverride
{
    public class SaveAdminOverrideCommandHandlerTests : ServiceTestBase
    {
        private readonly SaveAdminOverrideCommandHandler _handler;

        public SaveAdminOverrideCommandHandlerTests()
        {
            _handler = new SaveAdminOverrideCommandHandler(DbContext);
        }

        [Fact]
        public async Task SaveAdminOverrideCommandHandler_WithNewAdminOverride_ShouldCreateAdminOverride()
        {
            // Arrange
            var command = new SaveAdminOverrideCommand
            {
                Id = 0,
                Title = "Emergency Override",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2),
                DoctorId = 1,
                CreatedBy = 1
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SaveAdminOverrideCommandHandler_WithNewAdminOverride_ShouldSaveToDatabase()
        {
            // Arrange
            var command = new SaveAdminOverrideCommand
            {
                Id = 0,
                Title = "System Maintenance",
                Start = DateTime.Now.AddDays(1),
                End = DateTime.Now.AddDays(1).AddHours(4),
                DoctorId = 1,
                CreatedBy = 1
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var overridesCount = DbContext.AdminOverride.Count();

            // Assert
            Assert.False(result.HasErrors);
            Assert.True(overridesCount > 0);
        }

        [Fact]
        public async Task SaveAdminOverrideCommandHandler_WithExistingAdminOverride_ShouldUpdateAdminOverride()
        {
            // Arrange
            var adminOverride = new AdminOverrideData
            {
                Reason = "Original Reason",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                DoctorId = 1,
                CreatedBy = 1
            };

            await DbContext.AdminOverride.AddAsync(adminOverride);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveAdminOverrideCommand
            {
                Id = adminOverride.Id,
                Title = "Updated Reason",
                Start = DateTime.Now.AddDays(1),
                End = DateTime.Now.AddDays(1).AddHours(2),
                DoctorId = 2,
                CreatedBy = 2
            };

            // Act
            var result = await _handler.Handle(updateCommand, CancellationToken.None);
            var updatedOverride = await DbContext.AdminOverride.FindAsync(adminOverride.Id);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(updatedOverride);
            Assert.Equal("Updated Reason", updatedOverride.Reason);
            Assert.Equal(2, updatedOverride.DoctorId);
            Assert.Equal(2, updatedOverride.CreatedBy);
        }

        [Fact]
        public async Task SaveAdminOverrideCommandHandler_WithNonExistentAdminOverrideId_ShouldReturnError()
        {
            // Arrange
            var command = new SaveAdminOverrideCommand
            {
                Id = 9999,
                Title = "Non Existent",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                DoctorId = 1,
                CreatedBy = 1
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.HasErrors);
            Assert.Contains("Admin Override not found", result.Errors);
        }

        [Fact]
        public async Task SaveAdminOverrideCommandHandler_UpdateAdminOverrideProperties_ShouldPersistAllProperties()
        {
            // Arrange
            var startTime = new DateTime(2025, 1, 10, 9, 0, 0);
            var endTime = new DateTime(2025, 1, 10, 11, 0, 0);
            var updatedStart = new DateTime(2025, 1, 15, 10, 0, 0);
            var updatedEnd = new DateTime(2025, 1, 15, 12, 0, 0);

            var adminOverride = new AdminOverrideData
            {
                Reason = "Initial Override",
                Start = startTime,
                End = endTime,
                DoctorId = 1,
                CreatedBy = 1
            };

            await DbContext.AdminOverride.AddAsync(adminOverride);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveAdminOverrideCommand
            {
                Id = adminOverride.Id,
                Title = "Modified Override",
                Start = updatedStart,
                End = updatedEnd,
                DoctorId = 3,
                CreatedBy = 2
            };

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);
            var result = await DbContext.AdminOverride.FindAsync(adminOverride.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Modified Override", result.Reason);
            Assert.Equal(updatedStart, result.Start);
            Assert.Equal(updatedEnd, result.End);
            Assert.Equal(3, result.DoctorId);
            Assert.Equal(2, result.CreatedBy);
        }

        [Fact]
        public async Task SaveAdminOverrideCommandHandler_MultipleAdminOverrides_ShouldHandleMultipleOperations()
        {
            // Arrange
            var command1 = new SaveAdminOverrideCommand
            {
                Id = 0,
                Title = "Override 1",
                Start = DateTime.Now.AddDays(1),
                End = DateTime.Now.AddDays(1).AddHours(2),
                DoctorId = 1,
                CreatedBy = 1
            };

            var command2 = new SaveAdminOverrideCommand
            {
                Id = 0,
                Title = "Override 2",
                Start = DateTime.Now.AddDays(2),
                End = DateTime.Now.AddDays(2).AddHours(3),
                DoctorId = 2,
                CreatedBy = 2
            };

            // Act
            var result1 = await _handler.Handle(command1, CancellationToken.None);
            var result2 = await _handler.Handle(command2, CancellationToken.None);
            var overridesCount = DbContext.AdminOverride.Count();

            // Assert
            Assert.False(result1.HasErrors);
            Assert.False(result2.HasErrors);
            Assert.True(overridesCount >= 2);
        }

        [Fact]
        public async Task SaveAdminOverrideCommandHandler_WithNullValues_ShouldPreserveExistingValues()
        {
            // Arrange
            var startTime = new DateTime(2025, 1, 10, 9, 0, 0);
            var endTime = new DateTime(2025, 1, 10, 11, 0, 0);

            var adminOverride = new AdminOverrideData
            {
                Reason = "Original Reason",
                Start = startTime,
                End = endTime,
                DoctorId = 1,
                CreatedBy = 1
            };

            await DbContext.AdminOverride.AddAsync(adminOverride);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveAdminOverrideCommand
            {
                Id = adminOverride.Id,
                Title = null,
                Start = null,
                End = null,
                DoctorId = 0,
                CreatedBy = 0
            };

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);
            var result = await DbContext.AdminOverride.FindAsync(adminOverride.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Original Reason", result.Reason);
            Assert.Equal(startTime, result.Start);
            Assert.Equal(endTime, result.End);
            Assert.Equal(1, result.DoctorId);
            Assert.Equal(1, result.CreatedBy);
        }
    }
}
