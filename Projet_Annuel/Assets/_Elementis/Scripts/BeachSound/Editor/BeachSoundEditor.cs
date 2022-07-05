using System;
using PGSauce.Unity;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace _Elementis.Scripts.Beach_Sound
{
    [EditorTool(nameof(BeachSoundEditor))]
    public class BeachSoundEditor : EditorTool
    {
        private static float height;
        private const float windowWidth = 380f;
        
        public static BeachSound BeachSound { get; private set; }
        
        private static class LastHit
        {
            public static Vector3 position { get; private set; }
            public static Vector3 normal { get; private set; }
            public static float distance { get; private set; }

            public static void Update(Vector3 position, Vector3 normal, float distance)
            {
                LastHit.position = position;
                LastHit.normal = normal;
                LastHit.distance = distance;
            }
        }
        
        private void OnEnable()
        {
            
        }

        public override void OnActivated()
        {
            base.OnActivated();
        }

        public override void OnWillBeDeactivated()
        {
            base.OnWillBeDeactivated();
        }

        public override void OnToolGUI(EditorWindow window)
        {
            base.OnToolGUI(window);
            Handles.BeginGUI();
            GUILayout.Window(0, new Rect(10, 30, windowWidth, 0), id => OnGUI(), GUIContent.none);
            Handles.EndGUI();

            DoLogic(SceneView.currentDrawingSceneView);
        }

        private void DoLogic(SceneView sceneView)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            var e = Event.current;
            EditorGUIUtility.AddCursorRect(sceneView.position, MouseCursor.MoveArrow);
            var ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            var plane = new Plane(Vector3.up, height);
            if (plane.Raycast(ray, out var distance))
            {
                LastHit.Update(ray.GetPoint(distance), Vector3.up, distance);
            }
            CheckMouseClicks();
        }
        
        private void CheckMouseClicks()
        {
            var e = Event.current;
            if (e.type == EventType.MouseDown)
            {
                if (e.button == 0)
                {
                    BeachSound.AddPoint(LastHit.position);
                }
                else if (e.button == 1)
                {
                    BeachSound.TryRemovePoint(LastHit.position);
                }
            }
        }


        private void OnGUI()
        {
            BeachSound =
                (BeachSound) EditorGUILayout.ObjectField(new GUIContent("Beach Sound Instance"), BeachSound,
                    typeof(BeachSound), true);
            GUILayout.Label("Height", GUILayout.Width(75));
            height = EditorGUILayout.FloatField(height);
        }
    }
}