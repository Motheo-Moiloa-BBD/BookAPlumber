using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAPlumber.Core.Models.Domain
{
    public class Part
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity {  get; set; }
        public Guid RepairId { get; set; }

        //Navigation Properties

        public Repair Repair { get; set; }
    }
}
