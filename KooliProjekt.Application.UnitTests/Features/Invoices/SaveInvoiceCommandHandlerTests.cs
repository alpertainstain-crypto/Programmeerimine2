using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Invoices;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.Invoices
{
    public class SaveInvoiceCommandHandlerTests : ServiceTestBase
    {
        private readonly SaveInvoiceCommandHandler _handler;

        public SaveInvoiceCommandHandlerTests()
        {
            _handler = new SaveInvoiceCommandHandler(DbContext);
        }

        [Fact]
        public async Task SaveInvoiceCommandHandler_WithNewInvoice_ShouldCreateInvoice()
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                Id = 0,
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 100m,
                Discount = 10m,
                GrandTotal = 90m,
                MarkedPaidAt = null,
                AppointmentId = 1
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SaveInvoiceCommandHandler_WithNewInvoice_ShouldSaveToDatabase()
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                Id = 0,
                InvoiceNo = 1002,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 200m,
                Discount = 20m,
                GrandTotal = 180m,
                MarkedPaidAt = null,
                AppointmentId = 1
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var invoicesCount = DbContext.Invoice.Count();

            // Assert
            Assert.False(result.HasErrors);
            Assert.True(invoicesCount > 0);
        }

        [Fact]
        public async Task SaveInvoiceCommandHandler_WithExistingInvoice_ShouldUpdateInvoice()
        {
            // Arrange
            var invoice = new Invoice
            {
                InvoiceNo = 1003,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 300m,
                Discount = 0m,
                GrandTotal = 300m,
                MarkedPaidAt = null,
                AppointmentId = 1
            };

            await DbContext.Invoice.AddAsync(invoice);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveInvoiceCommand
            {
                Id = invoice.Id,
                InvoiceNo = 1003,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Paid",
                Subtotal = 300m,
                Discount = 30m,
                GrandTotal = 270m,
                MarkedPaidAt = DateTime.Now,
                AppointmentId = 1
            };

            // Act
            var result = await _handler.Handle(updateCommand, CancellationToken.None);
            var updatedInvoice = await DbContext.Invoice.FindAsync(invoice.Id);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(updatedInvoice);
            Assert.Equal("Paid", updatedInvoice.Status);
            Assert.Equal(30m, updatedInvoice.Discount);
            Assert.Equal(270m, updatedInvoice.GrandTotal);
        }

        [Fact]
        public async Task SaveInvoiceCommandHandler_WithNonExistentInvoiceId_ShouldReturnError()
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                Id = 9999,
                InvoiceNo = 1004,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 400m,
                Discount = 40m,
                GrandTotal = 360m,
                MarkedPaidAt = null,
                AppointmentId = 1
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.HasErrors);
            Assert.Contains("Invoice not found", result.Errors);
        }

        [Fact]
        public async Task SaveInvoiceCommandHandler_UpdateInvoiceProperties_ShouldPersistAllProperties()
        {
            // Arrange
            var invoice = new Invoice
            {
                InvoiceNo = 1005,
                InvoiceDate = new DateTime(2025, 1, 1),
                DueDate = new DateTime(2025, 2, 1),
                Status = "Pending",
                Subtotal = 500m,
                Discount = 0m,
                GrandTotal = 500m,
                MarkedPaidAt = null,
                AppointmentId = 1
            };

            await DbContext.Invoice.AddAsync(invoice);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveInvoiceCommand
            {
                Id = invoice.Id,
                InvoiceNo = 2005,
                InvoiceDate = new DateTime(2025, 1, 15),
                DueDate = new DateTime(2025, 2, 15),
                Status = "Overdue",
                Subtotal = 550m,
                Discount = 50m,
                GrandTotal = 500m,
                MarkedPaidAt = new DateTime(2025, 2, 10),
                AppointmentId = 2
            };

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);
            var result = await DbContext.Invoice.FindAsync(invoice.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2005, result.InvoiceNo);
            Assert.Equal(new DateTime(2025, 1, 15), result.InvoiceDate);
            Assert.Equal(new DateTime(2025, 2, 15), result.DueDate);
            Assert.Equal("Overdue", result.Status);
            Assert.Equal(550m, result.Subtotal);
            Assert.Equal(50m, result.Discount);
            Assert.Equal(500m, result.GrandTotal);
            Assert.Equal(new DateTime(2025, 2, 10), result.MarkedPaidAt);
            Assert.Equal(2, result.AppointmentId);
        }
    }
}
