using PGSauce.Core.PGDebugging;
using UnityEngine;

namespace PGSauce.Core.Extensions
{
    /// <summary>
    /// Scriptable Objects Extensions
    /// </summary>
    public static class PgScriptableObjectExtensions
    {
        /// <summary>
        /// Returns a clone of the scriptable object.
        /// The clone is stored in the RAM, not saved in the Unity Project
        /// </summary>
        /// <param name="scriptableObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CloneScriptableObject<T>(this T scriptableObject) where T : ScriptableObject
        {
            if (scriptableObject == null)
            {
                PGDebug.Message($"ScriptableObject was null. Returning default {typeof(T)} object.").LogError();
                return (T)ScriptableObject.CreateInstance(typeof(T));
            }
 
            var instance = Object.Instantiate(scriptableObject);
            instance.name = scriptableObject.name; // remove (Clone) from name
            return instance;
        }
    }
}