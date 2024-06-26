﻿using System.ComponentModel.DataAnnotations;

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
