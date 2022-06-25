using System.ComponentModel.DataAnnotations;

namespace MobileStore.Domain.Entities
{
    public class Ordering:BaseEntity
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
