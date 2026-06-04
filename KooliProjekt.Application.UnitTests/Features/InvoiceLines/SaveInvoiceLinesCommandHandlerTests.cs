using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.InvoiceLines;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.InvoiceLines
{
    public class SaveInvoiceLinesCommandHandlerTests : ServiceTestBase
    {
        private readonly SaveInvoiceLinesCommandHandler _handler;

        public SaveInvoiceLinesCommandHandlerTests()
        {
            _handler = new SaveInvoiceLinesCommandHandler(DbContext);
        }

        private Invoice CreateTestInvoice()
        {
            var invoice = new Invoice
            {
                InvoiceNo = 2001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 1000m,
                Discount = 0m,
                GrandTotal = 1000m,
                AppointmentId = 1
            };

            DbContext.Invoice.Add(invoice);
            DbContext.SaveChanges();
            return invoice;
        }

        [Fact]
        public async Task SaveInvoiceLinesCommandHandler_WithNewInvoiceLine_ShouldCreateInvoiceLine()
        {
            // Arrange
            var invoice = CreateTestInvoice();

            var command = new SaveInvoiceLinesCommand
            {
                Id = 0,
                InvoiceId = invoice.Id,
                Description = "Service 1",
                Amount = 100m
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SaveInvoiceLinesCommandHandler_WithNewInvoiceLine_ShouldSaveToDatabase()
        {
            // Arrange
            var invoice = CreateTestInvoice();

            var command = new SaveInvoiceLinesCommand
            {
                Id = 0,
                InvoiceId = invoice.Id,
                Description = "Service 2",
                Amount = 150m
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var linesCount = DbContext.InvoiceLines.Count();

            // Assert
            Assert.False(result.HasErrors);
            Assert.True(linesCount > 0);
        }

        [Fact]
        public async Task SaveInvoiceLinesCommandHandler_WithExistingInvoiceLine_ShouldUpdateInvoiceLine()
        {
            // Arrange
            var invoice = CreateTestInvoice();

            var invoiceLine = new InvoiceLine
            {
                InvoiceId = invoice.Id,
                Description = "Original Description",
                Amount = 200m
            };

            await DbContext.InvoiceLines.AddAsync(invoiceLine);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveInvoiceLinesCommand
            {
                Id = invoiceLine.Id,
                InvoiceId = invoice.Id,
                Description = "Updated Description",
                Amount = 250m
            };

            // Act
            var result = await _handler.Handle(updateCommand, CancellationToken.None);
            var updatedLine = await DbContext.InvoiceLines.FindAsync(invoiceLine.Id);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(updatedLine);
            Assert.Equal("Updated Description", updatedLine.Description);
            Assert.Equal(250m, updatedLine.Amount);
        }

        [Fact]
        public async Task SaveInvoiceLinesCommandHandler_WithNonExistentInvoiceLineId_ShouldReturnError()
        {
            // Arrange
            var invoice = CreateTestInvoice();

            var command = new SaveInvoiceLinesCommand
            {
                Id = 9999,
                InvoiceId = invoice.Id,
                Description = "Non-existent",
                Amount = 100m
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.HasErrors);
            Assert.Contains("Invoice line not found", result.Errors);
        }

        [Fact]
        public async Task SaveInvoiceLinesCommandHandler_UpdateInvoiceLineProperties_ShouldPersistAllProperties()
        {
            // Arrange
            var invoice = CreateTestInvoice();

            var invoiceLine = new InvoiceLine
            {
                InvoiceId = invoice.Id,
                Description = "Original Service",
                Amount = 300m
            };

            await DbContext.InvoiceLines.AddAsync(invoiceLine);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveInvoiceLinesCommand
            {
                Id = invoiceLine.Id,
                InvoiceId = invoice.Id + 1,
                Description = "Modified Service",
                Amount = 500m
            };

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);
            var result = await DbContext.InvoiceLines.FindAsync(invoiceLine.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(invoice.Id + 1, result.InvoiceId);
            Assert.Equal("Modified Service", result.Description);
            Assert.Equal(500m, result.Amount);
        }

        [Fact]
        public async Task SaveInvoiceLinesCommandHandler_MultipleInvoiceLines_ShouldHandleMultipleOperations()
        {
            // Arrange
            var invoice = CreateTestInvoice();

            var command1 = new SaveInvoiceLinesCommand
            {
                Id = 0,
                InvoiceId = invoice.Id,
                Description = "Service A",
                Amount = 100m
            };

            var command2 = new SaveInvoiceLinesCommand
            {
                Id = 0,
                InvoiceId = invoice.Id,
                Description = "Service B",
                Amount = 200m
            };

            // Act
            var result1 = await _handler.Handle(command1, CancellationToken.None);
            var result2 = await _handler.Handle(command2, CancellationToken.None);
            var linesCount = DbContext.InvoiceLines.Count();

            // Assert
            Assert.False(result1.HasErrors);
            Assert.False(result2.HasErrors);
            Assert.Equal(2, linesCount);
        }
    }
}
