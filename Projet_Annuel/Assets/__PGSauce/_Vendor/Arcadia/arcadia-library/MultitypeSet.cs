using System;
using System.Collections.Generic;

public class MultitypeSet
{
    public MultitypeSet()
    {
        m_types = new Dictionary<Type, HashSet<object>>();
    }

    public void Add<T>(T obj)
    {
        // TODO: improve exceptions
        SafeGetHashSet<T>().Add(obj);
    }

    public void AddRange<T>(IEnumerable<T> collection)
    {
        HashSet<object> typeSet = SafeGetHashSet<T>();

        foreach (T item in collection)
        {
            typeSet.Add(item);
        }
    }

    public void Remove<T>(T obj)
    {
        if (obj != null && m_types[typeof(T)].Contains(obj))
        {
            m_types[typeof(T)].Remove(obj);
        }
        else
        {
            throw new ArgumentException("Trying to delete an unassigned listener.");
        }
    }

    public void Clear()
    {
        m_types.Clear();
    }

    public void Clear<T>()
    {
        m_types.Remove(typeof(T));
    }

    public IEnumerable<T> ElementsOf<T>()
    {
        foreach (object obj in m_types[typeof(T)])
        {
            yield return (T)obj;
        }
    }

    public void ForEach<T>(Action<T> action)
    {
        foreach (object obj in m_types[typeof(T)])
        {
            action((T)obj);
        }
    }

    private HashSet<object> SafeGetHashSet<T>()
    {
        HashSet<object> typeSet;

        if (!m_types.TryGetValue(typeof(T), out typeSet))
        {
            typeSet = new HashSet<object>();
            m_types.Add(typeof(T), typeSet);
        }

        return typeSet;
    }

    private Dictionary<Type, HashSet<object>> m_types;
}
