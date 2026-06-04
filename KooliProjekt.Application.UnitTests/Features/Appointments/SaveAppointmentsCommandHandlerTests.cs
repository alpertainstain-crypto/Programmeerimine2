using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Appointments;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features.Appointments
{
    public class SaveAppointmentsCommandHandlerTests : ServiceTestBase
    {
        private readonly SaveAppointmentsCommandHandler _handler;

        public SaveAppointmentsCommandHandlerTests()
        {
            _handler = new SaveAppointmentsCommandHandler(DbContext);
        }

        [Fact]
        public async Task SaveAppointmentsCommandHandler_WithNewAppointment_ShouldCreateAppointment()
        {
            // Arrange
            var appointmentTime = DateTime.Now.AddDays(1);
            var command = new SaveAppointmentsCommand
            {
                Id = 0,
                AppointmentTime = appointmentTime,
                UserId = 1,
                DoctorId = 1,
                title = "General Checkup"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SaveAppointmentsCommandHandler_WithNewAppointment_ShouldSaveToDatabase()
        {
            // Arrange
            var appointmentTime = DateTime.Now.AddDays(2);
            var command = new SaveAppointmentsCommand
            {
                Id = 0,
                AppointmentTime = appointmentTime,
                UserId = 1,
                DoctorId = 1,
                title = "Consultation"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var appointmentsCount = DbContext.Appointments.Count();

            // Assert
            Assert.False(result.HasErrors);
            Assert.True(appointmentsCount > 0);
        }

        [Fact]
        public async Task SaveAppointmentsCommandHandler_WithExistingAppointment_ShouldUpdateAppointment()
        {
            // Arrange
            var originalTime = DateTime.Now.AddDays(1);
            var updatedTime = DateTime.Now.AddDays(5);

            var appointment = new Appointment
            {
                Time = originalTime,
                UserId = 1,
                DoctorId = 1
            };

            await DbContext.Appointments.AddAsync(appointment);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveAppointmentsCommand
            {
                Id = appointment.Id,
                AppointmentTime = updatedTime,
                UserId = 2,
                DoctorId = 2,
                title = "Updated Appointment"
            };

            // Act
            var result = await _handler.Handle(updateCommand, CancellationToken.None);
            var updatedAppointment = await DbContext.Appointments.FindAsync(appointment.Id);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(updatedAppointment);
            Assert.Equal(updatedTime, updatedAppointment.Time);
            Assert.Equal(2, updatedAppointment.UserId);
            Assert.Equal(2, updatedAppointment.DoctorId);
        }

        [Fact]
        public async Task SaveAppointmentsCommandHandler_WithNonExistentAppointmentId_ShouldReturnError()
        {
            // Arrange
            var command = new SaveAppointmentsCommand
            {
                Id = 9999,
                AppointmentTime = DateTime.Now.AddDays(1),
                UserId = 1,
                DoctorId = 1,
                title = "Non Existent"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.HasErrors);
            Assert.Contains("Appointment not found", result.Errors);
        }

        [Fact]
        public async Task SaveAppointmentsCommandHandler_UpdateAppointmentProperties_ShouldPersistAllProperties()
        {
            // Arrange
            var originalTime = new DateTime(2025, 1, 10, 14, 0, 0);
            var updatedTime = new DateTime(2025, 1, 15, 15, 30, 0);

            var appointment = new Appointment
            {
                Time = originalTime,
                UserId = 1,
                DoctorId = 1
            };

            await DbContext.Appointments.AddAsync(appointment);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveAppointmentsCommand
            {
                Id = appointment.Id,
                AppointmentTime = updatedTime,
                UserId = 5,
                DoctorId = 3,
                title = "Modified Appointment"
            };

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);
            var result = await DbContext.Appointments.FindAsync(appointment.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedTime, result.Time);
            Assert.Equal(5, result.UserId);
            Assert.Equal(3, result.DoctorId);
        }

        [Fact]
        public async Task SaveAppointmentsCommandHandler_MultipleAppointments_ShouldHandleMultipleOperations()
        {
            // Arrange
            var command1 = new SaveAppointmentsCommand
            {
                Id = 0,
                AppointmentTime = DateTime.Now.AddDays(1),
                UserId = 1,
                DoctorId = 1,
                title = "Appointment 1"
            };

            var command2 = new SaveAppointmentsCommand
            {
                Id = 0,
                AppointmentTime = DateTime.Now.AddDays(2),
                UserId = 2,
                DoctorId = 2,
                title = "Appointment 2"
            };

            // Act
            var result1 = await _handler.Handle(command1, CancellationToken.None);
            var result2 = await _handler.Handle(command2, CancellationToken.None);
            var appointmentsCount = DbContext.Appointments.Count();

            // Assert
            Assert.False(result1.HasErrors);
            Assert.False(result2.HasErrors);
            Assert.True(appointmentsCount >= 2);
        }

        [Fact]
        public async Task SaveAppointmentsCommandHandler_WithNullAppointmentTime_ShouldPreserveExistingTime()
        {
            // Arrange
            var originalTime = new DateTime(2025, 1, 10, 14, 0, 0);

            var appointment = new Appointment
            {
                Time = originalTime,
                UserId = 1,
                DoctorId = 1
            };

            await DbContext.Appointments.AddAsync(appointment);
            await DbContext.SaveChangesAsync();

            var updateCommand = new SaveAppointmentsCommand
            {
                Id = appointment.Id,
                AppointmentTime = null,
                UserId = 2,
                DoctorId = 2,
                title = "Updated"
            };

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);
            var result = await DbContext.Appointments.FindAsync(appointment.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(originalTime, result.Time);
        }
    }
}
