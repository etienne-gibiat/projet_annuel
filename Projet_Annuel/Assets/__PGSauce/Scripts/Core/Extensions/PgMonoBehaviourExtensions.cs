using System;
using System.Collections.Generic;
using PGSauce.Internal.Attributes;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace PGSauce.Core.Extensions
{
    /// <summary>
    /// This class provides useful methods to manipulate MonoBehaviour Components
    /// </summary>
    [Version(0,0,1)]
    public static class PgMonoBehaviourExtensions
    {
        #region Destroy

        /// <summary>
        /// Destroys given object using Object.DestroyImmediate
        /// </summary>
        public static void DestroyImmediate(this Object obj)
        {
            Object.DestroyImmediate(obj);
        }
        
        /// <summary>
        /// Destroys given object using Object.Destroy
        /// </summary>
        public static void Destroy(this Object obj)
        {
            Object.Destroy(obj);
        }
        
        /// <summary>
        /// Destroys given object using either Object.Destroy, or Object.DestroyImmediate,
        /// depending on whether Application.isPlaying is true or not. This is useful when 
        /// writing methods that is used by both editor tools and the game itself.
        /// </summary>
        public static void DestroyUniversal(this Object obj)
        {
            if (Application.isPlaying)
            {
                obj.Destroy();
            }
            else
            {
                obj.DestroyImmediate();
            }
        }

        #endregion
        
        #region Cloning

        /// <summary>
        /// Clones an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CloneMonoBehaviour<T>(this T obj) where T:MonoBehaviour
        {
            return Object.Instantiate(obj);
        }

        /// <summary>
        /// Clones an object X times.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<T> CloneMonoBehaviour<T>(this T obj, int count) where T : MonoBehaviour
        {
            var list = new List<T>();

            for (var i = 0; i < count; i++)
            {
                list.Add(obj.CloneMonoBehaviour<T>());
            }

            return list;
        }

        #endregion

        #region Children
        /// <summary>
        /// Gets a component of the given type, or fail if no such component is attached to the given component.
        /// </summary>
        /// <typeparam name="T">The type of component to get.</typeparam>
        /// <param name="thisComponent">The component to check.</param>
        /// <returns>A component of type T attached to the given component if it exists.</returns>
        /// <exception cref="System.InvalidOperationException">When no component of the required type exist on the given component.</exception>
        public static T GetRequiredComponent<T>(this Component thisComponent) where T : Component
        {
            var retrievedComponent = thisComponent.GetComponent<T>();

            if (retrievedComponent == null)
            {
                throw new InvalidOperationException(
                    $"GameObject \"{thisComponent.name}\" ({thisComponent.GetType()}) does not have a component of type {typeof(T)}");
            }

            return retrievedComponent;
        }

        /// <summary>
        /// Gets a component of the given type in the children, or fail if no such component is found.
        /// </summary>
        /// <typeparam name="T">The type of component to get.</typeparam>
        /// <param name="thisComponent">The component to check.</param>
        /// <returns>A component of type T attached to the given component if it exists.</returns>
        /// <exception cref="System.InvalidOperationException">When no component of the required type exist on the given component.</exception>
        public static T GetRequiredComponentInChildren<T>(this Component thisComponent) where T : Component
        {
            var retrievedComponent = thisComponent.GetComponentInChildren<T>();

            if (retrievedComponent == null)
            {
                throw new InvalidOperationException(
                    $"GameObject \"{thisComponent.name}\" ({thisComponent.GetType()}) does not have a child with component of type {typeof(T)}");
            }

            return retrievedComponent;
        }
        #endregion

        #region Components
        /// <summary>
        /// Sets the graphics alpha color and keeps the same r,g,b
        /// </summary>
        public static void Alpha(this Graphic graphics, float a)
        {
            graphics.color = graphics.color.WithAlpha(a);
        }
        #endregion

        #region Other
        /// <summary>
        /// Checks if layer is contained is mask
        /// </summary>
        public static bool IsInLayerMask(this int layer, LayerMask layermask)
        {
            return layermask == (layermask | (1 << layer));
        }
        /// <summary>
        /// Checks if masks contains layer
        /// </summary>
        public static bool ContainsLayer(this LayerMask layerMask, int layer)
        {
            return layer.IsInLayerMask(layerMask);
        }
        
        /// <summary>
        /// Throws a NullReferenceException if the object is null.
        /// </summary>
        /// <param name="o">An object to check.</param>
        /// <param name="name">The name of the variable this
        /// methods is called on.</param>
        /// <exception cref="NullReferenceException"></exception>
        public static void ThrowIfNull(this object o, string name)
        {
            if(o == null) throw new NullReferenceException(name);
        }
        #endregion
    }
}