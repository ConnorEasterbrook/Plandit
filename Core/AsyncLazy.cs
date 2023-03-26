using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Plandit.Core
{
    /// <summary>
    /// Asynchronous lazy initialization class. Use for resource-intensive objects.
    /// This will defer the execution of an asynchronous operation until it is needed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsyncLazy<T>
    {
        // Lazy defers creation of the object until needed, and then catches it for future use.
        readonly Lazy<Task<T>> instance; // 

        /// <summary>
        /// AsyncLazy constructor to delegate and run a new task asynchronously.
        /// </summary>
        /// <param name="factory"></param>
        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        /// <summary>
        /// Return TaskAwaiter to support the use of "await."
        /// </summary>
        /// <returns></returns>
        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }

        public void Start()
        {
            Task unused = instance.Value; // Invoke the instance value to start the asynchronous operation.
        }
    }
}
