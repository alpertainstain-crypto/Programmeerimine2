using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Services;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.Services
{
    public class SaveServicesCommandHandlerTests : ServiceTestBase
    {
        private readonly SaveServicesCommandHandler _handler;

        public SaveServicesCommandHandlerTests()
        {
            _handler = new SaveServicesCommandHandler(DbContext);
        }

        [Fact]
        public async Task SaveServicesCommandHandler_WithNewService_ShouldCreateService()
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Id = 0,
                Code = "SVC001",
                Description = "General Checkup",
                UnitPrice = 100m
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SaveServicesCommandHandler_WithNewService_ShouldSaveToDatabase()
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Id = 0,
                Code = "SVC002",
                Description = "Dental Cleaning",
                UnitPrice = 150m
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var servicesCount = DbContext.Services.Count();

            // Assert
            Assert.False(result.HasErrors);
            Assert.True(servicesCount > 0);
        }

        [Fact]
        public async Task SaveServicesCommandHandler_WithExistingService_ShouldUpdateService()
        {
            // Arrange
            var service = new Service
            {
                Code = "SVC003",
                Description = "Original Description",
                UnitPrice = 200m
            };

            await DbContext.Services.AddAsync(service);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveServicesCommand
            {
                Id = service.Id,
                Code = "SVC003",
                Description = "Updated Description",
                UnitPrice = 250m
            };

            // Act
            var result = await _handler.Handle(updateCommand, CancellationToken.None);
            var updatedService = await DbContext.Services.FindAsync(service.Id);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(updatedService);
            Assert.Equal("Updated Description", updatedService.Description);
            Assert.Equal(250m, updatedService.UnitPrice);
        }

        [Fact]
        public async Task SaveServicesCommandHandler_WithNonExistentServiceId_ShouldReturnError()
        {
            // Arrange
            var command = new SaveServicesCommand
            {
                Id = 9999,
                Code = "SVC999",
                Description = "Non Existent",
                UnitPrice = 500m
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.HasErrors);
            Assert.Contains("Service not found", result.Errors);
        }

        [Fact]
        public async Task SaveServicesCommandHandler_UpdateServiceProperties_ShouldPersistAllProperties()
        {
            // Arrange
            var service = new Service
            {
                Code = "SVC004",
                Description = "Original Service",
                UnitPrice = 300m
            };

            await DbContext.Services.AddAsync(service);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveServicesCommand
            {
                Id = service.Id,
                Code = "SVC004-UPDATE",
                Description = "Modified Service",
                UnitPrice = 350m
            };

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);
            var result = await DbContext.Services.FindAsync(service.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SVC004-UPDATE", result.Code);
            Assert.Equal("Modified Service", result.Description);
            Assert.Equal(350m, result.UnitPrice);
        }

        [Fact]
        public async Task SaveServicesCommandHandler_MultipleServices_ShouldHandleMultipleOperations()
        {
            // Arrange
            var command1 = new SaveServicesCommand
            {
                Id = 0,
                Code = "SVC010",
                Description = "Service A",
                UnitPrice = 100m
            };

            var command2 = new SaveServicesCommand
            {
                Id = 0,
                Code = "SVC011",
                Description = "Service B",
                UnitPrice = 200m
            };

            // Act
            var result1 = await _handler.Handle(command1, CancellationToken.None);
            var result2 = await _handler.Handle(command2, CancellationToken.None);
            var servicesCount = DbContext.Services.Count();

            // Assert
            Assert.False(result1.HasErrors);
            Assert.False(result2.HasErrors);
            Assert.True(servicesCount >= 2);
        }

        [Fact]
        public async Task SaveServicesCommandHandler_WithDifferentPrices_ShouldHandlePriceUpdates()
        {
            // Arrange
            var service = new Service
            {
                Code = "SVC012",
                Description = "Flexible Service",
                UnitPrice = 100m
            };

            await DbContext.Services.AddAsync(service);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveServicesCommand
            {
                Id = service.Id,
                Code = "SVC012",
                Description = "Flexible Service",
                UnitPrice = 500.50m
            };

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);
            var result = await DbContext.Services.FindAsync(service.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500.50m, result.UnitPrice);
        }
    }
}
