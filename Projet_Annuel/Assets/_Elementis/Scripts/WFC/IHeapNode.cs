using System;

namespace _Elementis.Scripts.WFC
{
    internal interface IHeapNode<TKey> where TKey : IComparable<TKey>
    {
        int HeapIndex { get; set; }

        TKey Key { get; }
    }
}