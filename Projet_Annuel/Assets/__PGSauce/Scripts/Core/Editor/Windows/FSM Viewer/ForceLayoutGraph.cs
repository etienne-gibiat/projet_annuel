using System.Linq;

namespace PGSauce.Core.PGEditor
{
    public class ForceLayoutGraph
    {
        private const float Threshold = FSMViewerEditorWindow.ForceLayoutThreshold;
        private const float Repulsion = FSMViewerEditorWindow.ForceLayoutRepulsion;
        private Graph _graph;
        private readonly int _maxCycles;
        private int _count;
        public bool WithinThreshold { get; set; }

        public int Count
        {
            get => _count;
            set => _count = value;
        }

        public int MaxCycles => _maxCycles;

        public ForceLayoutGraph(Graph graph, int maximumCycles)
        {
            _graph = graph;
            _maxCycles = maximumCycles;
            _count = 0;
        }


        public void Update(float timeStep)
        {
            if (!WithinThreshold) {
                Repulse ();
                Attract ();
                UpdateVelocity (timeStep);
                UpdatePosition (timeStep);
            }
            _count++;

            WithinThreshold = TotalEnergy () < Threshold || _count > _maxCycles;
        }

        private float TotalEnergy()
        {
            return _graph.Nodes.Select(node => node.Speed).Select(speed => 0.5f * 3f * speed * speed).Sum();
        }

        private void UpdatePosition(float timeStep)
        {
            foreach (var node in _graph.Nodes) {
                node.UpdatePosition (timeStep);
            }
        }

        private void UpdateVelocity(float timeStep)
        {
            foreach (var node in _graph.Nodes) {
                node.UpdateVelocity (timeStep, FSMViewerEditorWindow.ForceLayoutDamping);
            }
        }

        private void Attract()
        {
            foreach (var edge in _graph.Edges)
            {
                var delta = edge.Target.Position - edge.Source.Position;
                var displacement = Edge.Length - delta.magnitude ;
                var direction = delta.normalized;

                var attractForce = direction * (edge.Stiffness * displacement * 0.5f);
                
                edge.Source.AddForce(-attractForce);
                edge.Target.AddForce(attractForce);
            }

            foreach (var node in _graph.Nodes)
            {
                var direction = -node.Position;
                var displacement = direction.magnitude;
                direction.Normalize();
                node.AddForce(direction * (FSMViewerEditorWindow.Stiffness * displacement * 0.4f));
            }
        }

        private void Repulse()
        {
            foreach (var nodeA in _graph.Nodes)
            {
                foreach (var nodeB in _graph.Nodes)
                {
                    if (nodeA != nodeB)
                    {
                        var delta = nodeA.Position - nodeB.Position;
                        var distance = delta.magnitude * 0.1f;
                        var direction = delta.normalized;

                        var repulsion = direction * Repulsion / (distance * 0.5f);
                        nodeA.AddForce(repulsion);
                        nodeB.AddForce(-repulsion);
                    }
                }
            }
        }
    }
}