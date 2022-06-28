using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace _Elementis.Scripts.River_Editor
{
    [CustomEditor(typeof(RiverPath)), CanEditMultipleObjects]
    public class RiverPathEditor : Editor
    {
        private RiverPath _river;

        protected /*override*/ void OnDisable()
        {
            //base.OnDisable();
            SceneView.duringSceneGui -= DuringSceneGUI;
        }

        protected /*override*/ void OnEnable()
        {
            //base.OnEnable();
            SceneView.duringSceneGui -= DuringSceneGUI;
            SceneView.duringSceneGui += DuringSceneGUI;
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.Space();

            _river = (RiverPath) target;

            EditorGUILayout.Space();
            
            if (GUILayout.Button("Generate"))
            {
                _river.GenerateRiver();
            }
        }

        private void DuringSceneGUI(SceneView obj)
        {
            if (_river == null)
            {
                _river = (RiverPath) target;
            }
            
            if (_river != null)
            {
                if (Event.current.commandName == "UndoRedoPerformed")
                {
                    _river.GenerateRiver();
                    return;
                }

                for (var j = 0; j < _river.controlPoints.Count; j++)
                {
                    EditorGUI.BeginChangeCheck();
                    var handlePos = (Vector3) _river.controlPoints[j].position + _river.transform.position;

                    var style = new GUIStyle
                    {
                        normal =
                        {
                            textColor = Color.red
                        }
                    };

                    var screenPoint = Camera.current.WorldToScreenPoint(handlePos);

                    if (screenPoint.z > 0)
                    {
                        Handles.Label(handlePos + Vector3.up * HandleUtility.GetHandleSize(handlePos), $"Point {j}",
                            style);
                    }

                    var width = _river.controlPoints[j].position.w;
                    if (Event.current.control && Event.current.shift && _river.controlPoints.Count > 1)
                    {
                        var id = GUIUtility.GetControlID(FocusType.Passive);
                        Handles.color = Handles.xAxisColor;

                        var size = 0.6f;
                        size = HandleUtility.GetHandleSize(handlePos) * size;
                        if (Event.current.type == EventType.Repaint)
                        {
                            Handles.SphereHandleCap(id,
                                (Vector3) _river.controlPoints[j].position + _river.transform.position,
                                Quaternion.identity, size, EventType.Repaint);
                        }
                        else if (Event.current.type == EventType.Layout)
                        {
                            Handles.SphereHandleCap(id,
                                (Vector3) _river.controlPoints[j].position + _river.transform.position,
                                Quaternion.identity, size, EventType.Layout);
                        }
                    }
                    else if (Tools.current == Tool.Move)
                    {
                        float size = 0.6f;
                        size = HandleUtility.GetHandleSize(handlePos) * size;

                        Handles.color = Handles.xAxisColor;
                        Vector4 pos =
                            Handles.Slider((Vector3) _river.controlPoints[j].position + _river.transform.position,
                                Vector3.right, size, Handles.ArrowHandleCap, 0.01f) - _river.transform.position;
                        Handles.color = Handles.yAxisColor;
                        pos = Handles.Slider((Vector3) pos + _river.transform.position, Vector3.up, size,
                            Handles.ArrowHandleCap, 0.01f) - _river.transform.position;
                        Handles.color = Handles.zAxisColor;
                        pos = Handles.Slider((Vector3) pos + _river.transform.position, Vector3.forward, size,
                            Handles.ArrowHandleCap, 0.01f) - _river.transform.position;

                        Vector3 halfPos = (Vector3.right + Vector3.forward) * size * 0.3f;
                        Handles.color = Handles.yAxisColor;
                        pos = Handles.Slider2D((Vector3) pos + _river.transform.position + halfPos, Vector3.up,
                                  Vector3.right, Vector3.forward, size * 0.3f, Handles.RectangleHandleCap, 0.01f) -
                              _river.transform.position - halfPos;
                        halfPos = (Vector3.right + Vector3.up) * size * 0.3f;
                        Handles.color = Handles.zAxisColor;
                        pos = Handles.Slider2D((Vector3) pos + _river.transform.position + halfPos, Vector3.forward,
                                  Vector3.right, Vector3.up, size * 0.3f, Handles.RectangleHandleCap, 0.01f) -
                              _river.transform.position - halfPos;
                        halfPos = (Vector3.up + Vector3.forward) * size * 0.3f;
                        Handles.color = Handles.xAxisColor;
                        pos = Handles.Slider2D((Vector3) pos + _river.transform.position + halfPos, Vector3.right,
                                  Vector3.up, Vector3.forward, size * 0.3f, Handles.RectangleHandleCap, 0.01f) -
                              _river.transform.position - halfPos;

                        pos.w = width;
                        _river.controlPoints[j].position = pos;
                    }
                    else if (Tools.current == Tool.Scale)
                    {

                        Handles.color = Handles.xAxisColor;
                        //Vector3 handlePos = (Vector3)spline.controlPoints [j] + spline.transform.position;

                        width = Handles.ScaleSlider(_river.controlPoints[j].position.w, (Vector3)_river.controlPoints[j].position + _river.transform.position, new Vector3(0, 0.5f, 0),
                            Quaternion.Euler(-90, 0, 0), HandleUtility.GetHandleSize(handlePos), 0.01f);

                        Vector4 pos = _river.controlPoints[j].position;
                        pos.w = width;
                        _river.controlPoints[j].position = pos;

                    }


                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(_river, "Change Position");
                        _river.GenerateRiver();
                    }
                }
            }
        }

        
    }
}