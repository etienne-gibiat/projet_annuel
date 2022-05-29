using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.FSM.Base;
using PGSauce.Core.FSM.WithSo;
using UnityEngine;

namespace PGSauce.Core.PGEditor
{
    public class Graph
    {
        private const float OffsetLerp = 0.05f;
        
        private List<Node> _nodes;
        private List<Edge> _edges;
        public Vector2 GraphOffset { get; set; }

        public List<Node> Nodes => _nodes;

        public List<Edge> Edges => _edges;

        public Graph()
        {
            GraphOffset = Vector2.zero;
            _nodes = new List<Node>();
            _edges = new List<Edge>();
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }

        public Node GetNode(AbstractState fromState)
        {
            return Nodes.FirstOrDefault(node => node.State == fromState);
        }

        public void MoveTo(Vector2 targetGraphOffset)
        {
            GraphOffset = Vector2.Lerp(GraphOffset, targetGraphOffset, OffsetLerp);
        }
    }
}