using System.Linq;
using UnityEditor;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    [CustomEditor(typeof(WfcTileBase), true)]
    [CanEditMultipleObjects]
    public class WfcTileEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var tileBase = (WfcTileBase)target;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("palette"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("center"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tileSize"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rotatable"));
            if (serializedObject.targetObjects.Cast<WfcTileBase>().Any(t => t.rotatable))
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationGroupType"));
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("reflectable"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("symmetric"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("instantiateChildrenOnly"));

            var meshFilter = tileBase.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                if (meshFilter.sharedMesh.hideFlags == HideFlags.HideAndDontSave)
                {
                    if (WfcGeneratorEditor.HelpBoxWithButton("Mesh is flagged as HideAndDontSave", "Save it", MessageType.Info))
                    {
                        foreach(var t in targets)
                        {
                            foreach (var mf in ((WfcTileBase)t).GetComponents<MeshFilter>())
                            {
                                meshFilter.sharedMesh.hideFlags = HideFlags.None;
                            }
                        }
                    }
                }
            }


            serializedObject.ApplyModifiedProperties();

            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Paint");
            EditorGUILayout.EditorToolbar(WfcTileEditorToolBase.tile3dEditorTools);
            GUILayout.EndHorizontal();
        }
    }
}