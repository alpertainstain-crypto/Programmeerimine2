using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Dto
{
    internal class VisiteDocumentDto
    {
        public int id { get; set; }
        public int appointmentId { get; set; }
        public string FileType { get; set; }
        public int UploadedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Appointment> Appointment { get; set; }
    }
}
