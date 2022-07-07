using UnityEditor;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    [CustomEditor(typeof(WfcAnimatedGenerator))]
    public class WfcAnimatedGeneratorEditor : Editor
    {
         public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var ag = (WfcAnimatedGenerator)target;
            bool showStart = false;
            bool showStop = false;
            bool showPause = false;
            bool showResume = false;

            switch (ag.State)
            {
                case WfcAnimatedGenerator.AnimatedGeneratorState.Stopped:
                    showStart = true;
                    break;
                case WfcAnimatedGenerator.AnimatedGeneratorState.Initializing:
                    showStop = true;
                    break;
                case WfcAnimatedGenerator.AnimatedGeneratorState.Running:
                    showStop = showPause = true;
                    break;
                case WfcAnimatedGenerator.AnimatedGeneratorState.Paused:
                    showStop = showResume = true;
                    break;
            }

            if(showPause)
            {

                if (GUILayout.Button("Pause"))
                {
                    ag.PauseGeneration();

                    if (!Application.isPlaying)
                    {
                        EditorApplication.update -= ag.Step;
                    }
                }
            }

            if(showStop)
            {
                if (GUILayout.Button("Stop"))
                {
                    ag.StopGeneration();

                    if (!Application.isPlaying)
                    {
                        EditorApplication.update -= ag.Step;
                    }
                }
            }

            if(showResume)
            {
                if (GUILayout.Button("Resume"))
                {
                    ag.ResumeGeneration();

                    if (!Application.isPlaying)
                    {
                        EditorApplication.update += ag.Step;
                    }
                }
            }

            if (showStart)
            {
                if (GUILayout.Button("Start"))
                {
                    ag.StartGeneration();

                    if (!Application.isPlaying)
                    {
                        EditorApplication.update += ag.Step;
                    }
                }
            }
        }
    }
}