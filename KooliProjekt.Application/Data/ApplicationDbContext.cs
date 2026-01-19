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
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Availability> Availability { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<AdminOverride> AdminOverride { get; set; }
        public DbSet<VisitDocument> VisitDocuments { get; set; }
    }
}
