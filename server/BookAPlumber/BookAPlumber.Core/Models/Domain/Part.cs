using System.ComponentModel.DataAnnotations;

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
