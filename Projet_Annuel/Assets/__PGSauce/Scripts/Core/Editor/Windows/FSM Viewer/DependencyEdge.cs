using PGSauce.Core.FSM.Base;
using PGSauce.Core.FSM.WithSo;

namespace PGSauce.Core.PGEditor
{
    public class DependencyEdge : Edge
    {
        public DependencyEdge(string name, Node source, Node target, IDecision decision, float stiffness) : base(name, source, target, decision, stiffness)
        {
        }
    }
}