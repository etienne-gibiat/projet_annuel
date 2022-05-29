using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Elementis.Scripts.Procedural_Trees.Volumes
{
    [InlineEditor()]
    public abstract class TreeVolume : ScriptableObject
    {
        public abstract List<Vector3> GenerateRandomLocalSpaceAttractors(TreeGenerator tree);
        public virtual void DrawGizmos(TreeGenerator treeGenerator){}
    }
}