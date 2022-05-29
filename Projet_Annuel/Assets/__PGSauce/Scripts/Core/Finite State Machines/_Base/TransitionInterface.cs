namespace PGSauce.Core.FSM.Base
{
    /// <summary>
    /// An implementation of the interface ITransition
    /// </summary>
    public class TransitionInterface : ITransition
    {
        private readonly AbstractState _target;
        private readonly IDecision _decision;
        private readonly bool _reverse;

        public TransitionInterface(AbstractState target, IDecision decision, bool reverse)
        {
            _target = target;
            _decision = decision;
            _reverse = reverse;
        }
        
        public IDecision GetDecision()
        {
            return _decision;
        }

        public AbstractState GetTargetState()
        {
            return _target;
        }

        public bool ReverseValue()
        {
            return _reverse;
        }
    }
}