using UnityEditor;
using UnityEngine;

namespace PGSauce.Core.PGEditor
{
    /// <summary>
    /// Draws a grid background in Editor Window
    /// </summary>
    public class Background
    {
        private readonly FSMViewerEditorWindow _window;
        private Rect _rect;

        public Background(ref FSMViewerEditorWindow window)
        {
            _window = window;
        }

        public void Draw(Graph graph)
        {
            var offset = graph.GraphOffset;
            var size = _window.position.size + Vector2.one * FSMViewerEditorWindow.OverDrawSize;
            var position = -Vector2.one * FSMViewerEditorWindow.OverDrawSize;

            _rect = new Rect(position.x, position.y, size.x, size.y);
            var skin = GUI.skin;
            GUI.skin = null;
            var defaultColor = GUI.color;
            GUI.color = new Color(.102f, .102f, .102f);
            GUI.Box(_rect, "");
            GUI.color = defaultColor;
            DrawLines(offset);
            GUI.skin = skin;
        }

        private void DrawLines(Vector2 offset)
        {
            var size = _window.position.size;
            const float lineOffset = FSMViewerEditorWindow.LineOffset;
            var horizontalLines = Mathf.FloorToInt (size.y / lineOffset);
            var verticalLines = Mathf.FloorToInt (size.x / lineOffset);

            var defaultColor = Handles.color;
            Handles.color = new Color(0.2f, 0.2f, 0.2f);
            
            offset.x = Mathf.FloorToInt (offset.x) % (Mathf.FloorToInt (lineOffset) - 1);
            offset.y = Mathf.FloorToInt (offset.y) % (Mathf.FloorToInt (lineOffset) - 1);
            for (var i = -1; i < horizontalLines + 2; i++) {

                Vector2 start = new Vector3 (-lineOffset, i * lineOffset);
                Vector2 end = new Vector3 (size.x + lineOffset, i * lineOffset);
                Handles.DrawLine (start + offset, end + offset);

            }
            for (var i = -1; i < verticalLines + 2; i++) {

                Vector2 start = new Vector3 (i * lineOffset, -lineOffset);
                Vector2 end = new Vector3 (i * lineOffset, size.y + lineOffset);
                Handles.DrawLine (start + offset, end + offset);
            }
            Handles.color = defaultColor;
        }
    }
}