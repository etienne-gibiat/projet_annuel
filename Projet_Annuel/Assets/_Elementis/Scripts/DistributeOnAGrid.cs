using System.Collections.Generic;
using PGSauce.Core.Extensions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace _Elementis.Scripts
{
    public class DistributeOnAGrid : MonoBehaviour
    {
        [AssetsOnly] public GameObject prefab;
        public List<GameObject> instances;
        public Vector2Int gridSize;
        public Vector2 space;
        public float y = 0.1f;

        [Button]
        private void Distribute()
        {
#if UNITY_EDITOR
            foreach (var instance in instances)
            {
                instance.DestroyUniversal();
            }
            
            instances.Clear();
            
            if (gridSize.x % 2 == 0)
            {
                gridSize.x++;
            }
            
            if (gridSize.y % 2 == 0)
            {
                gridSize.y++;
            }

            var width = gridSize.x * space.x;
            var height = gridSize.y * space.y;
            var size = new Vector3(width, 0, height);

            var min = transform.position - size / 2f;
            
            var start = min + new Vector3(space.x, y, space.y) / 2f;

            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    var pos = start + new Vector3(i * space.x, 0, j * space.y);
                    var go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                    go.transform.position = pos;
                    go.transform.SetParent(transform,true);
                    instances.Add(go);
                }
            }
#endif
            
        }
    }
}