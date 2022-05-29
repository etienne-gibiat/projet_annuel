using PGSauce.Core.PGDebugging;

namespace PGSauce.Core.FSM.Scripted.Example
{
    public class ImmortalState : PlayerState
    {
        public override void LogicUpdate(Player controller)
        {
            base.LogicUpdate(controller);
            PGDebug.Message("I am IMMORTAL").Log();
        }
    }
}