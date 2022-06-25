using System.ComponentModel.DataAnnotations;

namespace MobileStore.Domain.Entities
{
    public class Person:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Phone { get; set; }
    }
}
