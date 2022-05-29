using System;
using System.Collections.Generic;
using System.Linq;
using PGSauce.Internal.Attributes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PGSauce.Core.Extensions
{
    /// <summary>
    /// Extensions for Transform
    /// </summary>
    [Version(0,0,1)]
    public static class PgTransformExtensions
    {
        #region Position

        /// <summary>
        /// Sets the X position of this transform.
        /// </summary>
        public static void X(this Transform transform, float x)
        {
            var pos = transform.position;
            pos.x = x;
            transform.position = pos;
        }

        /// <summary>
        /// Sets the Y position of this transform.
        /// </summary>
        public static void Y(this Transform transform, float y)
        {
            var pos = transform.position;
            pos.y = y;
            transform.position = pos;
        }

        /// <summary>
        /// Sets the Z position of this transform.
        /// </summary>
        public static void Z(this Transform transform, float z)
        {
            var pos = transform.position;
            pos.z = z;
            transform.position = pos;
        }
        
        /// <summary>
        /// Sets the X and Y position of this transform.
        /// </summary>
        public static void SetXY(this Transform transform, float x, float y)
        {
            var newPosition = new Vector3(x, y, transform.position.z);
            transform.position = newPosition;
        }
        
        /// <summary>
        /// Sets the X and Z position of this transform.
        /// </summary>
        public static void SetXZ(this Transform transform, float x, float z)
        {
            var newPosition = new Vector3(x, transform.position.y, z);
            transform.position = newPosition;
        }

        /// <summary>
        /// Sets the Y and Z position of this transform.
        /// </summary>
        public static void SetYZ(this Transform transform, float y, float z)
        {
            var newPosition = new Vector3(transform.position.x, y, z);
            transform.position = newPosition;
        }

        /// <summary>
        /// Sets the X, Y and Z position of this transform.
        /// </summary>
        public static void SetXYZ(this Transform transform, float x, float y, float z)
        {
            var newPosition = new Vector3(x, y, z);
            transform.position = newPosition;
        }

        /// <summary>
        /// Sets the local X position of this transform.
        /// </summary>
        public static void LocalX(this Transform transform, float x)
        {
            var pos = transform.localPosition;
            pos.x = x;
            transform.localPosition = pos;
        }

        /// <summary>
        /// Sets the local Y position of this transform.
        /// </summary>
        public static void LocalY(this Transform transform, float y)
        {
            var pos = transform.localPosition;
            pos.y = y;
            transform.localPosition = pos;
        }

        /// <summary>
        /// Sets the local Z position of this transform.
        /// </summary>
        public static void LocalZ(this Transform transform, float z)
        {
            var pos = transform.localPosition;
            pos.z = z;
            transform.localPosition = pos;
        }
        
        /// <summary>
        /// Sets the local X and Y position of this transform.
        /// </summary>
        public static void SetLocalXY(this Transform transform, float x, float y)
        {
            var newPosition = new Vector3(x, y, transform.localPosition.z);
            transform.localPosition = newPosition;
        }

        /// <summary>
        /// Sets the local X and Z position of this transform.
        /// </summary>
        public static void SetLocalXZ(this Transform transform, float x, float z)
        {
            var newPosition = new Vector3(x, transform.localPosition.z, z);
            transform.localPosition = newPosition;
        }

        /// <summary>
        /// Sets the local Y and Z position of this transform.
        /// </summary>
        public static void SetLocalYZ(this Transform transform, float y, float z)
        {
            var newPosition = new Vector3(transform.localPosition.x, y, z);
            transform.localPosition = newPosition;
        }

        /// <summary>
        /// Sets the local X, Y and Z position of this transform.
        /// </summary>
        public static void SetLocalXYZ(this Transform transform, float x, float y, float z)
        {
            var newPosition = new Vector3(x, y, z);
            transform.localPosition = newPosition;
        }
        
        /// <summary>
        /// Sets the position to 0, 0, 0.
        /// </summary>
        public static void ResetPosition(this Transform transform)
        {
            transform.position = Vector3.zero;
        }

        /// <summary>
        /// Sets the local position to 0, 0, 0.
        /// </summary>
        public static void ResetLocalPosition(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
        }

        #endregion

        #region Scale

        /// <summary>
        /// Sets the local scale of the transform
        /// </summary>
        public static void LocalScale(this Transform t, float scale)
        {
            t.LocalScale(scale, scale, scale);
        }

        /// <summary>
        /// Sets the local scale of the transform
        /// </summary>
        public static void LocalScale(this Transform t, float x, float y, float z)
        {
            t.localScale = new Vector3(x, y ,z);
        }

        /// <summary>
        /// Sets the local X scale of this transform.
        /// </summary>
        public static void LocalScaleX(this Transform t, float x)
        {
            var localScale = t.localScale;
            localScale = new Vector3(x, localScale.y , localScale.z);
            t.localScale = localScale;
        }


        /// <summary>
        /// Sets the local Y scale of this transform.
        /// </summary>
        public static void LocalScaleY(this Transform t, float y)
        {
            var localScale = t.localScale;
            localScale = new Vector3(localScale.x, y , localScale.z);
            t.localScale = localScale;
        }
        
        /// <summary>
        /// Sets the local Z scale of this transform.
        /// </summary>
        public static void LocalScaleZ(this Transform t, float z)
        {
            var localScale = t.localScale;
            localScale = new Vector3(localScale.x, localScale.y , z);
            t.localScale = localScale;
        }
        
        /// <summary>
        /// Sets the local X and Y scale of this transform.
        /// </summary>

        public static void SetScaleXY(this Transform transform, float x, float y)
        {
            var newScale = new Vector3(x, y, transform.localScale.z);
            transform.localScale = newScale;
        }

        /// <summary>
        /// Sets the local X and Z scale of this transform.
        /// </summary>

        public static void SetScaleXZ(this Transform transform, float x, float z)
        {
            var newScale = new Vector3(x, transform.localScale.y, z);
            transform.localScale = newScale;
        }

        /// <summary>
        /// Sets the local Y and Z scale of this transform.
        /// </summary>

        public static void SetScaleYZ(this Transform transform, float y, float z)
        {
            var newScale = new Vector3(transform.localScale.x, y, z);
            transform.localScale = newScale;
        }

        /// <summary>
        /// Sets the local X, Y and Z scale of this transform.
        /// </summary>

        public static void SetScaleXYZ(this Transform transform, float x, float y, float z)
        {
            var newScale = new Vector3(x, y, z);
            transform.localScale = newScale;
        }

        /// <summary>
        /// Sets global scale, not very precise but works most of the time
        /// </summary>
        public static void SetGlobalScale (this Transform transform, Vector3 globalScale)
        {
            transform.localScale = Vector3.one;
            var lossyScale = transform.lossyScale;
            transform.localScale = new Vector3 (globalScale.x/lossyScale.x, globalScale.y/lossyScale.y, globalScale.z/lossyScale.z);
        }
        
        /// <summary>
        /// Resets the local scale of this transform in to 1 1 1.
        /// </summary>

        public static void ResetScale(this Transform transform)
        {
            transform.localScale = Vector3.one;
        }

        #endregion

        #region Rotation
        /// <summary>
		/// Rotates the transform around the X axis.
		/// </summary>
        public static void RotateAroundX(this Transform transform, float angle)
		{
			var rotation = new Vector3(angle, 0, 0);
			transform.Rotate(rotation);
		}

		/// <summary>
		/// Rotates the transform around the Y axis.
		/// </summary>
		public static void RotateAroundY(this Transform transform, float angle)
		{
			var rotation = new Vector3(0, angle, 0);
			transform.Rotate(rotation);
		}

		/// <summary>
		/// Rotates the transform around the Z axis.
		/// </summary>
		public static void RotateAroundZ(this Transform transform, float angle)
		{
			var rotation = new Vector3(0, 0, angle);
			transform.Rotate(rotation);
		}

		/// <summary>
		/// Sets the X rotation.
		/// </summary>
		public static void SetRotationX(this Transform transform, float angle)
		{
			transform.eulerAngles = new Vector3(angle, 0, 0);
		}

		/// <summary>
		/// Sets the Y rotation.
		/// </summary>
		public static void SetRotationY(this Transform transform, float angle)
		{
			transform.eulerAngles = new Vector3(0, angle, 0);
		}

		/// <summary>
		/// Sets the Z rotation.
		/// </summary>
		public static void SetRotationZ(this Transform transform, float angle)
		{
			transform.eulerAngles = new Vector3(0, 0, angle);
		}

		/// <summary>
		/// Sets the local X rotation.
		/// </summary>
		public static void SetLocalRotationX(this Transform transform, float angle)
		{
			transform.localRotation = Quaternion.Euler(new Vector3(angle, 0, 0));
		}

		/// <summary>
		/// Sets the local Y rotation.
		/// </summary>
		public static void SetLocalRotationY(this Transform transform, float angle)
		{
			transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
		}

		/// <summary>
		/// Sets the local Z rotation.
		/// </summary>
		public static void SetLocalRotationZ(this Transform transform, float angle)
		{
			transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}

		/// <summary>
		/// Resets the rotation to 0, 0, 0.
		/// </summary>
		public static void ResetRotation(this Transform transform)
		{
			transform.rotation = Quaternion.identity;
		}

		/// <summary>
		/// Resets the local rotation to 0, 0, 0.
		/// </summary>
		public static void ResetLocalRotation(this Transform transform)
		{
			transform.localRotation = Quaternion.identity;
		}
        #endregion

        #region Local and World Conversions
        
        /// <summary>
        /// Convert Local position to world Space
        /// </summary>
        public static Vector3 GetWorldPosition(this Transform transform, Vector3 localPosition)
        {
	        return transform.TransformPoint(localPosition);
        }

        /// <summary>
        /// Convert World Position to this transform's local Space
        /// </summary>
        public static Vector3 GetLocalPosition(this Transform transform, Vector3 worldPosition)
        {
	        return transform.InverseTransformPoint(worldPosition);
        }

        /// <summary>
        /// Convert Local Direction to world Space
        /// </summary>
        public static Vector3 GetWorldDirection(this Transform transform, Vector3 localDirection)
        {
	        return transform.TransformDirection(localDirection);
        }

        /// <summary>
        /// Convert World Direction to this transform's local Space
        /// </summary>
        public static Vector3 GetLocalDirection(this Transform transform, Vector3 worldDirection)
        {
	        return transform.InverseTransformDirection(worldDirection);
        }
        
        /// <summary>
        /// Convert World Rotation to this transform's local Space
        /// </summary>
        public static Quaternion GetLocalRotation(this Transform transform, Quaternion worldRotation)
        {
	        var originalRotation = transform.rotation;
	        transform.rotation = worldRotation;
	        var localRotation = transform.localRotation;
	        transform.rotation = originalRotation;

	        return localRotation;
        }
        
        /// <summary>
        /// Convert this transform's local Space rotation to World Rotation
        /// </summary>
        public static Quaternion GetWorldRotation(this Transform transform, Quaternion localRotation)
        {
	        var originalRotation = transform.rotation;
	        transform.localRotation = localRotation;
	        var worldRotation = transform.rotation;
	        transform.rotation = originalRotation;

	        return worldRotation;
        }

        #endregion

        #region Children

        /// <summary>
        /// Gets all the children of the transform and destroys them
        /// </summary>
        public static void DestroyChildren(this Transform transform)
        {
	        var children = transform.GetChildren();
			foreach (var child in children)
			{
				child.gameObject.Destroy();
			}
		}

        /// <summary>
        /// Gets all the children of the transform and destroys them immediately
        /// </summary>
		public static void DestroyChildrenImmediate(this Transform transform)
		{
			var children = transform.GetChildren();

			foreach (var child in children)
			{
				child.gameObject.DestroyImmediate();
			}
		}

		/// <summary>
		/// Destroys children using either Object.Destroy, or Object.DestroyImmediate,
		/// depending on whether Application.isPlaying is true or not. This is useful when 
		/// writing methods that is used by both editor tools and the game itself.
		/// </summary>
		public static void DestroyUniversal(this Transform transform)
		{
			if (Application.isPlaying)
			{
				transform.DestroyChildren();
			}
			else
			{
				transform.DestroyChildrenImmediate();
			}
		}

		/// <summary>
		/// Gets all the children of the transform
		/// </summary>
		public static List<Transform> GetChildren(this Transform transform)
		{
			var children = new List<Transform>();

			for (var i = 0; i < transform.childCount; i++)
			{
				var child = transform.GetChild(i);
				children.Add(child);
			}

			return children;
		}

		/// <summary>
		/// Sorts all the children of the transform
		/// </summary>
		/// <param name="transform"></param>
		/// <param name="sortFunction">How to sort the children</param>
		public static void SortChildren(this Transform transform, Func<Transform, IComparable> sortFunction)
		{
			var children = transform.GetChildren();
			var sortedChildren = children.OrderBy(sortFunction).ToList();

			for (var i = 0; i < sortedChildren.Count(); i++)
			{
				sortedChildren[i].SetSiblingIndex(i);
			}
		}

		/// <summary>
		/// Sorts the children by name
		/// </summary>
		/// <param name="transform"></param>
		public static void SortChildrenAlphabetically(this Transform transform)
		{
			transform.SortChildren(t => t.name);
		}

		/// <summary>
		/// A lazy enumerable of this objects transform, and all it's children down the hierarchy.
		/// </summary>
		public static IEnumerable<Transform> SelfAndAllChildren(this Transform transform)
		{
			var openList = new Queue<Transform>();

			openList.Enqueue(transform);

			while (openList.Any())
			{
				var currentChild = openList.Dequeue();

				yield return currentChild;

				var children = transform.GetChildren();

				foreach (var child in children)
				{
					openList.Enqueue(child);
				}
			}
		}

        #endregion

        #region Geometry
        
        /// <summary>
        /// Is the target object left, right, or in front this transform ?
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="target"></param>
        /// <returns>-1 = left, 1 = right, 0 = in front (or behind)</returns>
        public static float RelativeOrientation(this Transform transform, Transform target)
        {
	        var targetDir = (target.position - transform.position).normalized;
	        var up = transform.up;
	        var forward = transform.forward;

	        var perp = Vector3.Cross(forward, targetDir);
	        var dir = Vector3.Dot(perp, up);
	        if (dir > 0f)
	        {
		        return 1f;
	        }
	        else if (dir < 0f)
	        {
		        return -1f;
	        }
	        else
	        {
		        return 0f;
	        }
        }

        /// <summary>
        /// Is The target in front of this transform ? How much ?
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="target"></param>
        /// <returns>1 = right in front, -1 right behind, anything in between</returns>
        public static float InFront(this Transform transform, Transform target)
        {
	        var toTarget = (target.position - transform.position).normalized;
	        return Vector3.Dot(toTarget, transform.forward);
        }

        #endregion
        
        #region Other
        /// <summary>
        /// Resets the ;local position, local rotation, and local scale.
        /// </summary>

        public static void ResetLocal(this Transform transform)
        {
	        transform.ResetLocalRotation();
	        transform.ResetLocalPosition();
	        transform.ResetScale();

        }

        /// <summary>
        /// Resets the position, rotation, and local scale.
        /// </summary>

        public static void Reset(this Transform transform)
        {
	        transform.ResetRotation();
	        transform.ResetPosition();
	        transform.ResetScale();
        }
        
        /// <summary>
        /// Gets the hierarchy path
        /// </summary>
        public static string GetTransformPath(this Transform current) {
	        if (current.parent == null)
		        return "/" + current.name;
	        return current.parent.GetTransformPath() + "/" + current.name;
        }
        #endregion
    }
}