using System.ComponentModel.DataAnnotations;

namespace BookAPlumber.Core.Models.Domain
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }

        //Navigation Properties
        //public User User { get; set; }
        public ICollection<Repair> Repairs { get; set; }
    }
}
