using System;
using System.Collections.Generic;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
    internal class FrequencySet
  {
    public int[] priorityIndices;
    public double[] frequencies;
    public double[] plogp;

    public FrequencySet(double[] weights, int[] priorities = null)
    {
      if (priorities == null)
        priorities = Enumerable.Repeat<int>(0, weights.Length).ToArray<int>();
      Dictionary<int, FrequencySet.Group> source = new Dictionary<int, FrequencySet.Group>();
      Dictionary<int, List<double>> dictionary1 = new Dictionary<int, List<double>>();
      Dictionary<int, List<int>> dictionary2 = new Dictionary<int, List<int>>();
      FrequencySet.Group group1;
      for (int index = 0; index < weights.Length; ++index)
      {
        int priority = priorities[index];
        double weight = weights[index];
        FrequencySet.Group group2;
        if (!source.TryGetValue(priority, out group2))
        {
          group1 = new FrequencySet.Group();
          group1.priority = priority;
          group1.plogp = new List<double>();
          group2 = group1;
          dictionary1[priority] = new List<double>();
          dictionary2[priority] = new List<int>();
        }
        ++group2.patternCount;
        group2.weightSum += weight;
        dictionary2[priority].Add(index);
        source[priority] = group2;
      }
      this.frequencies = new double[weights.Length];
      this.plogp = new double[weights.Length];
      for (int index = 0; index < weights.Length; ++index)
      {
        int priority = priorities[index];
        FrequencySet.Group group3 = source[priority];
        double frequency = weights[index] / group3.weightSum;
        this.frequencies[index] = frequency;
        this.plogp[index] = this.ToPLogP(frequency);
        dictionary1[priority].Add(frequency);
        group3.plogp.Add(this.ToPLogP(frequency));
      }
      foreach (int key1 in source.Keys.ToList<int>())
      {
        FrequencySet.Group group4 = source[key1];
        Dictionary<int, FrequencySet.Group> dictionary3 = source;
        int key2 = key1;
        group1 = new FrequencySet.Group();
        group1.priority = group4.priority;
        group1.patternCount = group4.patternCount;
        group1.weightSum = group4.weightSum;
        group1.patterns = dictionary2[key1].ToArray();
        group1.frequencies = dictionary1[key1].ToArray();
        group1.plogp = group4.plogp;
        FrequencySet.Group group5 = group1;
        dictionary3[key2] = group5;
      }
      this.groups = source.OrderByDescending<KeyValuePair<int, FrequencySet.Group>, int>((Func<KeyValuePair<int, FrequencySet.Group>, int>) (x => x.Key)).Select<KeyValuePair<int, FrequencySet.Group>, FrequencySet.Group>((Func<KeyValuePair<int, FrequencySet.Group>, FrequencySet.Group>) (x => x.Value)).ToArray<FrequencySet.Group>();
      Dictionary<int, int> priorityToPriorityIndex = ((IEnumerable<FrequencySet.Group>) this.groups).Select((g, i) => new
      {
        g = g,
        i = i
      }).ToDictionary(t => t.g.priority, t => t.i);
      this.priorityIndices = ((IEnumerable<int>) priorities).Select<int, int>((Func<int, int>) (p => priorityToPriorityIndex[p])).ToArray<int>();
    }

    private double ToPLogP(double frequency) => frequency <= 0.0 ? 0.0 : frequency * Math.Log(frequency);

    public FrequencySet.Group[] groups { get; }

    public struct Group
    {
      public int priority;
      public int patternCount;
      public double weightSum;
      public int[] patterns;
      public double[] frequencies;
      public List<double> plogp;
    }
  }
}