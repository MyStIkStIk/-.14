using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static ДЗ._14.Program;

namespace ДЗ._14
{
    static class Arrays
    {
        public static T[] GetArray<T>(this MyList list)
        {
            T[] result = new T[list.GetCollectionLength()];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = list[i];
            }
            return result;
        }
    }
    internal class Program
    {
        public class Product
        {
            public string TypeOfProduct { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public Product(string typeOfProduct, string name, double price)
            {
                TypeOfProduct = typeOfProduct;
                Name = name;
                Price = price;
            }
        }
        public class MyList : IEnumerable, IEnumerator
        {
            public Product[] products = null;
            public Product[] productsBoofer = null;
            public void Add(string typeOfProduct, string name, double price)
            {
                if(products == null)
                {
                    products = new Product[1];
                    products[0] = new Product(typeOfProduct, name, price);
                }
                else
                {
                    productsBoofer = new Product[products.Length + 1];
                    for (int i = 0; i < productsBoofer.Length; i++)
                    {
                        if (i < products.Length)
                            productsBoofer[i] = products[i];
                        else
                            productsBoofer[i] = new Product(typeOfProduct, name, price);
                    }
                    products = productsBoofer;
                    productsBoofer = null;
                }
            }
            public int GetCollectionLength()
            {
                return products.Length;
            }
            public Product this[int index]
            {
                get
                {
                    if(index > 0 && index < products.Length)
                    {
                        return products[index];
                    }
                    else
                        return null;
                }
                set
                {
                    if (index > 0 && index < products.Length)
                    {
                        products[index] = value;
                    }
                    else
                        Console.WriteLine("Попытка обращения за пределы коллекции");
                }
            }
            int pos = -1;
            public bool MoveNext()
            {
                if(pos < products.Length -1)
                {
                    pos++;
                    return true;
                }
                else
                {
                    Reset();
                    return false;
                }
            }
            public void Reset()
            {
                pos = -1;
            }
            public object Current
            {
                get { return products[pos]; }
            }
            public IEnumerator GetEnumerator()
            {
                return this;
            }
        }
        static void Main(string[] args)
        {
            MyList supermarket = new MyList();
            supermarket.Add("Food", "Milk", 16.90);
            foreach (Product products in supermarket)
            {
                Console.WriteLine("Type - {0}, Name - {1}, Price - {2}", products.TypeOfProduct, products.Name, products.Price);
            }
            supermarket.Add("Food", "Meat", 16.90);
            Console.WriteLine();
            foreach (Product products in supermarket)
            {
                Console.WriteLine("Type - {0}, Name - {1}, Price - {2}", products.TypeOfProduct, products.Name, products.Price);
            }
            Console.WriteLine();
            Arrays.GetArray<Product>(supermarket);
            Console.ReadKey();
        }
        
    }
}
