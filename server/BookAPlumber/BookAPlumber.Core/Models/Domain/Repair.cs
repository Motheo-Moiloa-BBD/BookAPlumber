using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAPlumber.Core.Models.Domain
{
    public class Repair
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid BookingId { get; set; }
        
        //Navigation Properties
        public Booking Booking { get; set; }
    }
}
