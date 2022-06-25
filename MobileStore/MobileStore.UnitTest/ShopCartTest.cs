using MobileStore.Domain.Entities;
using System.Linq;
using Xunit;

namespace MobileStore.UnitTest
{
    public class ShopCartTest
    {
        Smartphone smartphone1 = new Smartphone { Id = 0, Name = "Iphone", Brand = "Apple", Price = 300 };
        Smartphone smartphone2 = new Smartphone { Id = 1, Name = "Nokia", Brand = "Nokia", Price = 200 };

        [Fact]
        public void Add_New_Lines()
        {
       
            Cart cart = new Cart();

            cart.AddItem(smartphone1,1);
            cart.AddItem(smartphone2, 2);
            var results = cart.Lines.ToList();

            Assert.Equal(2, results.Count);
            Assert.Equal(results[0].Smartphone, smartphone1);
        }

        [Fact]
        public void Add_Quantity_For_Existing_Lines()
        {
            Cart cart = new Cart();

            cart.AddItem(smartphone1, 2);
            cart.AddItem(smartphone2, 3);
            cart.AddItem(smartphone1, 3);
            var results = cart.Lines.ToList();

            Assert.Equal(5, results[0].Quantity);
            Assert.Equal(3, results[1].Quantity);
        }

        [Fact]
        public void Remove_Line()
        {
            Cart cart = new Cart();

            cart.AddItem(smartphone1, 2);
            cart.AddItem(smartphone2, 3);
            cart.RemoveLine(smartphone1);
            var result = cart.Lines.Where(s => s.Smartphone.Id == 0).FirstOrDefault();

            Assert.Null(result);
        }

        [Fact]
        public void Remove_Item_One_Quantity_Should_Remain()
        {
            Cart cart = new Cart();

            cart.AddItem(smartphone1, 2);
            cart.AddItem(smartphone2, 3);
            cart.RemoveItem(smartphone1);
            cart.RemoveItem(smartphone1);
            cart.RemoveItem(smartphone1);
            cart.RemoveItem(smartphone2);
            var quantitySmartphone1 = cart.Lines.ToList()[0].Quantity;
            var quantitySmartphone2 = cart.Lines.ToList()[1].Quantity;

            Assert.Equal(1, quantitySmartphone1);
            Assert.Equal(2, quantitySmartphone2);
        }
    }
}
