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
                new Doctor { Name = "Dr. Jaan Tamm", Specialty = "Kardioloogia" },
                new Doctor { Name = "Dr. Kaarina Saar", Specialty = "Pädiaatria" },
                new Doctor { Name = "Dr. Martin Ots", Specialty = "Ortopeedika" },
                new Doctor { Name = "Dr. Liisa Kask", Specialty = "Dermatoloogia" },
                new Doctor { Name = "Dr. Rein Vaher", Specialty = "Oftalmoloogia" },
                new Doctor { Name = "Dr. Erika Roosild", Specialty = "Psühhiaatria" },
                new Doctor { Name = "Dr. Andres Koppel", Specialty = "Kirurgia" },
                new Doctor { Name = "Dr. Kristel Sillari", Specialty = "Neuropatoloogia" },
                new Doctor { Name = "Dr. Toomas Lepp", Specialty = "Gastroenteroloogia" },
                new Doctor { Name = "Dr. Anne Sikk", Specialty = "Pulmonoloogia" },
                new Doctor { Name = "Dr. Mihkel Ratassepp", Specialty = "Onkoloogia" },
                new Doctor { Name = "Dr. Kaarina Pärna", Specialty = "Urioloogia" }
            };
            context.Doctors.AddRange(doctors);
            context.SaveChanges();

            // Generate Users (10+)
            var users = new List<User>
            {
                new User 
                { 
                    Name = "Peeter Sepp", 
                    Email = "peeter.sepp@email.com", 
                    Phone = "+3725551234", 
                    PasswordHash = "hash123", 
                    Role = "Patient",
                    CreatedAt = DateTime.Now.AddDays(-30)
                },
                new User 
                { 
                    Name = "Mari Järv", 
                    Email = "mari.jarv@email.com", 
                    Phone = "+3725551235", 
                    PasswordHash = "hash124", 
                    Role = "Patient",
                    CreatedAt = DateTime.Now.AddDays(-25)
                },
                new User 
                { 
                    Name = "Jüri Kask", 
                    Email = "juri.kask@email.com", 
                    Phone = "+3725551236", 
                    PasswordHash = "hash125", 
                    Role = "Patient",
                    CreatedAt = DateTime.Now.AddDays(-20)
                },
                new User 
                { 
                    Name = "Krista Mänd", 
                    Email = "krista.mand@email.com", 
                    Phone = "+3725551237", 
                    PasswordHash = "hash126", 
                    Role = "Patient",
                    CreatedAt = DateTime.Now.AddDays(-15)
                },
                new User 
                { 
                    Name = "Rein Lepp", 
                    Email = "rein.lepp@email.com", 
                    Phone = "+3725551238", 
                    PasswordHash = "hash127", 
                    Role = "Admin",
                    CreatedAt = DateTime.Now.AddDays(-10)
                },
                new User 
                { 
                    Name = "Liina Vaher", 
                    Email = "liina.vaher@email.com", 
                    Phone = "+3725551239", 
                    PasswordHash = "hash128", 
                    Role = "Patient",
                    CreatedAt = DateTime.Now.AddDays(-5)
                },
                new User 
                { 
                    Name = "Andres Sillari", 
                    Email = "andres.sillari@email.com", 
                    Phone = "+3725551240", 
                    PasswordHash = "hash129", 
                    Role = "Doctor",
                    CreatedAt = DateTime.Now.AddDays(-3)
                },
                new User 
                { 
                    Name = "Eve Rossi", 
                    Email = "eve.rossi@email.com", 
                    Phone = "+3725551241", 
                    PasswordHash = "hash130", 
                    Role = "Patient",
                    CreatedAt = DateTime.Now.AddDays(-1)
                },
                new User 
                { 
                    Name = "Toomas Kiik", 
                    Email = "toomas.kiik@email.com", 
                    Phone = "+3725551242", 
                    PasswordHash = "hash131", 
                    Role = "Patient",
                    CreatedAt = DateTime.Now
                },
                new User 
                { 
                    Name = "Kadri Saar", 
                    Email = "kadri.saar@email.com", 
                    Phone = "+3725551243", 
                    PasswordHash = "hash132", 
                    Role = "Nurse",
                    CreatedAt = DateTime.Now
                },
                new User 
                { 
                    Name = "Mikk Oja", 
                    Email = "mikk.oja@email.com", 
                    Phone = "+3725551244", 
                    PasswordHash = "hash133", 
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
                new Appointment { Time = DateTime.Now.AddDays(1).Date.AddHours(9), UserId = users[0].Id, DoctorId = doctors[0].Id },
                new Appointment { Time = DateTime.Now.AddDays(1).Date.AddHours(10), UserId = users[1].Id, DoctorId = doctors[1].Id },
                new Appointment { Time = DateTime.Now.AddDays(2).Date.AddHours(11), UserId = users[2].Id, DoctorId = doctors[2].Id },
                new Appointment { Time = DateTime.Now.AddDays(2).Date.AddHours(14), UserId = users[3].Id, DoctorId = doctors[3].Id },
                new Appointment { Time = DateTime.Now.AddDays(3).Date.AddHours(9), UserId = users[4].Id, DoctorId = doctors[4].Id },
                new Appointment { Time = DateTime.Now.AddDays(3).Date.AddHours(15), UserId = users[5].Id, DoctorId = doctors[5].Id },
                new Appointment { Time = DateTime.Now.AddDays(4).Date.AddHours(10), UserId = users[6].Id, DoctorId = doctors[6].Id },
                new Appointment { Time = DateTime.Now.AddDays(4).Date.AddHours(13), UserId = users[7].Id, DoctorId = doctors[7].Id },
                new Appointment { Time = DateTime.Now.AddDays(5).Date.AddHours(9), UserId = users[8].Id, DoctorId = doctors[8].Id },
                new Appointment { Time = DateTime.Now.AddDays(5).Date.AddHours(14), UserId = users[9].Id, DoctorId = doctors[9].Id },
                new Appointment { Time = DateTime.Now.AddDays(6).Date.AddHours(11), UserId = users[10].Id, DoctorId = doctors[10].Id }
            };
            context.Appointments.AddRange(appointments);
            context.SaveChanges();

            // Generate Availability (10+)
            var availability = new List<Availability>
            {
                new Availability { DoctorId = doctors[0].Id, DayOfWeek = 1, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), Date = DateTime.Now.AddDays(1), IsException = false },
                new Availability { DoctorId = doctors[0].Id, DayOfWeek = 2, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), Date = DateTime.Now.AddDays(2), IsException = false },
                new Availability { DoctorId = doctors[1].Id, DayOfWeek = 1, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0), Date = DateTime.Now.AddDays(1), IsException = false },
                new Availability { DoctorId = doctors[1].Id, DayOfWeek = 3, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0), Date = DateTime.Now.AddDays(3), IsException = false },
                new Availability { DoctorId = doctors[2].Id, DayOfWeek = 2, StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(16, 0, 0), Date = DateTime.Now.AddDays(2), IsException = false },
                new Availability { DoctorId = doctors[2].Id, DayOfWeek = 4, StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(16, 0, 0), Date = DateTime.Now.AddDays(4), IsException = false },
                new Availability { DoctorId = doctors[3].Id, DayOfWeek = 1, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), Date = DateTime.Now.AddDays(1), IsException = false },
                new Availability { DoctorId = doctors[4].Id, DayOfWeek = 3, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), Date = DateTime.Now.AddDays(3), IsException = false },
                new Availability { DoctorId = doctors[5].Id, DayOfWeek = 2, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0), Date = DateTime.Now.AddDays(2), IsException = false },
                new Availability { DoctorId = doctors[6].Id, DayOfWeek = 4, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), Date = DateTime.Now.AddDays(4), IsException = false },
                new Availability { DoctorId = doctors[7].Id, DayOfWeek = 1, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0), Date = DateTime.Now.AddDays(1), IsException = false }
            };
            context.Availability.AddRange(availability);
            context.SaveChanges();

            // Generate Invoices and InvoiceLines (10+ each)
            var invoices = new List<Invoice>
            {
                new Invoice 
                { 
                    InvoiceNo = "INV001", 
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
                    InvoiceNo = "INV002", 
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
                    InvoiceNo = "INV003", 
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
                    InvoiceNo = "INV004", 
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
                    InvoiceNo = "INV005", 
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
                    InvoiceNo = "INV006", 
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
                    InvoiceNo = "INV007", 
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
                    InvoiceNo = "INV008", 
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
                    InvoiceNo = "INV009", 
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
                    InvoiceNo = "INV010", 
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
                    DoctorId = doctors[0].Id, 
                    Start = DateTime.Now.AddDays(1).Date.AddHours(12), 
                    End = DateTime.Now.AddDays(1).Date.AddHours(14), 
                    Reason = "Kohtumine",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[1].Id, 
                    Start = DateTime.Now.AddDays(2).Date.AddHours(13), 
                    End = DateTime.Now.AddDays(2).Date.AddHours(15), 
                    Reason = "Koolitamine",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[2].Id, 
                    Start = DateTime.Now.AddDays(3).Date.AddHours(11), 
                    End = DateTime.Now.AddDays(3).Date.AddHours(12), 
                    Reason = "Haiguspäev",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[3].Id, 
                    Start = DateTime.Now.AddDays(4).Date.AddHours(9), 
                    End = DateTime.Now.AddDays(4).Date.AddHours(17), 
                    Reason = "Puhkus",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[4].Id, 
                    Start = DateTime.Now.AddDays(5).Date.AddHours(12), 
                    End = DateTime.Now.AddDays(5).Date.AddHours(13), 
                    Reason = "Lunch",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[5].Id, 
                    Start = DateTime.Now.AddDays(6).Date.AddHours(14), 
                    End = DateTime.Now.AddDays(6).Date.AddHours(16), 
                    Reason = "Operatsiooniruumi ettevalmistus",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[6].Id, 
                    Start = DateTime.Now.AddDays(7).Date.AddHours(10), 
                    End = DateTime.Now.AddDays(7).Date.AddHours(11), 
                    Reason = "Administratiivne töö",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[7].Id, 
                    Start = DateTime.Now.AddDays(8).Date.AddHours(9), 
                    End = DateTime.Now.AddDays(8).Date.AddHours(17), 
                    Reason = "Konverents",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[8].Id, 
                    Start = DateTime.Now.AddDays(9).Date.AddHours(13), 
                    End = DateTime.Now.AddDays(9).Date.AddHours(14), 
                    Reason = "Väike vahe",
                    CreatedBy = users[4].Id
                },
                new AdminOverride 
                { 
                    DoctorId = doctors[9].Id, 
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
                new VisitDocument { AppointmentId = appointments[0].Id, FilePath = "/docs/visit_001.pdf", FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-15) },
                new VisitDocument { AppointmentId = appointments[1].Id, FilePath = "/docs/visit_002.pdf", FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-14) },
                new VisitDocument { AppointmentId = appointments[2].Id, FilePath = "/docs/visit_003.pdf", FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-13) },
                new VisitDocument { AppointmentId = appointments[3].Id, FilePath = "/docs/xray_001.jpg", FileType = "JPG", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-12) },
                new VisitDocument { AppointmentId = appointments[4].Id, FilePath = "/docs/visit_004.pdf", FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-11) },
                new VisitDocument { AppointmentId = appointments[5].Id, FilePath = "/docs/lab_results.pdf", FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-10) },
                new VisitDocument { AppointmentId = appointments[6].Id, FilePath = "/docs/ultrasound.jpg", FileType = "JPG", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-9) },
                new VisitDocument { AppointmentId = appointments[7].Id, FilePath = "/docs/visit_005.pdf", FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-8) },
                new VisitDocument { AppointmentId = appointments[8].Id, FilePath = "/docs/visit_006.pdf", FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-7) },
                new VisitDocument { AppointmentId = appointments[9].Id, FilePath = "/docs/visit_007.pdf", FileType = "PDF", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-6) },
                new VisitDocument { AppointmentId = appointments[10].Id, FilePath = "/docs/xray_002.jpg", FileType = "JPG", UploadedBy = users[4].Id, CreatedAt = DateTime.Now.AddDays(-5) }
            };
            context.VisitDocuments.AddRange(visitDocuments);
            context.SaveChanges();
        }
    }
}
