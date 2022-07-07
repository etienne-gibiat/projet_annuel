using System;
using System.Collections.Generic;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
    internal class Heap<T, TKey>
    where T : IHeapNode<TKey>
    where TKey : IComparable<TKey>
  {
    private T[] data;
    private int size;

    private static int Parent(int i) => i - 1 >> 1;

    private static int Left(int i) => (i << 1) + 1;

    private static int Right(int i) => (i << 1) + 2;

    public Heap()
    {
      this.data = new T[0];
      this.size = 0;
    }

    public Heap(int capacity)
    {
      this.data = new T[capacity];
      this.size = 0;
    }

    public Heap(T[] items)
    {
      this.data = new T[items.Length];
      this.size = this.data.Length;
      Array.Copy((Array) items, (Array) this.data, this.data.Length);
      for (int index = 0; index < this.size; ++index)
        this.data[index].HeapIndex = index;
      this.Heapify();
    }

    public Heap(IEnumerable<T> items)
    {
      this.data = items.ToArray<T>();
      this.size = this.data.Length;
      for (int index = 0; index < this.size; ++index)
        this.data[index].HeapIndex = index;
      this.Heapify();
    }

    public int Count => this.size;

    public T Peek()
    {
      if (this.size == 0)
        throw new Exception("Heap is empty");
      return this.data[0];
    }

    public void Heapify()
    {
      for (int i = Heap<T, TKey>.Parent(this.size); i >= 0; --i)
        this.Heapify(i);
    }

    private void Heapify(int i)
    {
      TKey key1 = this.data[i].Key;
      int i1 = i;
      TKey other = key1;
      int index1 = Heap<T, TKey>.Left(i);
      if (index1 < this.size)
      {
        TKey key2 = this.data[index1].Key;
        if (key2.CompareTo(other) < 0)
        {
          i1 = index1;
          other = key2;
        }
      }
      int index2 = Heap<T, TKey>.Right(i);
      if (index2 < this.size)
      {
        if (this.data[index2].Key.CompareTo(other) < 0)
          i1 = index2;
      }
      if (i == i1)
      {
        this.data[i].HeapIndex = i;
      }
      else
      {
        T[] data1 = this.data;
        int index3 = i;
        T[] data2 = this.data;
        int num = i1;
        T obj1 = this.data[i1];
        T obj2 = this.data[i];
        data1[index3] = obj1;
        int index4 = num;
        T obj3 = obj2;
        data2[index4] = obj3;
        this.data[i].HeapIndex = i;
        this.Heapify(i1);
      }
    }

    public void DecreasedKey(T item)
    {
      int i = item.HeapIndex;
      TKey key = item.Key;
      int index1;
      for (; i != 0; i = index1)
      {
        index1 = Heap<T, TKey>.Parent(i);
        T obj1 = this.data[index1];
        if (obj1.Key.CompareTo(key) > 0)
        {
          T[] data1 = this.data;
          int index2 = index1;
          T[] data2 = this.data;
          int num = i;
          T obj2 = this.data[i];
          T obj3 = this.data[index1];
          data1[index2] = obj2;
          int index3 = num;
          T obj4 = obj3;
          data2[index3] = obj4;
          obj1.HeapIndex = i;
        }
        else
        {
          item.HeapIndex = i;
          return;
        }
      }
      item.HeapIndex = i;
    }

    public void IncreasedKey(T item) => this.Heapify(item.HeapIndex);

    public void ChangedKey(T item)
    {
      this.DecreasedKey(item);
      this.IncreasedKey(item);
    }

    public void Insert(T item)
    {
      if (this.data.Length == this.size)
      {
        T[] objArray = new T[this.size * 2];
        Array.Copy((Array) this.data, (Array) objArray, this.size);
        this.data = objArray;
      }
      this.data[this.size] = item;
      item.HeapIndex = this.size;
      ++this.size;
      this.DecreasedKey(item);
    }

    public void Delete(T item)
    {
      int heapIndex = item.HeapIndex;
      if (heapIndex == this.size - 1)
      {
        --this.size;
      }
      else
      {
        item = this.data[heapIndex] = this.data[this.size - 1];
        item.HeapIndex = heapIndex;
        --this.size;
        this.IncreasedKey(item);
        this.DecreasedKey(item);
      }
    }

    public void Clear() => this.size = 0;
  }
}