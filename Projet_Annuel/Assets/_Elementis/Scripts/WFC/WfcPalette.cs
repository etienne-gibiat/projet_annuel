using System;
using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.Strings;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    [Serializable]
    public class PaletteEntry
    {
        public Color color;
        public string name;
    }

    [Serializable]
    internal class MatchOverride
    {
        public int color1;
        public int color2;
        public bool isMatch;
    }
    
    [CreateAssetMenu(fileName = "WFC Palette", menuName = MenuPaths.GamePath + "WFC/Palette", order = 1)]
    public class WfcPalette : ScriptableObject, ISerializationCallbackReceiver
    {
        public List<PaletteEntry> entries;

        [SerializeField]
        private List<MatchOverride> matchOverridesList;

        public Dictionary<(int, int), bool> matchOverrides = new Dictionary<(int color1, int color2), bool>();

        
        public WfcPalette()
        {
            entries = new List<PaletteEntry>
            {
                new PaletteEntry { name="Empty", color=new Color(0, 0, 0, 0) },
                new PaletteEntry { name="Black", color=Color.black },
                new PaletteEntry { name="White", color=Color.white },
                new PaletteEntry { name="Grey", color=Color.grey },
                new PaletteEntry { name="Light Grey", color=new Color(195/255f, 195/255f, 195/255f) },
                new PaletteEntry { name="Brown", color=new Color(136/255f, 0/255f, 21/255f) },
                new PaletteEntry { name="Light Brown", color=new Color(185/255f, 122/255f, 87/255f) },
                new PaletteEntry { name="Red", color=new Color(237/255f, 28/255f, 36/255f) },
                new PaletteEntry { name="Pink", color=new Color(255/255f, 174/255f, 201/255f) },
                new PaletteEntry { name="Orange", color=new Color(255/255f, 127/255f, 39/255f) },
                new PaletteEntry { name="Light Orange", color=new Color(255/255f, 201/255f, 14/255f) },
                new PaletteEntry { name="Yellow", color=new Color(255/255f, 242/255f, 0/255f) },
                new PaletteEntry { name="Beige", color=new Color(239/255f, 228/255f, 176/255f) },
                new PaletteEntry { name="Green", color=new Color(34/255f, 177/255f, 76/255f) },
                new PaletteEntry { name="Light Green", color=new Color(181/255f, 230/255f, 29/255f) },
                new PaletteEntry { name="Blue", color=new Color(0/255f, 162/255f, 232/255f) },
                new PaletteEntry { name="Light Blue", color=new Color(153/255f, 217/255f, 234/255f) },
                new PaletteEntry { name="Purple", color=new Color(163/255f, 73/255f, 164/255f) },
                new PaletteEntry { name="Light Purple", color=new Color(200/255f, 161/255f, 231/255f) },
            };
        }
        
        public int entryCount => entries.Count;
        
        public PaletteEntry GetEntry(int i)
        {
            if (i >= 0 && i < entries.Count)
                return entries[i];
            return null;
        }

        public Color GetColor(int i)
        {
            if (i >= 0 && i < entries.Count)
                return entries[i].color;
            return new Color(0.0f, 0.0f, 0.0f);
        }

        public void OnBeforeSerialize()
        {
            matchOverridesList = matchOverrides.Select(kv => new MatchOverride
                {
                    color1 = kv.Key.Item1,
                    color2 = kv.Key.Item2,
                    isMatch = kv.Value,
                })
                .ToList();
        }

        public void OnAfterDeserialize()
        {
            matchOverrides = matchOverridesList
                                 .ToDictionary(x => (x.color1, x.color2), x => x.isMatch) 
                             ?? new Dictionary<(int color1, int color2), bool>();
            matchOverridesList = null;
        }
        
        public bool Match(WfcFaceDetails a, WfcFaceDetails b)
        {
            return Match(a.topLeft, b.topRight) &&
                   Match(a.top, b.top) &&
                   Match(a.topRight, b.topLeft) &&
                   Match(a.left, b.right) &&
                   Match(a.center, b.center) &&
                   Match(a.right, b.left) &&
                   Match(a.bottomLeft, b.bottomRight) &&
                   Match(a.bottom, b.bottom) &&
                   Match(a.bottomRight, b.bottomLeft);
        }
        
        public bool Match(int a, int b)
        {
            // NB: This is a good line to customize if  you want more control over which tiles can be adjacent
            // See Customization in the documentation
            if (matchOverrides.TryGetValue((a, b), out var isMatch))
            {
                return isMatch;
            }

            return a == b || a == 0 || b == 0;
        }
    }
}