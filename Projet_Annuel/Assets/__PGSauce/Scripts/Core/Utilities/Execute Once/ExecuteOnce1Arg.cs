using System;

namespace PGSauce.Core.Utilities
{
    public class ExecuteOnce1Arg<T1>
    {
        private Action<T1> execute;
        private bool executed;

        public ExecuteOnce1Arg(Action<T1> execute)
        {
            this.execute = execute;
            executed = false;
        }

        public void Execute(T1 a1)
        {
            if(executed) { return; }
            executed = true;
            execute(a1);
        }
        
        public void Reset()
        {
            executed = false;
        }
    }
}