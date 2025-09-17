using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace c_advanced
{
    internal class Repository<T> where T : IEntity
    {
        private List<T> items=new List<T>();
        public void Add(T item)
        {
            items.Add(item);
            Console.WriteLine("added");
        }
        public T Read(int id)
        {
            foreach (T item in items) 
            {
                if (item.Id == id) 
                {
                    return item;
                }
            }
            return default!;
        }
        public void Update(int id,T update)
        {
            T old = Read(id);
            if (old != null) 
            {
                items.Remove(old);
                items.Add(update);
            }
        }

        public void Delete(int id)
        {
            T old = Read(id);
            if (old != null)
            {
                items.Remove(old);
                Console.WriteLine("removed");
            }
            else
            {
                Console.WriteLine("not found");
            }
        }
        public Repository() { }
       
    }
    public interface IEntity
    {
        int Id { get; set; }
    }

    public class student : IEntity
    {
        public student(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        
    }
}
