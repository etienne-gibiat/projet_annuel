using PGSauce.Core.FSM.Base;
using PGSauce.Core.FSM.WithSo;

namespace PGSauce.Core.PGEditor
{
    public class Edge
    {
        public const float Length = 130f;
        
        public string Name { get; }
        public Node Source { get; }
        public Node Target { get; }

        public float Stiffness { get; }
        
        public IDecision Decision { get; set; }

        protected Edge(string name, Node source, Node target, IDecision decision, float stiffness)
        {
            Name = name;
            Source = source;
            Target = target;
            Stiffness = stiffness;
            Decision = decision;
        }
    }
}