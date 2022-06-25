
namespace MobileStore.Domain.Entities
{
    public class CartItem:BaseEntity
    {
        public int Quantity { get; set; }
        public Smartphone Smartphone { get; set; }

    }
}
