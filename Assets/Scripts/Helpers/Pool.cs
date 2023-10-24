using System;
using System.Collections.Generic;

namespace CoinRunner.Helpers
{
    /// <summary> Pool, that is not thread-safe. Use this pool for best performance. </summary>
    public class Pool<TPoolObject>
    {
        readonly Stack<TPoolObject> values = new();
        readonly Func<TPoolObject> create;
        readonly Action<TPoolObject> reset;
    
        public Pool(Func<TPoolObject> create, Action<TPoolObject> reset, int initialSize = 0)
        {
            this.create = create;
            this.reset = reset;
            for (var i = 0; i < initialSize; i++)
            {
                Release(create());
            }
        }

        /// <summary> Number of currently pooled available instances. </summary>
        public int Size => values.Count;

        /// <summary> Get instance from the pool. If pool is empty creates new instance and return it. </summary>
        public TPoolObject Borrow() => Size > 0 ? values.Pop() : create();

        /// <summary> Return instance back to the pool. </summary>
        public void Release(TPoolObject value)
        {
            reset(value);
            values.Push(value);
        }
    }
}