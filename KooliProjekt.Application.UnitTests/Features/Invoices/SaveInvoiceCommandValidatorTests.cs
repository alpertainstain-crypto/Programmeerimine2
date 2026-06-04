using FluentValidation.TestHelper;
using KooliProjekt.Application.Features.Invoices;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.Invoices
{
    public class SaveInvoiceCommandValidatorTests
    {
        private readonly SaveInvoiceCommandValidator _validator;

        public SaveInvoiceCommandValidatorTests()
        {
            _validator = new SaveInvoiceCommandValidator();
        }

        #region InvoiceNo Validation Tests

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Validator_WithInvalidInvoiceNo_ShouldFail(int invoiceNo)
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = invoiceNo,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 100m,
                Discount = 0m,
                GrandTotal = 100m,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.InvoiceNo);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1001)]
        [InlineData(999999)]
        public void Validator_WithValidInvoiceNo_ShouldPass(int invoiceNo)
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = invoiceNo,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 100m,
                Discount = 0m,
                GrandTotal = 100m,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.InvoiceNo);
        }

        #endregion

        #region Status Validation Tests

        [Fact]
        public void Validator_WithEmptyStatus_ShouldFail()
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "",
                Subtotal = 100m,
                Discount = 0m,
                GrandTotal = 100m,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Status)
                .WithErrorMessage("Status is required");
        }

        [Theory]
        [InlineData("InvalidStatus")]
        [InlineData("Processing")]
        [InlineData("Completed")]
        public void Validator_WithInvalidStatus_ShouldFail(string status)
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = status,
                Subtotal = 100m,
                Discount = 0m,
                GrandTotal = 100m,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Status);
        }

        [Theory]
        [InlineData("Pending")]
        [InlineData("Paid")]
        [InlineData("Cancelled")]
        [InlineData("Overdue")]
        public void Validator_WithValidStatus_ShouldPass(string status)
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = status,
                Subtotal = 100m,
                Discount = 0m,
                GrandTotal = 100m,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Status);
        }

        #endregion

        #region Subtotal Validation Tests

        [Theory]
        [InlineData(-1)]
        [InlineData(-100.50)]
        public void Validator_WithNegativeSubtotal_ShouldFail(decimal subtotal)
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = subtotal,
                Discount = 0m,
                GrandTotal = Math.Max(subtotal, 0),
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Subtotal);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(1000.50)]
        public void Validator_WithValidSubtotal_ShouldPass(decimal subtotal)
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = subtotal,
                Discount = 0m,
                GrandTotal = subtotal,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Subtotal);
        }

        #endregion

        #region Discount Validation Tests

        [Theory]
        [InlineData(-1)]
        [InlineData(-50)]
        public void Validator_WithNegativeDiscount_ShouldFail(decimal discount)
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 100m,
                Discount = discount,
                GrandTotal = 100m - discount,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Discount);
        }

        [Fact]
        public void Validator_WithDiscountGreaterThanSubtotal_ShouldFail()
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 100m,
                Discount = 150m,
                GrandTotal = -50m,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Discount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(100)]
        public void Validator_WithValidDiscount_ShouldPass(decimal discount)
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 100m,
                Discount = discount,
                GrandTotal = 100m - discount,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Discount);
        }

        #endregion

        #region GrandTotal Validation Tests

        [Theory]
        [InlineData(-1)]
        [InlineData(-100.50)]
        public void Validator_WithNegativeGrandTotal_ShouldFail(decimal grandTotal)
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 100m,
                Discount = 0m,
                GrandTotal = grandTotal,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.GrandTotal);
        }

        [Fact]
        public void Validator_WithIncorrectGrandTotalCalculation_ShouldFail()
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 100m,
                Discount = 20m,
                GrandTotal = 95m,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.GrandTotal)
                .WithErrorMessage("Grand total must equal subtotal minus discount");
        }

        [Theory]
        [InlineData(100, 0, 100)]
        [InlineData(100, 10, 90)]
        [InlineData(200, 50, 150)]
        [InlineData(0, 0, 0)]
        public void Validator_WithCorrectGrandTotal_ShouldPass(decimal subtotal, decimal discount, decimal grandTotal)
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = subtotal,
                Discount = discount,
                GrandTotal = grandTotal,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.GrandTotal);
        }

        #endregion

        #region DueDate Validation Tests

        [Fact]
        public void Validator_WithDueDateBeforeInvoiceDate_ShouldFail()
        {
            // Arrange
            var today = DateTime.Now;
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = today,
                DueDate = today.AddDays(-5),
                Status = "Pending",
                Subtotal = 100m,
                Discount = 0m,
                GrandTotal = 100m,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DueDate);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(30)]
        [InlineData(365)]
        public void Validator_WithValidDueDate_ShouldPass(int daysAfter)
        {
            // Arrange
            var today = DateTime.Now;
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = today,
                DueDate = today.AddDays(daysAfter),
                Status = "Pending",
                Subtotal = 100m,
                Discount = 0m,
                GrandTotal = 100m,
                AppointmentId = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.DueDate);
        }

        #endregion

        #region AppointmentId Validation Tests

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validator_WithInvalidAppointmentId_ShouldFail(int appointmentId)
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 100m,
                Discount = 0m,
                GrandTotal = 100m,
                AppointmentId = appointmentId
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.AppointmentId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999999)]
        public void Validator_WithValidAppointmentId_ShouldPass(int appointmentId)
        {
            // Arrange
            var command = new SaveInvoiceCommand
            {
                InvoiceNo = 1001,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                Status = "Pending",
                Subtotal = 100m,
                Discount = 0m,
                GrandTotal = 100m,
                AppointmentId = appointmentId
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.AppointmentId);
        }

        #endregion

        #region Complete Command Validation Tests

        [Fact]
        public void Validator_WithValidCompleteCommand_ShouldPass()
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
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion
    }
}
