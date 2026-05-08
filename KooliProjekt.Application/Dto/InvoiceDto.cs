using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Dto
{
    internal class InvoiceDto
    {
        public int id { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime? MarkedPaidAt { get; set; }
        public string Appointment { get; set; }
        public int AppointmentId { get; set; }
        public List<InvoiceLine> Lines { get; set; }

    }
}
