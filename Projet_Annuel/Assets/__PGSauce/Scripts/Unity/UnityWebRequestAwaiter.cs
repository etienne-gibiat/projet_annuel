using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

namespace PGSauce.Unity
{
    //https://gist.github.com/krzys-h/9062552e33dd7bd7fe4a6c12db109a1a
    public class UnityWebRequestAwaiter : INotifyCompletion
    {
        private readonly UnityWebRequestAsyncOperation asyncOp;
        private Action _continuation;

        public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp)
        {
            this.asyncOp = asyncOp;
            asyncOp.completed += OnRequestCompleted;
        }

        public bool IsCompleted => asyncOp.isDone;

        public void GetResult() { }

        public void OnCompleted(Action continuation)
        {
            this._continuation = continuation;
        }

        private void OnRequestCompleted(AsyncOperation obj)
        {
            _continuation();
        }
    }
    
    public static class ExtensionMethods
    {
        public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
        {
            return new UnityWebRequestAwaiter(asyncOp);
        }
    }
}