using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        class MyDictionary<T> : IEnumerable, IEnumerator
        {
            Dictionary<T>[] dictionary = null;
            Dictionary<T>[] dictionaryBuffer = null;
            public void Add(int key, T value)
            {
                if (dictionary == null)
                {
                    dictionary = new Dictionary<T>[1];
                    dictionary[0] = new Dictionary<T>(key, value);
                }
                else
                {
                    dictionaryBuffer = new Dictionary<T>[dictionary.Length + 1];
                    for (int i = 0; i < dictionaryBuffer.Length; i++)
                    {
                        if (i < dictionary.Length)
                            dictionaryBuffer[i] = dictionary[i];
                        else
                            dictionaryBuffer[i] = new Dictionary<T>(key, value);
                    }
                    dictionary = dictionaryBuffer;
                    dictionaryBuffer = null;
                }
            }
            public Dictionary<T> this[int index]
            {
                get
                {
                    if (dictionary != null)
                    {
                        if (index >= 0 && index < dictionary.Length)
                        {
                            return dictionary[index];
                        }
                        else
                        {
                            Console.WriteLine("Попытка обращения за пределы коллекции, возвращено стандартное значение value");
                            return default(Dictionary<T>);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Обнаружена пустая коллекция, возвращено стандартное значение value");
                        return default(Dictionary<T>);
                    }
                }
                set
                {
                    if (dictionary != null)
                    {
                        if (index >= 0 && index < dictionary.Length)
                            dictionary[index] = value;
                        else
                            Console.WriteLine("Попытка обращения за пределы коллекции, попробуйте другой индекс");
                    }
                    else
                        Console.WriteLine("Обнаружена пустая коллекция, добавьте значение");
                }
            }
            //public Dictionary<T> this[int key]
            //{
            //    get
            //    {
            //        if (dictionary != null)
            //        {
            //            for (int i = 0; i < dictionary.Length; i++)
            //            {
            //                if (key == dictionary[i].Key)
            //                {
            //                    return dictionary[i];
            //                }
            //            }
            //            Console.WriteLine("Не найдено подходящего ключа, возвращено стандратное значение value");
            //            return default(Dictionary<T>);
            //        }
            //        else
            //        {
            //            Console.WriteLine("Обнаружена пустая коллекция, возвращено стандартное значение value");
            //            return default(Dictionary<T>);
            //        }
            //    }
            //    set
            //    {
            //        if (dictionary != null)
            //        {
            //            for (int i = 0; i < dictionary.Length; i++)
            //            {
            //                if (key == dictionary[i].Key)
            //                {
            //                    dictionary[i] = value;
            //                }
            //            }
            //            Console.WriteLine("Попытка обращения за пределы коллекции, попробуйте другой индекс");
            //        }
            //        else
            //            Console.WriteLine("Обнаружена пустая коллекция, добавьте значение");
            //    }
            //}
            int pos = -1;
            public bool MoveNext()
            {
                if(pos < dictionary.Length - 1)
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
            public void Reset() { pos = -1; }
            public object Current { get { return dictionary[pos]; } }
            public IEnumerator GetEnumerator() { return this; }
        }
        static void Main(string[] args)
        {
            MyDictionary<string> dict = new MyDictionary<string>();
            dict.Add(2, "Hello");
            foreach (Dictionary<string> dictionary in dict)
            {
                Console.WriteLine("Key - {0}, Value - {1}", dictionary.Key, dictionary.Value);
            }
            Console.WriteLine();
            dict.Add(4, "World");
            foreach (Dictionary<string> dictionary in dict)
            {
                Console.WriteLine("Key - {0}, Value - {1}", dictionary.Key, dictionary.Value);
            }
            Console.ReadKey();
        }
        class Dictionary<T>
        {
            public int Key { get; set; }
            public T Value { get; set; }
            public Dictionary(int key, T value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}
