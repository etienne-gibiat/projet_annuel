using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PGSauce.AudioManagement;
using PGSauce.AudioManagement.External;
using PGSauce.Core;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Strings;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace PGSauce.Windows
{
    public class GenerateSfxObjectsWindow : OdinEditorWindow
    {
        [ShowInInspector, ReadOnly]
        private string _localSelectedPath;

        [MenuItem(MenuPaths.Audio + "Generate SFX Objects")]
        public static void ShowWindow()
        {
            GenerateSfxObjectsWindow window = GetWindow<GenerateSfxObjectsWindow>();
            window.titleContent = new GUIContent("GenerateSFXObjects");
            window.Show();
        }
        
        [OnInspectorGUI]
        private void OnInspectorGUI()
        {
            UpdateSelectedPath();
        }

        [Button]
        private void GenerateSfxObjects(List<AudioClip> clips)
        {
            var sos = new List<PgSfxObject>();
            var names = new List<string>();
            for (var i = 0; i < clips.Count; i++)
            {
                if (clips[i] == null)
                {
                    PGDebug.Message($"Clip {i} is null").LogWarning();
                    continue;
                }

                var so = ScriptableObject.CreateInstance<PgSfxObject>();

                if (so.SFXLayers.Length <= 0)
                {
                    so.SFXLayers = new SFXLayer[1];
                    so.SFXLayers[0] = new SFXLayer()
                    {
                        SFX = clips[i],
                        FixedVolume = 1,
                        FixedPitch = 1,
                        Priority = 128,
                        MinDistance = 1,
                        MaxDistance = 500
                    };
                }
                else
                {
                    so.SFXLayers[0].SFX = clips[i];
                }
                
                
                
                sos.Add(so);
                names.Add($"{clips[i].name}.asset");
            }
            
            UpdateSelectedPath();
            
            PGAssets.SaveAssetsToEditor(sos, names, _localSelectedPath);
        }
        
        private void UpdateSelectedPath()
        {
            var folderPath = $"Assets/{PGAssets.GetPathUnderMouse(true)}";
            _localSelectedPath = folderPath;
        }
    }
}
