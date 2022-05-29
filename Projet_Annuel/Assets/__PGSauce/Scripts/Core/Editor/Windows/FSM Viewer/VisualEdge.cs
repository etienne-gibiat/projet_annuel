using UnityEngine;

namespace PGSauce.Core.PGEditor
{
    public class VisualEdge
    {
        private DependencyEdge _edge;
        private static Texture _arrowTexture;
        
        public Vector2 TargetPosition { get; set; }

        public Vector2 SourcePosition { get; set; }

        static VisualEdge()
        {
	        _arrowTexture = Resources.Load (FSMViewerEditorWindow.ArrowIconName) as Texture;
        }

        public VisualEdge(DependencyEdge edge)
        {
            _edge = edge;
        }

        public void DrawEdge(Graph graph)
        {
            var graphOffset = graph.GraphOffset;
            var source = (_edge.Source.Position + new Vector2(50, 17.5f) + graphOffset) * VisualNode.SizeMultiplier;
            var target = (_edge.Target.Position + new Vector2(50, 17.5f) + graphOffset) * VisualNode.SizeMultiplier;

            var source2 = FindEdgePoint (source, target);
            var target2 = FindEdgePoint (target, source);
            
            source = source2;
            target = target2;

            SourcePosition = source;
            TargetPosition = target;
            
            var handlesDefaultColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = Color.white;
            UnityEditor.Handles.DrawLine (source, target);
            
            
            //var labelPosition = ((target - source) / 2f) + source - Vector2.one * 7.5f;
            //labelPosition.x += 20f;
            
            //GUI.Label (new Rect (labelPosition.x, labelPosition.y, 100f, 100f), _edge.Decision.DecisionName);
            
            UnityEditor.Handles.color = handlesDefaultColor;
            graph.GraphOffset = graphOffset;
        }
        
        public void DrawArrow(Graph graph)
        {
	        var sourceCenter = SourcePosition;
	        var targetCenter = TargetPosition;
	        var delta = targetCenter - sourceCenter;
	        var center = 1.25f * (delta / 2f) + sourceCenter -
	                     Vector2.one * 7.5f * VisualNode.SizeMultiplier;
	        var angle = 180f + Mathf.Acos(Vector2.Dot(Vector2.up, delta) / delta.magnitude) * Mathf.Rad2Deg;
	        if (targetCenter.x > sourceCenter.x)
	        {
		        angle = 360f - angle;
	        }

	        var pivot = center + Vector2.one * 7.5f * VisualNode.SizeMultiplier;
	        if (!float.IsNaN(angle))
	        {
		        GUIUtility.RotateAroundPivot(angle, pivot);
	        }
	        
	        GUI.DrawTexture(
		        new Rect(center.x, center.y, 15f * VisualNode.SizeMultiplier,
			        15f * VisualNode.SizeMultiplier), _arrowTexture);
	        
	        if (!float.IsNaN(angle))
	        {
		        GUIUtility.RotateAroundPivot(-angle, pivot);
	        }
        }

        private static Vector2 FindEdgePoint (Vector2 source, Vector2 target)
        		{
        			var rectSize = new Vector2 (50f, 17.5f) * VisualNode.SizeMultiplier;
                    var y1 = target.y;
        			var y0 = source.y;
        			var x1 = target.x;
        			var x0 = source.x;
        			var m = (y1 - y0) / (x1 - x0);
        			var centerToCorner = rectSize.y / rectSize.x;
        			var vec = Vector2.zero;
        			if (y1 > y0 && x1 > x0) {
        				if (m > centerToCorner) {
        					vec.y = source.y + rectSize.y;
        					vec.x = (vec.y - y0 + m * x0) / m;
        				} else {
        					vec.x = source.x + rectSize.x;
        					vec.y = m * (vec.x - x0) + y0;
        				}
        			}
        			if (y1 >= y0 && x1 <= x0) {
        				if (m < -centerToCorner) {
        					vec.y = source.y + rectSize.y;
        					vec.x = (vec.y - y0 + m * x0) / m;
        				} else {
        					vec.x = source.x - rectSize.x;
        					vec.y = m * (vec.x - x0) + y0;
        				}
        			}
        			if (y1 <= y0 && x1 <= x0) {
        				if (m < centerToCorner) {
        					vec.x = source.x - rectSize.x;
        					vec.y = m * (vec.x - x0) + y0;
        				} else {
        					vec.y = source.y - rectSize.y;
        					vec.x = (vec.y - y0 + m * x0) / m;
        				}
        			}
        			if (x1 >= x0 && y1 <= y0) {
        				if (m < -centerToCorner) {
        					vec.y = source.y - rectSize.y;
        					vec.x = (vec.y - y0 + m * x0) / m;
        				} else {
        					vec.x = source.x + rectSize.x;
        					vec.y = m * (vec.x - x0) + y0;
        				}
        			}
        			return vec;
        		}

        
    }
}