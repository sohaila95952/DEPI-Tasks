using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_advanced
{
    internal class MyStack<T>
    {
        private Stack<T> stack = new Stack<T>();
        public void Push(T item)
        {
            stack.Push(item);
        }
        public T Pop()
        {
            return stack.Pop();
        }
        public T Peek()
        {
            return stack.Peek();
        }

    }
}
