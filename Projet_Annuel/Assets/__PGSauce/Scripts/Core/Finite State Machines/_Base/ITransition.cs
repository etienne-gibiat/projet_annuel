namespace PGSauce.Core.FSM.Base
{
    /// <summary>
    /// Interface representing a transition (target state and decision)
    /// </summary>
    public interface ITransition
    {
        AbstractState GetTargetState();
        IDecision GetDecision();
    }
}