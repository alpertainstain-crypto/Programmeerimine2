using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; } = default!;
            public string Email { get; set; } = default!;
            public string Phone { get; set; } = default!;
            public string PasswordHash { get; set; } = default!;
            public string Role { get; set; } = default!;
            public DateTime CreatedAt { get; set; }

            public List<Appointment> Appointments { get; set; } = new();
        }

        public class Doctor
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string Specialization { get; set; } = default!;
            public TimeSpan WorkStart { get; set; }
            public TimeSpan WorkEnd { get; set; }

            public User User { get; set; } = default!;
            public List<Availability> Availabilities { get; set; } = new();
            public List<AdminOverride> AdminOverrides { get; set; } = new();
        }

        public class Availability
        {
            public int Id { get; set; }
            public int DoctorId { get; set; }
            public int DayOfWeek { get; set; }

            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }

            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }

            public bool IsException { get; set; }

            public Doctor Doctor { get; set; } = default!;
        }

        public class Appointment
        {
            public int Id { get; set; }
            public int DoctorId { get; set; }
            public int PatientId { get; set; }

            public DateTime StartAt { get; set; }
            public DateTime EndAt { get; set; }

            public string Status { get; set; } = default!;
            public int? CancelledBy { get; set; }
            public DateTime? CancelledAt { get; set; }

            public Doctor Doctor { get; set; } = default!;
            public User Patient { get; set; } = default!;
            public List<Invoice> Invoices { get; set; } = new();
            public List<VisitDocument> Documents { get; set; } = new();
        }

        public class Invoice
        {
            public int Id { get; set; }
            public string InvoiceNo { get; set; } = default!;
            public int AppointmentId { get; set; }
            public int CustomerId { get; set; }

            public DateTime InvoiceDate { get; set; }
            public DateTime DueDate { get; set; }

            public string Status { get; set; } = default!;
            public decimal Subtotal { get; set; }
            public decimal Discount { get; set; }
            public decimal GrandTotal { get; set; }
            public DateTime? MarkedPaidAt { get; set; }

            public Appointment Appointment { get; set; } = default!;
            public User Customer { get; set; } = default!;
            public List<InvoiceLine> Lines { get; set; } = new();
        }

        public class InvoiceLine
        {
            public int Id { get; set; }
            public int InvoiceId { get; set; }

            public string Description { get; set; } = default!;
            public decimal Amount { get; set; }

            public Invoice Invoice { get; set; } = default!;
        }

        public class Service
        {
            public int Id { get; set; }
            public string Code { get; set; } = default!;
            public string Description { get; set; } = default!;
            public decimal UnitPrice { get; set; }
        }

        public class AdminOverride
        {
            public int Id { get; set; }
            public int DoctorId { get; set; }

            public DateTime Start { get; set; }
            public DateTime End { get; set; }

            public string Reason { get; set; } = default!;
            public int CreatedBy { get; set; }

            public Doctor Doctor { get; set; } = default!;
        }

        public class VisitDocument
        {
            public int Id { get; set; }
            public int AppointmentId { get; set; }

            public string FilePath { get; set; } = default!;
            public string FileType { get; set; } = default!;
            public int UploadedBy { get; set; }
            public DateTime CreatedAt { get; set; }

            public Appointment Appointment { get; set; } = default!;
        }
    }
}
