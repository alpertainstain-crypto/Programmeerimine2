using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Dto
{
    internal class AppointmentsDto
    {
        public int id { get; set; }
        public DateTime time { get; set; }
        public int userId { get; set; }
        public User User { get; set; }
        public int doctorId { get; set; }
        public Doctor Doctor { get; set; }

    }
}
