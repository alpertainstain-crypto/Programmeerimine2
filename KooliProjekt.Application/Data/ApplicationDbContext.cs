using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

//        public DbSet<ToDoList> ToDoLists { get; set; }
//        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Availability> Availability { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceLine> InvoiceLine { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<AdminOverride> AdminOverride { get; set; }
        public DbSet<VisitDocument> VisitDocument { get; set; }
    }


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
        public string Name { get; set; } = default!;
        public string Specialty { get; set; } = default!;
        public List<Appointment> Appointments { get; set; } = new();
    }

    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = default!;

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = default!;
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

    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; } = default!;
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = default!;
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime? MarkedPaidAt { get; set; }

        public Appointment Appointment { get; set; } = default!;
        public int AppointmentId { get; set; }

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
