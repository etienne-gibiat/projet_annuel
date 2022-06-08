using System.Collections.Generic;
using Drawing;
using PGSauce.Core;
using PGSauce.Core.Strings;
using UnityEngine;

namespace _Elementis.Scripts.Procedural_Trees.Volumes
{
    [CreateAssetMenu(menuName = MenuPaths.GamePath + "Trees/Volume")]
    public class SphereTreeVolume : TreeVolume
    {
        public Vector3 center;
        public float radius = 5;

        public override List<Vector3> GenerateRandomLocalSpaceAttractors(TreeGenerator tree)
        {
            var number = tree.NbAttractors;
            var res = new List<Vector3>();
            for (var i = 0; i < number; i++)
            {
                var randomRadius = Random.Range(0f, 1f);
                randomRadius = Mathf.Pow(Mathf.Sin(randomRadius * Mathf.PI / 2f), 0.8f);
                randomRadius *= radius * tree.Scale;
                
                var alpha = Random.Range(0f, Mathf.PI);
                var theta = Random.Range(0f, Mathf.PI*2f);

                var pt = new Vector3(
                    randomRadius * Mathf.Cos(theta) * Mathf.Sin(alpha),
                    randomRadius * Mathf.Sin(theta) * Mathf.Sin(alpha),
                    randomRadius * Mathf.Cos(alpha)
                );

                pt += center * tree.Scale;

                res.Add(pt);
            }

            return res;
        }

        public override void DrawGizmos(TreeGenerator treeGenerator)
        {
            base.DrawGizmos(treeGenerator);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(treeGenerator.transform.position + center * treeGenerator.Scale, radius * treeGenerator.Scale);
        }
    }
}