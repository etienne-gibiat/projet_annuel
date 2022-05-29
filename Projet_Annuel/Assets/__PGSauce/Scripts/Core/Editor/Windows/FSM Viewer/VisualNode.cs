using UnityEngine;

namespace PGSauce.Core.PGEditor
{
    public class VisualNode
    {
        private Rect _entityRect;
        private Rect _drawableRect;
        
        private readonly Node _node;
        public static int FontSize = 8;
        public static float SizeMultiplier { get; set; } = 1f;
        public string Title => _node.State.Name();

        public Rect DrawableRect => _drawableRect;

        public Node NodeInstance => _node;
        
        public VisualNode(Node node)
        {
            _node = node;
            _node.VisualNodeInstance = this;
            
            _entityRect = new Rect (node.Position.x, node.Position.y, 100, 35);
            _drawableRect = _entityRect;
        }

        public void Draw(Graph graph)
        {
            var offset = graph.GraphOffset;
            _entityRect.position = _node.Position + offset;

            var f = Title.Length / 20f;
            FontSize = 3 - Mathf.RoundToInt(f);
            FontSize += Mathf.RoundToInt (8f * SizeMultiplier);
            GUI.skin.box.fontSize = FontSize;


            _drawableRect = _entityRect;
            _drawableRect.position *= SizeMultiplier;
            _drawableRect.size *= SizeMultiplier;
            GUI.Box(_drawableRect, Title);
            GUI.skin.box.fontSize = 11;
        }

        
    }
}