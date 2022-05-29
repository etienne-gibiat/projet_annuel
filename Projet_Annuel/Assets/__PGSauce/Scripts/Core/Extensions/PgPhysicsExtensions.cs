using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PGSauce.Core.Extensions
{
    /// <summary>
    /// Some Unity physics extensions
    /// </summary>
    public static class PgPhysicsExtensions
    {
        /// <summary>
        /// Given a capsule collider, returns any colliders inside of it
        /// </summary>
        /// <param name="col">The collider to check from</param>
        /// <param name="layerMask">The mask of the colliders we want to find</param>
        /// <param name="radiusFactor">If  we want to scale the capsule</param>
        /// <returns>All the colliders inside the capsule</returns>
        public static Collider[] OverlapCapsule(this CapsuleCollider col, int layerMask, float radiusFactor = 1)
        {
            var (top, bottom, radius) = GetDataFromCapsule(col, radiusFactor);
            var cols = Physics.OverlapCapsule(top, bottom, radius * radiusFactor, layerMask);
            return cols;
        }

        /// <summary>
        /// Performs a capsule cast with the capsule collider as the reference
        /// </summary>
        /// <param name="col">The collider to check from</param>
        /// <param name="direction">The direction to cast the capsule</param>
        /// <param name="hit">The raycast hit info if an object is hit</param>
        /// <param name="layerMask">The mask of the objects we want to hit</param>
        /// <param name="maxDistance">The max distance of the cast</param>
        /// <param name="radiusFactor">If  we want to scale the capsule</param>
        /// <returns>The first hit object by the capsule</returns>
        public static bool CapsuleCast(this CapsuleCollider col, Vector3 direction, out RaycastHit hit, int layerMask,
            float maxDistance = Mathf.Infinity, float radiusFactor = 1F)
        {
            var (top, bottom, radius) = GetDataFromCapsule(col, radiusFactor);
            return Physics.CapsuleCast(top, bottom, radius, direction, out hit, maxDistance, layerMask);
        }
        
        /// <summary>
        /// Given a Box collider, returns any colliders inside of it
        /// </summary>
        /// <param name="collider">The collider to check from</param>
        /// <param name="layerMask">The mask of the colliders we want to find</param>
        /// <returns>All the colliders inside the box</returns>
        public static Collider[] OverlapBox(this BoxCollider collider, int layerMask)
        {
            var worldCenter = collider.transform.TransformPoint(collider.center);
            var worldHalfExtents = collider.transform.TransformVector(collider.size * 0.5f);
            var cols = Physics.OverlapBox(worldCenter, worldHalfExtents, collider.transform.rotation, layerMask);
            return cols;
        }
        
        /// <summary>
        /// Gets a random (world) point inside the collider
        /// </summary>
        public static Vector3 GetRandomPointInsideCollider( this BoxCollider boxCollider )
        {
            var extents = boxCollider.size / 2f;
            var point = new Vector3(
                Random.Range( -extents.x, extents.x ),
                Random.Range( -extents.y, extents.y ),
                Random.Range( -extents.z, extents.z )
            )  + boxCollider.center;
            return boxCollider.transform.TransformPoint( point );
        }

        private static (Vector3 point1, Vector3 point2, float radius) GetDataFromCapsule(CapsuleCollider col, float radiusFactor = 1f)
        {
            var transform = col.transform;
            var center = transform.TransformPoint(col.center);
            var scale = transform.lossyScale;
            var radius = col.radius * scale.x;
            var height = col.height * scale.y;
            var bottom = new Vector3(center.x, center.y - height  / 2 + radius, center.z);
            var top = new Vector3(center.x, center.y + height / 2 - radius, center.z);

            return (top, bottom, radius * radiusFactor);
        }
    }
}
