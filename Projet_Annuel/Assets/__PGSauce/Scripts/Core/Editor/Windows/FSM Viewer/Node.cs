using PGSauce.Core.FSM.Base;
using PGSauce.Core.FSM.WithSo;
using UnityEngine;

namespace PGSauce.Core.PGEditor
{
    public class Node
    {
        private Vector2 _acceleration;
        private Vector2 _velocity;
        public Vector2 Position { get; set; }
        public AbstractState State { get; }
        public float Speed => _velocity.magnitude;
        public VisualNode VisualNodeInstance { get; set; }

        public Node(AbstractState state)
        {
            State = state;
            Position = Random.insideUnitCircle * 5000f;
            _acceleration = Vector2.zero;
        }


        public void AddForce(Vector2 force)
        {
            _acceleration += force / 3f;
        }

        public void UpdateVelocity(float timeStep, float damping)
        {
            _velocity += _acceleration * timeStep;
            _velocity *= damping;
            _acceleration = Vector2.zero;
        }
        
        public void UpdatePosition (float timeStep)
        {
            Position += (_velocity * timeStep);
        }
    }
}