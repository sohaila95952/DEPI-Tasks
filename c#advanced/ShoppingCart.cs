using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace c_advanced
{
    class shoppingcart
    {
     List<string> Items = new List<string>();
     Dictionary<string,int>Quantities = new Dictionary<string,int>();
     HashSet<string>Discount = new HashSet<string>();
        public void addItem(string item,int quantity)
        {
            Items.Add(item);
            Quantities.Add(item, quantity);
        }
        public void removeItem(string item) { 
        Items.Remove(item);
         Quantities.Remove(item);
        }
        public void addDiscount(string discount) { 
            Discount.Add(discount);
        }
        public void showCart()
        {
            if (Quantities.Count>0)
            {
                foreach (var item in Quantities)
                {
                    Console.Write($"Item : {item.Key}=>Quantity : {item.Value} , ");
                    if (Discount.Contains(item.Key))
                        Console.WriteLine("Has Discount");

                }
            }
            else
                Console.WriteLine("Cart is empty");
        }


    }
}
