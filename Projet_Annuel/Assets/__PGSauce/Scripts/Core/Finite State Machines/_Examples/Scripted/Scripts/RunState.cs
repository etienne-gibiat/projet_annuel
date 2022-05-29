using PGSauce.Core.PGDebugging;

namespace PGSauce.Core.FSM.Scripted.Example
{
    public class RunState : PlayerState
    {
        public override void LogicUpdate(Player controller)
        {
            base.LogicUpdate(controller);
            PGDebug.Message("I am RUNNING").Log();
        }
    }
}