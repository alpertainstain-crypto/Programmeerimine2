using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KooliProjekt.Application.Dto
{
    internal class AvailabilityDto
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int doctorId { get; set; }
        [Required]
        public int DayOfWeek { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        public Date Date { get; set; }
        public bool IsException { get; set; }
        [Required]
         public Doctor Doctor { get; set; }
    }
}
