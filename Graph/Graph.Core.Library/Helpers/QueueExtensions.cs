using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Core.Library
{
    public static class QueueExtensions
    {
        public static bool TryDequeue<T>(this Queue<T> queue, out T item)
        {
            item = default(T);
            bool ok = false;
            try
            {
                item = queue.Dequeue();
                ok = true;
            }
            catch (Exception e)
            { }
            return ok;
        }
    }
}
