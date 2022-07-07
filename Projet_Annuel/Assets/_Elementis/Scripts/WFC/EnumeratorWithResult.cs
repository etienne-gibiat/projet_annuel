using System;
using System.Collections;

namespace _Elementis.Scripts.WFC
{
    /// <summary>
    /// An IEnumerator that also records a given result when it is finished.
    /// It is intended for use with Unity coroutines.
    /// </summary>
    public class EnumeratorWithResult<T> : IEnumerator
    {
        private IEnumerator inner;
        private bool hasResult;
        private T result;

        public EnumeratorWithResult(IEnumerator e)
        {
            this.inner = e;
        }

        /// <summary>
        /// The value returned by this enumerator.
        /// This will throw if you attempt to access it before fully iterating through the enumerator.
        /// </summary>
        public T Result
        {
            get
            {
                if (!hasResult)
                {
                    throw new Exception("Enumerator has not finished yet");
                }
                return result;
            }
        }

        /// <summary>
        /// The value returned by this enumerator.
        /// This will return false if you attempt to access it before fully iterating through the enumerator.
        /// </summary>
        public bool TryGetResult(out T result)
        {
            if(hasResult)
            {
                result = this.result;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        public object Current => inner.Current;

        public bool MoveNext()
        {
            var hasNext = inner.MoveNext();
            if (hasNext && inner.Current is T result)
            {
                hasResult = true;
                this.result = result;
                return false;
            }
            if (!hasNext && !hasResult)
            {
                throw new Exception($"Enumerator finished without yielding a value of type {typeof(T).Name}");
            }
            return hasNext;
        }

        public void Reset()
        {
            inner.Reset();
            hasResult = false;
        }
    }
}