using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Dto
{
    internal class InvoiceLineDto
    {
        public int id { get; set; }
        public int InvoiceId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Invoice invoice { get; set; }
    }
}
