using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            // Clear existing data
            context.Doctors.RemoveRange(context.Doctors);
            context.Users.RemoveRange(context.Users);
            context.Services.RemoveRange(context.Services);
            context.Availability.RemoveRange(context.Availability);
            context.Appointments.RemoveRange(context.Appointments);
            context.AdminOverride.RemoveRange(context.AdminOverride);
            context.VisitDocuments.RemoveRange(context.VisitDocuments);
            context.InvoiceLines.RemoveRange(context.InvoiceLines);
            context.Invoice.RemoveRange(context.Invoice);

            // Generate Doctors (10+)
            var doctors = new List<Doctor>
            {
                new Doctor { FirstName = "Dr. Jaan ", LastName = "Tamm", Specialty = "Kardioloogia" },
                new Doctor { FirstName = "Dr. Kaarina ", LastName = "Saar", Specialty = "Pädiaatria" },
                new Doctor { FirstName = "Dr. Martin ", LastName = "Ots", Specialty = "Ortopeedika" },
                new Doctor { FirstName = "Dr. Liisa ", LastName = "Kask", Specialty = "Dermatoloogia" },
                new Doctor { FirstName = "Dr. Rein ", LastName = "Vaher", Specialty = "Oftalmoloogia" },
                new Doctor { FirstName = "Dr. Erika ", LastName = "Roosild", Specialty = "Psühhiaatria" },
                new Doctor { FirstName = "Dr. Andres ", LastName = "Koppel", Specialty = "Kirurgia" },
                new Doctor { FirstName = "Dr. Kristel ", LastName = "Sillari", Specialty = "Neuropatoloogia" },
                new Doctor { FirstName = "Dr. Toomas ", LastName = "Lepp", Specialty = "Gastroenteroloogia" },
                new Doctor { FirstName = "Dr. Anne ", LastName = "Sikk", Specialty = "Pulmonoloogia" },
                new Doctor { FirstName = "Dr. Mihkel ", LastName = "Ratassepp", Specialty = "Onkoloogia" },
                new Doctor { FirstName = "Dr. Kaarina ", LastName = "Pärna", Specialty = "Urioloogia" }
            };
            context.Doctors.AddRange(doctors);
            context.SaveChanges();

            // Generate Users (10+)
            var users = new List<User>
            {
                new User 
                { 
                    FirstName = "Peeter ",
                    LastName = "Sepp",
                    Email = "peeter.sepp@email.com", 
                    Phone = "+3725551234",  
                    Role = "Patient",
                    CreatedAt = DateTime.Now.AddDays(-30)
                },
                new User 
                { 
                    FirstName = "Mari ",
                    LastName = "Järv",
                    Email = "mari.jarv@email.com", 
                    Phone = "+3725551235",  
                    Role = "Patient",
                    CreatedAt = DateTime.Now.AddDays(-25)
                },
                new User 
                { 
                    FirstName = "Jüri ",
                    LastName = "Kask",
                    Email = "juri.kask@email.com", 
                    Phone = "+3725551236",  
                    Role = "Patient",
                    CreatedAt = DateTime.Now.AddDays(-20)
                },
                new User 
                { 
                    FirstName = "Krista ",
                    LastName = "Mand",
                    Email = "krista.mand@email.com", 
                    Phone = "+3725551237", 
                    Role = "Patient",
                    CreatedAt = DateTime.Now.AddDays(-15)
                },
                new User 
                { 
                    FirstName = "Rein",
                    LastName = "Lepp", 
                    Email = "rein.lepp@email.com", 
                    Phone = "+3725551238", 
                    Role = "Admin",
                    CreatedAt = DateTime.Now.AddDays(-10)
                },
                new User 
                { 
                    FirstName = "Liina",
                    LastName = "Vaher", 
                    Email = "liina.vaher@email.com", 
                    Phone = "+3725551239", 
                    Role = "Patient",
                    CreatedAt = DateTime.Now.AddDays(-5)
                },
                new User 
                { 
                    FirstName = "Andres",
                    LastName = "Sillari", 
                    Email = "andres.sillari@email.com", 
                    Phone = "+3725551240", 
                    Role = "Doctor",
                    CreatedAt = DateTime.Now.AddDays(-3)
                },
                new User 
                { 
                    FirstName = "Eve",
                    LastName = "Rossi", 
                    Email = "eve.rossi@email.com", 
                    Phone = "+3725551241", 
                    Role = "Patient",
                    CreatedAt = DateTime.Now.AddDays(-1)
                },
                new User 
                { 
                    FirstName = "Toomas",
                    LastName = "Kiik", 
                    Email = "toomas.kiik@email.com", 
                    Phone = "+3725551242", 
                    Role = "Patient",
                    CreatedAt = DateTime.Now
                },
                new User 
                { 
                    FirstName = "Kadri",
                    LastName = "Saar", 
                    Email = "kadri.saar@email.com", 
                    Phone = "+3725551243", 
                    Role = "Nurse",
                    CreatedAt = DateTime.Now
                },
                new User 
                { 
                    FirstName = "Mikk",
                    LastName = "Oja", 
                    Email = "mikk.oja@email.com", 
                    Phone = "+3725551244", 
                    Role = "Patient",
                    CreatedAt = DateTime.Now
                }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            // Generate Services (10+)
            var services = new List<Service>
            {
                new Service { Code = "SVC001", Description = "Üldkonsultatsioon", UnitPrice = 50m },
                new Service { Code = "SVC002", Description = "EKG uuring", UnitPrice = 75m },
                new Service { Code = "SVC003", Description = "Vererõhu mõõtmine", UnitPrice = 20m },
                new Service { Code = "SVC004", Description = "Raadiograafia", UnitPrice = 100m },
                new Service { Code = "SVC005", Description = "Ultraheli uuring", UnitPrice = 120m },
                new Service { Code = "SVC006", Description = "Laboratoorne analüüs", UnitPrice = 60m },
                new Service { Code = "SVC007", Description = "Vaktsineerimine", UnitPrice = 40m },
                new Service { Code = "SVC008", Description = "Kardioloogiline konsultatsion", UnitPrice = 90m },
                new Service { Code = "SVC009", Description = "Dermatoloogiline uuring", UnitPrice = 85m },
                new Service { Code = "SVC010", Description = "Psühholoogiline nõustamine", UnitPrice = 80m },
                new Service { Code = "SVC011", Description = "Füsioteraapia seanss", UnitPrice = 55m },
                new Service { Code = "SVC012", Description = "Hambaraavi", UnitPrice = 110m }
            };
            context.Services.AddRange(services);
            context.SaveChanges();

            // Generate Appointments (10+)
            var appointments = new List<Appointment>
            {
                new Appointment { Time = DateTime.Now.AddDays(1).Date.AddHours(9), UserId = users[0].Id, DoctorId = doctors[0].DoctorId },
                new Appointment { Time = DateTime.Now.AddDays(1).Date.AddHours(10), UserId = users[1].Id, DoctorId = doctors[1].DoctorId },
                new Appointment { Time = DateTime.Now.AddDays(2).Date.AddHours(11), UserId = users[2].Id, DoctorId = doctors[2].DoctorId },
                new Appointment { Time = DateTime.Now.AddDays(2).Date.AddHours(14), UserId = users[3].Id, DoctorId = doctors[3].DoctorId },
                new Appointment { Time = DateTime.Now.AddDays(3).Date.AddHours(9), UserId = users[4].Id, DoctorId = doctors[4].DoctorId },
                new Appointment { Time = DateTime.Now.AddDays(3).Date.AddHours(15), UserId = users[5].Id, DoctorId = doctors[5].DoctorId },
                new Appointment { Time = DateTime.Now.AddDays(4).Date.AddHours(10), UserId = users[6].Id, DoctorId = doctors[6].DoctorId },
                new Appointment { Time = DateTime.Now.AddDays(4).Date.AddHours(13), UserId = users[7].Id, DoctorId = doctors[7].DoctorId },
                new Appointment { Time = DateTime.Now.AddDays(5).Date.AddHours(9), UserId = users[8].Id, DoctorId = doctors[8].DoctorId },
                new Appointment { Time = DateTime.Now.AddDays(5).Date.AddHours(14), UserId = users[9].Id, DoctorId = doctors[9].DoctorId },
                new Appointment { Time = DateTime.Now.AddDays(6).Date.AddHours(11), UserId = users[10].Id, DoctorId = doctors[10].DoctorId }
            };
            context.Appointments.AddRange(appointments);
            context.SaveChanges();

            // Generate Availability (10+)
            var availability = new List<Availability>
            {
                new Availability { DoctorId = doctors[0].DoctorId, DayOfWeek = 1, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsException = false },
                new Availability { DoctorId = doctors[0].DoctorId, DayOfWeek = 2, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsException = false },
                new Availability { DoctorId = doctors[1].DoctorId, DayOfWeek = 1, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0), IsException = false },
                new Availability { DoctorId = doctors[1].DoctorId, DayOfWeek = 3, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0), IsException = false },
                new Availability { DoctorId = doctors[2].DoctorId, DayOfWeek = 2, StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(16, 0, 0), IsException = false },
                new Availability { DoctorId = doctors[2].DoctorId, DayOfWeek = 4, StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(16, 0, 0), IsException = false },
                new Availability { DoctorId = doctors[3].DoctorId, DayOfWeek = 1, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsException = false },
                new Availability { DoctorId = doctors[4].DoctorId, DayOfWeek = 3, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsException = false },
                new Availability { DoctorId = doctors[5].DoctorId, DayOfWeek = 2, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0), IsException = false },
                new Availability { DoctorId = doctors[6].DoctorId, DayOfWeek = 4, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsException = false },
                new Availability { DoctorId = doctors[7].DoctorId, DayOfWeek = 1, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0), IsException = false }
            };
            context.Availability.AddRange(availability);
            context.SaveChanges();

            // Generate Invoices and InvoiceLines (10+ each)
            var invoices = new List<Invoice>
            {
                new Invoice 
                { 
                    InvoiceNo = 1, 
                    InvoiceDate = DateTime.Now.AddDays(-20), 
                    DueDate = DateTime.Now.AddDays(10), 
                    Status = "Issued",
                    Subtotal = 200m,
                    Discount = 20m,
                    GrandTotal = 180m,
                    AppointmentId = appointments[0].Id
                },
                new Invoice 
                { 
                    InvoiceNo = 2, 
                    InvoiceDate = DateTime.Now.AddDays(-15), 
                    DueDate = DateTime.Now.AddDays(15), 
                    Status = "Issued",
                    Subtotal = 150m,
                    Discount = 0m,
                    GrandTotal = 150m,
                    AppointmentId = appointments[1].Id
                },
                new Invoice 
                { 
                    InvoiceNo = 3, 
                    InvoiceDate = DateTime.Now.AddDays(-10), 
                    DueDate = DateTime.Now.AddDays(20), 
                    Status = "Paid",
                    Subtotal = 300m,
                    Discount = 30m,
                    GrandTotal = 270m,
                    MarkedPaidAt = DateTime.Now,
                    AppointmentId = appointments[2].Id
                },
                new Invoice 
                { 
                    InvoiceNo = 4, 
                    InvoiceDate = DateTime.Now.AddDays(-5), 
                    DueDate = DateTime.Now.AddDays(25), 
                    Status = "Issued",
                    Subtotal = 100m,
                    Discount = 10m,
                    GrandTotal = 90m,
                    AppointmentId = appointments[3].Id
                },
                new Invoice 
                { 
                    InvoiceNo = 5, 
                    InvoiceDate = DateTime.Now.AddDays(-3), 
                    DueDate = DateTime.Now.AddDays(27), 
                    Status = "Issued",
                    Subtotal = 250m,
                    Discount = 0m,
                    GrandTotal = 250m,
                    AppointmentId = appointments[4].Id
                },
                new Invoice 
                { 
                    InvoiceNo = 6, 
                    InvoiceDate = DateTime.Now.AddDays(-2), 
                    DueDate = DateTime.Now.AddDays(28), 
                    Status = "Overdue",
                    Subtotal = 175m,
                    Discount = 17.5m,
                    GrandTotal = 157.5m,
                    AppointmentId = appointments[5].Id
                },
                new Invoice 
                { 
                    InvoiceNo = 7, 
                    InvoiceDate = DateTime.Now.AddDays(-1), 
                    DueDate = DateTime.Now.AddDays(29), 
                    Status = "Issued",
                    Subtotal = 220m,
                    Discount = 0m,
                    GrandTotal = 220m,
                    AppointmentId = appointments[6].Id
                },
                new Invoice 
                { 
                    InvoiceNo = 8, 
                    InvoiceDate = DateTime.Now, 
                    DueDate = DateTime.Now.AddDays(30), 
                    Status = "Issued",
                    Subtotal = 280m,
                    Discount = 28m,
                    GrandTotal = 252m,
                    AppointmentId = appointments[7].Id
                },
                new Invoice 
                { 
                    InvoiceNo = 9, 
                    InvoiceDate = DateTime.Now, 
                    DueDate = DateTime.Now.AddDays(30), 
                    Status = "Draft",
                    Subtotal = 95m,
                    Discount = 0m,
                    GrandTotal = 95m,
                    AppointmentId = appointments[8].Id
                },
                new Invoice 
                { 
                    InvoiceNo = 10, 
                    InvoiceDate = DateTime.Now, 
                    DueDate = DateTime.Now.AddDays(30), 
                    Status = "Issued",
                    Subtotal = 345m,
                    Discount = 34.5m,
                    GrandTotal = 310.5m,
                    AppointmentId = appointments[9].Id
                }
            };
            context.Invoice.AddRange(invoices);
            context.SaveChanges();

            // Generate InvoiceLines
            var invoiceLines = new List<InvoiceLine>
            {
                new InvoiceLine { InvoiceId = invoices[0].Id, Description = "Üldkonsultatsioon", Amount = 50m },
                new InvoiceLine { InvoiceId = invoices[0].Id, Description = "EKG uuring", Amount = 75m },
                new InvoiceLine { InvoiceId = invoices[0].Id, Description = "Laboratoorne analüüs", Amount = 75m },
                new InvoiceLine { InvoiceId = invoices[1].Id, Description = "Vererõhu mõõtmine", Amount = 20m },
                new InvoiceLine { InvoiceId = invoices[1].Id, Description = "Raadiograafia", Amount = 100m },
                new InvoiceLine { InvoiceId = invoices[1].Id, Description = "Vaktsineerimine", Amount = 30m },
                new InvoiceLine { InvoiceId = invoices[2].Id, Description = "Kardioloogiline konsultatsion", Amount = 90m },
                new InvoiceLine { InvoiceId = invoices[2].Id, Description = "Ultraheli uuring", Amount = 120m },
                new InvoiceLine { InvoiceId = invoices[2].Id, Description = "Kartogramm", Amount = 90m },
                new InvoiceLine { InvoiceId = invoices[3].Id, Description = "Dermatoloogiline uuring", Amount = 85m },
                new InvoiceLine { InvoiceId = invoices[3].Id, Description = "Psühholoogiline nõustamine", Amount = 15m },
                new InvoiceLine { InvoiceId = invoices[4].Id, Description = "Üldkonsultatsioon", Amount = 50m },
                new InvoiceLine { InvoiceId = invoices[4].Id, Description = "Füsioteraapia seanss", Amount = 55m },
                new InvoiceLine { InvoiceId = invoices[4].Id, Description = "Laboranalüüs", Amount = 60m },
                new InvoiceLine { InvoiceId = invoices[4].Id, Description = "Hambaraavi", Amount = 85m },
                new InvoiceLine { InvoiceId = invoices[5].Id, Description = "Üldkonsultatsioon", Amount = 50m },
                new InvoiceLine { InvoiceId = invoices[5].Id, Description = "Raadiograafia", Amount = 100m },
                new InvoiceLine { InvoiceId = invoices[5].Id, Description = "Ultraheli uuring", Amount = 25m },
                new InvoiceLine { InvoiceId = invoices[6].Id, Description = "Üldkonsultatsioon", Amount = 50m },
                new InvoiceLine { InvoiceId = invoices[6].Id, Description = "EKG uuring", Amount = 75m },
                new InvoiceLine { InvoiceId = invoices[6].Id, Description = "Laboratoorne analüüs", Amount = 95m },
                new InvoiceLine { InvoiceId = invoices[7].Id, Description = "Kardioloogiline konsultatsion", Amount = 90m },
                new InvoiceLine { InvoiceId = invoices[7].Id, Description = "Ultraheli uuring", Amount = 120m },
                new InvoiceLine { InvoiceId = invoices[7].Id, Description = "Dermatoloogiline uuring", Amount = 70m },
                new InvoiceLine { InvoiceId = invoices[8].Id, Description = "Vaktsineerimine", Amount = 95m },
                new InvoiceLine { InvoiceId = invoices[9].Id, Description = "Üldkonsultatsioon", Amount = 50m },
                new InvoiceLine { InvoiceId = invoices[9].Id, Description = "Füsioteraapia seanss", Amount = 55m },
                new InvoiceLine { InvoiceId = invoices[9].Id, Description = "Laboranalüüs", Amount = 60m },
                new InvoiceLine { InvoiceId = invoices[9].Id, Description = "Hambaraavi", Amount = 110m },
                new InvoiceLine { InvoiceId = invoices[9].Id, Description = "Psühholoogiline nõustamine", Amount = 25.5m }
            };
            context.InvoiceLines.AddRange(invoiceLines);
            context.SaveChanges();

            // Generate AdminOverrides (10+)
            var adminOverrides = new List<AdminOverride>
            {
                new AdminOverride 
                { 
                    DoctorId = doctors[0].DoctorId, 
                    Start = DateTime.Now.AddDays(1).Date.AddHours(12), 
                    End = DateTime.Now.AddDays(1).Date.AddHours(14), 
                    Reason = "Kohtumine",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[1].DoctorId, 
                    Start = DateTime.Now.AddDays(2).Date.AddHours(13), 
                    End = DateTime.Now.AddDays(2).Date.AddHours(15), 
                    Reason = "Koolitamine",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[2].DoctorId, 
                    Start = DateTime.Now.AddDays(3).Date.AddHours(11), 
                    End = DateTime.Now.AddDays(3).Date.AddHours(12), 
                    Reason = "Haiguspäev",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[3].DoctorId, 
                    Start = DateTime.Now.AddDays(4).Date.AddHours(9), 
                    End = DateTime.Now.AddDays(4).Date.AddHours(17), 
                    Reason = "Puhkus",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[4].DoctorId, 
                    Start = DateTime.Now.AddDays(5).Date.AddHours(12), 
                    End = DateTime.Now.AddDays(5).Date.AddHours(13), 
                    Reason = "Lunch",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[5].DoctorId, 
                    Start = DateTime.Now.AddDays(6).Date.AddHours(14), 
                    End = DateTime.Now.AddDays(6).Date.AddHours(16), 
                    Reason = "Operatsiooniruumi ettevalmistus",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[6].DoctorId, 
                    Start = DateTime.Now.AddDays(7).Date.AddHours(10), 
                    End = DateTime.Now.AddDays(7).Date.AddHours(11), 
                    Reason = "Administratiivne töö",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[7].DoctorId, 
                    Start = DateTime.Now.AddDays(8).Date.AddHours(9), 
                    End = DateTime.Now.AddDays(8).Date.AddHours(17), 
                    Reason = "Konverents",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[8].DoctorId, 
                    Start = DateTime.Now.AddDays(9).Date.AddHours(13), 
                    End = DateTime.Now.AddDays(9).Date.AddHours(14), 
                    Reason = "Väike vahe",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[9].DoctorId, 
                    Start = DateTime.Now.AddDays(10).Date.AddHours(9), 
                    End = DateTime.Now.AddDays(10).Date.AddHours(17), 
                    Reason = "Sünnipäev",
                    CreatedBy = users[4].Id
                }
            };
            context.AdminOverride.AddRange(adminOverrides);
            context.SaveChanges();

            // Generate VisitDocuments (10+)
            var visitDocuments = new List<VisitDocument>
            {
                new VisitDocument { AppointmentId = appointments[0].Id, FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-15) },
                new VisitDocument { AppointmentId = appointments[1].Id, FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-14) },
                new VisitDocument { AppointmentId = appointments[2].Id, FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-13) },
                new VisitDocument { AppointmentId = appointments[3].Id, FileType = "JPG", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-12) },
                new VisitDocument { AppointmentId = appointments[4].Id, FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-11) },
                new VisitDocument { AppointmentId = appointments[5].Id, FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-10) },
                new VisitDocument { AppointmentId = appointments[6].Id, FileType = "JPG", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-9) },
                new VisitDocument { AppointmentId = appointments[7].Id, FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-8) },
                new VisitDocument { AppointmentId = appointments[8].Id, FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-7) },
                new VisitDocument { AppointmentId = appointments[9].Id, FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-6) },
                new VisitDocument { AppointmentId = appointments[10].Id, FileType = "JPG", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-5) }
            };
            context.VisitDocuments.AddRange(visitDocuments);
            context.SaveChanges();
        }
    }
}
