using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Availability;
using Xunit;
using AvailabilityData = Availability;

namespace KooliProjekt.Application.UnitTests.Features.Availability
{
    public class SaveAvailabilityCommandHandlerTests : ServiceTestBase
    {
        private readonly SaveAvailabilityCommandHandler _handler;

        public SaveAvailabilityCommandHandlerTests()
        {
            _handler = new SaveAvailabilityCommandHandler(DbContext);
        }

        [Fact]
        public async Task SaveAvailabilityCommandHandler_WithNewAvailability_ShouldCreateAvailability()
        {
            // Arrange
            var command = new SaveAvailabilityCommand
            {
                title = "Monday Morning"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SaveAvailabilityCommandHandler_WithNewAvailability_ShouldSaveToDatabase()
        {
            // Arrange
            var command = new SaveAvailabilityCommand
            {
                title = "Tuesday Afternoon"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var availabilityCount = DbContext.Availability.Count();

            // Assert
            Assert.False(result.HasErrors);
            Assert.True(availabilityCount > 0);
        }

        [Fact]
        public async Task SaveAvailabilityCommandHandler_WithExistingAvailability_ShouldUpdateAvailability()
        {
            // Arrange
            var availability = new AvailabilityData
            {
                DoctorId = 1,
                StartTime = TimeSpan.FromHours(9),
                EndTime = TimeSpan.FromHours(17),
                DayOfWeek = 1,
                DateValue = DateTime.Now
            };

            await DbContext.Availability.AddAsync(availability);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveAvailabilityCommand
            {
                title = "Updated Time Slot"
            };
            updateCommand.GetType().GetProperty("Id").SetValue(updateCommand, availability.Id);

            // Act
            var result = await _handler.Handle(updateCommand, CancellationToken.None);
            var updatedAvailability = await DbContext.Availability.FindAsync(availability.Id);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(updatedAvailability);
            Assert.Equal(1, updatedAvailability.DoctorId);
        }

        [Fact]
        public async Task SaveAvailabilityCommandHandler_WithNewAvailability_ShouldSetDoctorId()
        {
            // Arrange
            var command = new SaveAvailabilityCommand
            {
                title = "Wednesday"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var availabilities = DbContext.Availability.ToList();

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotEmpty(availabilities);
            var created = availabilities[availabilities.Count - 1];
            Assert.Equal(1, created.DoctorId);
        }

        [Fact]
        public async Task SaveAvailabilityCommandHandler_MultipleAvailabilities_ShouldHandleMultipleOperations()
        {
            // Arrange
            var command1 = new SaveAvailabilityCommand
            {
                title = "Thursday Morning"
            };

            var command2 = new SaveAvailabilityCommand
            {
                title = "Friday Afternoon"
            };

            // Act
            var result1 = await _handler.Handle(command1, CancellationToken.None);
            var result2 = await _handler.Handle(command2, CancellationToken.None);
            var availabilityCount = DbContext.Availability.Count();

            // Assert
            Assert.False(result1.HasErrors);
            Assert.False(result2.HasErrors);
            Assert.True(availabilityCount >= 2);
        }

        [Fact]
        public async Task SaveAvailabilityCommandHandler_UpdateAvailability_ShouldPersistChanges()
        {
            // Arrange
            var availability = new AvailabilityData
            {
                DoctorId = 2,
                StartTime = TimeSpan.FromHours(10),
                EndTime = TimeSpan.FromHours(16),
                DayOfWeek = 2,
                DateValue = DateTime.Now
            };

            await DbContext.Availability.AddAsync(availability);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveAvailabilityCommand
            {
                title = "Updated Slot"
            };
            updateCommand.GetType().GetProperty("Id").SetValue(updateCommand, availability.Id);

            // Act
            var result = await _handler.Handle(updateCommand, CancellationToken.None);
            var updated = await DbContext.Availability.FindAsync(availability.Id);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(updated);
            Assert.Equal(1, updated.DoctorId);
        }
    }
}
