using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileStore.Domain.Entities
{
    public class Cart
    {
        private List<CartItem> _lineCollection = new List<CartItem>();
        public IEnumerable<CartItem> Lines { get { return _lineCollection; } }

        public void AddItem(Smartphone smartphone, int quantity)
        {
            CartItem line = _lineCollection.Where(s => s.Smartphone.Id == smartphone.Id)
                                                     .FirstOrDefault();
            if (line == null)
                _lineCollection.Add(new CartItem { Smartphone = smartphone, Quantity = quantity });
            else
                line.Quantity += quantity;
        }

        public void RemoveItem(Smartphone smartphone)
        {
            CartItem line = _lineCollection.Where(s => s.Smartphone.Id == smartphone.Id)
                                                     .FirstOrDefault();
            if (line == null || line.Quantity == 1)
                return;

            line.Quantity -= 1;
        }

        public void RemoveLine(Smartphone smartphone)
        {
            _lineCollection.RemoveAll(l => l.Smartphone.Id == smartphone.Id);
        }

        public decimal ComptuteTotalValue()
        {
            return _lineCollection.Sum(s => s.Smartphone.Price * s.Quantity);
        }

        public void Clear()
        {
            _lineCollection.Clear();
        }  
    }
}
