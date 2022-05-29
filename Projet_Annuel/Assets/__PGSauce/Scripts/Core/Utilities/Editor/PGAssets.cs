using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#if UNITY_EDITOR
using PGSauce.Core.PGDebugging;
using UnityEditor;
#endif

using UnityEngine;
using Object = UnityEngine.Object;

#pragma warning disable 162

namespace PGSauce.Core
{
    /// <summary>
    /// Utility class to manipulate files and assets in Unity Editor
    /// </summary>
    public static class PGAssets
    {
        /// <summary>
        /// The absolute path to the Unity Assets Folder
        /// </summary>
        public static string AssetsDirPath => Application.dataPath;
        /// <summary>
        /// The absolute path to the Unity Project Folder (containing Assets, Packages, Project Settings etc.)
        /// </summary>
        public static string ProjectDirPath => Directory.GetParent(AssetsDirPath).FullName;
        
        /// <summary>
        /// Gets the LOCAL path to asset or folder selected in the project
        /// </summary>
        /// <param name="sanitized">Should the path be sanitized ? (<see cref="SanitizePath"/>)</param>
        /// <returns>The path under the mouse</returns>
        public static string GetPathUnderMouse(bool sanitized = true)
        {
#if UNITY_EDITOR
            var folderPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
            if(sanitized) {folderPath = SanitizePath(folderPath);}
            return folderPath;
#endif
            return "";
        }

        /// <summary>
        /// Removes anything useless in the path. Assets/My Game/Scripts/Player.cs would become MyGame/Scripts
        /// </summary>
        /// <param name="folderPath">The path to sanitize</param>
        /// <returns>The sanitized path</returns>
        public static string SanitizePath(string folderPath)
        {
#if UNITY_EDITOR
            if (folderPath.Contains("."))
                folderPath = folderPath.Remove(folderPath.LastIndexOf('/'));
            folderPath = folderPath.Replace("Assets/", "");
            folderPath = folderPath.Replace("Assets", "");
            return folderPath;
#endif
            return "";
        }

        /// <summary>
        /// Gets the LOCAL path to asset
        /// </summary>
        /// <param name="obj">The asset to have the path</param>
        /// <param name="sanitized">Should the path be sanitized ? (<see cref="SanitizePath"/>)</param>
        /// <returns>The asset path</returns>
        public static string GetAssetPath(Object obj, bool sanitized = true)
        {
#if UNITY_EDITOR
            var folderPath = AssetDatabase.GetAssetPath(obj);
            if(sanitized) {folderPath = SanitizePath(folderPath);}
            return folderPath;
#endif
            return "";
        }
        
        /// <summary>
        /// Writes a file to the location
        /// </summary>
        /// <param name="content">The content of the file</param>
        /// <param name="intoPath">The folder where it should be written</param>
        /// <param name="fileName">The filename with its extension</param>
        public static void AbstractGenerateOneFile(string content, string intoPath, string fileName)
        {
#if UNITY_EDITOR
            if (!Directory.Exists(intoPath))
            {
                Directory.CreateDirectory(intoPath);
            }
            
            intoPath = Path.Combine(intoPath, fileName);
            
            PGDebug.Message($"Generation path is {intoPath}").Log();
            
            File.WriteAllText(intoPath, content);
#endif
        }
        /// <summary>
        /// Tells how to format a list of strings
        /// For example we could return the value + '-', except if the index is 2 or the value is "Foo"
        /// </summary>
        public delegate string Format(string value, int index);
        /// <summary>
        /// Concatenates the list of string with rules being given by the formatter. For example if values is ["a","b","c","d","e"] and formatter tells we should add a "-" between each except
        /// between "b" and "c" then the results is "a-bc-d-e".
        /// </summary>
        /// <param name="values">The string list</param>
        /// <param name="formatter">How to format ths list</param>
        /// <returns>The concatenated string</returns>
        public static string AbstractFormatting(IReadOnlyList<string> values, Format formatter)
        {
#if UNITY_EDITOR
            var sb = new StringBuilder();
            
            for (var i = 0; i < values.Count; i++)
            {
                sb.Append(formatter(values[i], i)) ;
            }
            
            return sb.ToString();
#endif
            return "";
        }

        /// <summary>
        /// Save Unity Project Database
        /// </summary>
        public static void SaveUnityAssetDatabase()
        {
#if UNITY_EDITOR
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
            return;
        }

        /// <summary>
        /// Checks if the path is inside PG Sauce folder
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsInsidePgSauce(string path)
        {
#if UNITY_EDITOR
            return path.Contains("__PGSauce");
#endif
            return false;
        }
        
        /// <summary>
        /// Finds all assets in editor corresponding to the filter. It is like typing directly the filter in the editor
        /// </summary>
        /// <param name="filter">The type of the asset</param>
        /// <typeparam name="T">The type of the asset</typeparam>
        /// <returns>The list of all the existing assets</returns>
        public static T[] GetAllInstances<T>(Type filter) where T : ScriptableObject
        {
            var guids = AssetDatabase.FindAssets("t:"+ filter.Name);  //FindAssets uses tags check documentation for more info
            var a = new T[guids.Length];
            for(var i =0;i<guids.Length;i++)         //probably could get optimized 
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }
 
            return a;
 
        }

        /// <summary>
        /// Save procedurally created assets to the editor
        /// </summary>
        /// <param name="assets">The list of assets</param>
        /// <param name="names">Their names with extension</param>
        /// <param name="folderPath">Where to save them</param>
        /// <typeparam name="T">The type of the asset (ScriptableObject for example)</typeparam>
        public static void SaveAssetsToEditor<T>(List<T> assets, List<string> names, string folderPath) where T : Object
        {
            for (var index = 0; index < assets.Count; index++)
            {
                var asset = assets[index];
                AssetDatabase.CreateAsset(asset, $"{folderPath}/{names[index]}");
            }

            SaveUnityAssetDatabase();
        }
    }
}