using System.Collections.Generic;
using Drawing;
using PGSauce.Core;
using PGSauce.Core.Extensions;
using PGSauce.Core.Strings;
using UnityEngine;

namespace _Elementis.Scripts.Procedural_Trees.Volumes
{
    [CreateAssetMenu(menuName = MenuPaths.GamePath + "Trees/Sphere Volume")]
    public class SphereTreeVolume : TreeVolume
    {
        public Vector3 center;
        public float radius = 5;
        [Range(0f, 1f)] public float minYToHaveLeavesRadiusPercentage = 0.8f;
        [Range(0f, 1f)] public float radiusPercentageToHaveLeaves = 0.75f;
        public AnimationCurve probaToHaveLeavesDistribution = AnimationCurve.Linear(0,0,1,1);

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

        public override float ProbabilityToHaveALeafGerm(TreeGenerator tree, Vector3 nodeWorldPosition)
        {
            
            if (nodeWorldPosition.y < GetWorldMinYToHaveLeaves(tree))
            {
                return 0f;
            }

            var wCenter = GetWorldCenter(tree);
            var distanceToCenter = Vector3.Distance(nodeWorldPosition, wCenter);
            var minDistanceToHaveLeaves = WorldRadiusToHaveLeaves(tree);
            if (distanceToCenter < minDistanceToHaveLeaves)
            {
                return 0f;
            }

            var maxDistanceToHaveLeaves = WorldRadius(tree);
            var remappedDistance = distanceToCenter.Remap(minDistanceToHaveLeaves, maxDistanceToHaveLeaves, 0f, 1f);
            return probaToHaveLeavesDistribution.Evaluate(remappedDistance);
        }

        public override void DrawGizmos(TreeGenerator treeGenerator)
        {
            base.DrawGizmos(treeGenerator);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(GetWorldCenter(treeGenerator), WorldRadius(treeGenerator));
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GetWorldCenter(treeGenerator), WorldRadiusToHaveLeaves(treeGenerator));

            var y = GetWorldMinYToHaveLeaves(treeGenerator);
            
            Gizmos.color = Color.red;
            Gizmos.DrawCube(treeGenerator.transform.position.WithY(y), new Vector3(3,0,3));
        }

        private float WorldRadius(TreeGenerator treeGenerator)
        {
            return radius * treeGenerator.Scale;
        }

        private float WorldRadiusToHaveLeaves(TreeGenerator treeGenerator)
        {
            return WorldRadius(treeGenerator) * radiusPercentageToHaveLeaves;
        }

        public override Vector3 GetWorldCenter(TreeGenerator treeGenerator)
        {
            return treeGenerator.transform.position + center * treeGenerator.Scale;
        }

        private float GetWorldMinYToHaveLeaves(TreeGenerator treeGenerator)
        {
            return (center * treeGenerator.Scale).y - radius * treeGenerator.Scale * minYToHaveLeavesRadiusPercentage + treeGenerator.transform.position.y;
        }
    }
}