using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;


namespace AmazingAssets.DynamicRadialMasksEditor
{
    public static class EditorUtilities
    {
        public enum RenderPipeline { Builtin, Universal, HighDefinition }

        static public char[] invalidFileNameCharachters = Path.GetInvalidFileNameChars();

        static string thisAssetPath;



        static public string GetThisAssetFolderPath()
        {
            if (string.IsNullOrEmpty(thisAssetPath))
            {
                string[] assets = AssetDatabase.FindAssets("EditorUtilities");

                if (assets != null && assets.Length > 0)
                {
                    for (int i = 0; i < assets.Length; i++)
                    {
                        if (string.IsNullOrEmpty(assets[i]) == false)
                        {
                            string currentFilePath = AssetDatabase.GUIDToAssetPath(assets[i]);

                            if (currentFilePath.Contains("Amazing Assets") &&
                                currentFilePath.Contains("Dynamic Radial Masks") &&
                                currentFilePath.Contains("Editor") &&
                                currentFilePath.Contains("Base") &&
                                Path.GetExtension(currentFilePath) == ".cs")

                            {
                                thisAssetPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(currentFilePath)));
                                break;
                            }
                        }
                    }
                }
            }

            return thisAssetPath;
        }


        static public RenderPipeline GetCurrentRenderPipeline()
        {
            if (UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset == null)
                return RenderPipeline.Builtin;
            else
            {
                if (UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset.name.Contains("Universal") ||
                    UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset.name.Contains("URP"))
                    return RenderPipeline.Universal;
                else
                    return RenderPipeline.HighDefinition;
            }
        }


        static public string RemoveInvalidCharacters(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            else
            {
                if (name.IndexOfAny(invalidFileNameCharachters) == -1)
                    return name;
                else
                    return string.Concat(name.Split(invalidFileNameCharachters, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        static public bool ContainsInvalidFileNameCharacters(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else
                return name.IndexOfAny(invalidFileNameCharachters) >= 0;
        }
    }
}