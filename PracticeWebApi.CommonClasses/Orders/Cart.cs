using PracticeWebApi.CommonClasses.Products;
using System;
using System.Collections.Generic;

namespace PracticeWebApi.CommonClasses.Orders
{
    public class Cart
    {
        public Dictionary<string, ProductTracker> Items { get; private set; }

        public Cart()
        {
            Items = new Dictionary<string, ProductTracker>();
        }

        public void UpdateCart(Product product, int count)
        {
            if (count < 0) throw new ArgumentException("Product count must be non-negative number");

            if (Items.ContainsKey(product.Id))
            {
                Items[product.Id].Count = count;
            }
            else
            {
                Items.Add(product.Id, new ProductTracker { Product = product, Count = count });
            }
        }
    }
}
