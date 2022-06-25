using System.ComponentModel.DataAnnotations;


namespace MobileStore.Domain.Entities
{
    public class Smartphone:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
