using UnityEngine;
using UnityEditor;


using AmazingAssets.DynamicRadialMasks;

namespace AmazingAssets.DynamicRadialMasksEditor
{
    [CustomEditor(typeof(DRMAnimatableObjectsPool))]
    public class DRMAnimatableObjectsPoolEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DRMAnimatableObjectsPool t = (target as DRMAnimatableObjectsPool);



            EditorGUILayout.LabelField("Objects in pool", EditorStyles.miniBoldLabel);

            //Anchor
            EditorGUILayout.LabelField(" ");

            if (t != null && t.drmController != null && t.drmAnimatableObjects != null)
            {
                float value = (float)t.drmAnimatableObjects.Count / t.drmController.count;
                EditorGUI.ProgressBar(GUILayoutUtility.GetLastRect(), value, t.drmAnimatableObjects.Count + "/" + t.drmController.count);
            }
            else
            {
                EditorGUI.ProgressBar(GUILayoutUtility.GetLastRect(), 0, "0");
            }

        }
    }
}