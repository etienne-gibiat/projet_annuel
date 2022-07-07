using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace _Elementis.Scripts.WFC
{
    internal class Deque<T> : IEnumerable<T>, IEnumerable
  {
    private T[] _data;
    private int _dataLength;
    private int _lo;
    private int _hi;

    public Deque(int capacity = 4)
    {
      _data = new T[capacity];
      _dataLength = capacity;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Push(T t)
    {
      int hi = _hi;
      int lo = _lo;
      _data[hi] = t;
      int num = hi + 1;
      if (num == _dataLength)
        num = 0;
      _hi = num;
      if (num != lo)
        return;
      ResizeFromFull();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Pop()
    {
      int lo = _lo;
      int num1 = _hi;
      int num2 = num1;
      if (lo == num2)
        throw new Exception("Deque is empty");
      if (num1 == 0)
        num1 = _dataLength;
      int index = num1 - 1;
      _hi = index;
      return _data[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Shift(T t)
    {
      int num1 = _lo;
      int hi = _hi;
      if (num1 == 0)
        num1 = _dataLength;
      int index = num1 - 1;
      _data[index] = t;
      _lo = index;
      int num2 = index;
      if (hi != num2)
        return;
      ResizeFromFull();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Unshift()
    {
      int lo = _lo;
      int hi = _hi;
      int index = lo != hi ? lo : throw new Exception("Deque is empty");
      int num = lo + 1;
      if (num == _dataLength)
        num = 0;
      _lo = num;
      return _data[index];
    }

    public void DropFirst(int n)
    {
      int hi = _hi;
      int lo = _lo;
      if (lo <= hi)
      {
        int num = lo + n;
        if (num >= hi)
          _lo = _hi = 0;
        else
          _lo = num;
      }
      else
      {
        int num1 = lo + n;
        if (num1 < _dataLength)
          return;
        int num2 = num1 - _dataLength;
        if (num2 >= hi)
          _lo = _hi = 0;
        else
          _lo = num2;
      }
    }

    public void DropLast(int n)
    {
      int hi = _hi;
      int lo = _lo;
      if (lo <= hi)
      {
        int num = hi - n;
        if (lo >= num)
          _lo = _hi = 0;
        else
          _hi = num;
      }
      else
      {
        int num1 = hi - n;
        if (num1 >= 0)
          return;
        int num2 = num1 + _dataLength;
        if (lo >= num2)
          _lo = _hi = 0;
        else
          _hi = num2;
      }
    }

    public int Count
    {
      get
      {
        int num = _hi - _lo;
        if (num < 0)
          num += _dataLength;
        return num;
      }
    }

    private void ResizeFromFull()
    {
      int dataLength = _dataLength;
      int length = dataLength * 2;
      T[] objArray = new T[length];
      int index1 = _lo;
      int index2 = 0;
      int hi = _hi;
      do
      {
        objArray[index2] = _data[index1];
        ++index2;
        ++index1;
        if (index1 == dataLength)
          index1 = 0;
      }
      while (index1 != hi);
      _data = objArray;
      _dataLength = length;
      _lo = 0;
      _hi = index2;
    }

    public IEnumerable<T> Slice(int start, int end)
    {
      int lo = _lo;
      int hi = _hi;
      T[] data = _data;
      int dataLength = _dataLength;
      int i = lo + start;
      int e = lo + end;
      if (start < 0)
        throw new Exception();
      if (hi >= lo)
      {
        if (e > hi)
          throw new Exception();
      }
      else if (e > hi + dataLength)
        throw new Exception();
      if (start < end)
      {
        if (i >= dataLength)
          i -= dataLength;
        if (e >= dataLength)
          e -= dataLength;
        do
        {
          yield return data[i];
          ++i;
          if (i == dataLength)
            i = 0;
        }
        while (i != e);
      }
    }

    public IEnumerable<T> ReverseSlice(int start, int end)
    {
      int lo = _lo;
      int hi = _hi;
      T[] data = _data;
      int dataLength = _dataLength;
      int i = lo + start;
      int e = lo + end;
      if (start < 0)
        throw new Exception();
      if (hi >= lo)
      {
        if (e > hi)
          throw new Exception();
      }
      else if (e > hi + dataLength)
        throw new Exception();
      if (start < end)
      {
        if (i >= dataLength)
          i -= dataLength;
        if (e >= dataLength)
          e -= dataLength;
        do
        {
          --e;
          yield return data[e];
          if (e == 0)
            e = dataLength - 1;
        }
        while (i != e);
      }
    }

    public IEnumerator<T> GetEnumerator()
    {
      int lo = _lo;
      int hi = _hi;
      T[] data = _data;
      int dataLength = _dataLength;
      int i = lo;
      int e = hi;
      if (hi >= lo)
      {
        if (e > hi)
          throw new Exception();
      }
      else if (e > hi + dataLength)
        throw new Exception();
      if (lo != hi)
      {
        if (i >= dataLength)
          i -= dataLength;
        if (e >= dataLength)
          e -= dataLength;
        do
        {
          yield return data[i];
          ++i;
          if (i == dataLength)
            i = 0;
        }
        while (i != e);
      }
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) GetEnumerator();
  }
}