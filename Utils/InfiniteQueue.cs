using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAWars.Utils
{
    class InfiniteQueue<T>
    {
        private Queue<T> queue;

        /// <summary>
        /// Removes the element at the head of the queue, and puts it at the end.
        /// </summary>
        /// <returns>The element at the head of the queue.</returns>
        public T next()
        {
            T value = queue.Dequeue();
            queue.Enqueue(value);
            return value;
        }

        /// <summary>
        /// Creates an infinite queue containing a given set of elements.
        /// </summary>
        /// <param name="values">Collection of elements to add to the queue.</param>
        public InfiniteQueue(ICollection<T> values)
        {
            queue = new Queue<T>();

            foreach (T value in values)
            {
                queue.Enqueue(value);
            }
        }
    }
}
