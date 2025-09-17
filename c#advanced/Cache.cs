using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_advanced
{
    internal class Cache<TKey, TValue> where TKey:  notnull
    {
        private Dictionary<TKey, (TValue value, DateTime expiration)> cache;
        public Cache(TKey key,TValue value ,DateTime expire_date)
        {
            cache = new Dictionary<TKey, (TValue, DateTime)>();
            cache[key]=(value, expire_date);
        }
        public void Add(TKey key, TValue value,DateTime expire_date) {
            cache[key] = (value, expire_date);
        }
        public TValue GetValue(TKey key) 
        {
            if(cache.ContainsKey(key))
            {
                var value = cache[key];
                if(DateTime.Now< value.expiration)
                {
                    return value.value;
                }
                else
                {
                    cache.Remove(key);
                }
               
            } return default!;

        }
    }
}
