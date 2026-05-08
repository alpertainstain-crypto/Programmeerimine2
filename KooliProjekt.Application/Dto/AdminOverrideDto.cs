using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Dto
{
    internal class AdminOverrideDto
    {
        public int id { get; set; }
        public int doctorId { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string reason { get; set; }
        public bool isAdmin { get; set; }
        public string CreatedBy { get; set; }
        public string Doctor { get; set; } 
    }
}
