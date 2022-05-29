using System;

namespace PGSauce.Core.Utilities
{
    public class ExecuteOnce3Args<T1, T2, T3>
    {
        private Action<T1, T2, T3> execute;
        private bool executed;

        public ExecuteOnce3Args(Action<T1, T2, T3> execute)
        {
            this.execute = execute;
            executed = false;
        }

        public void Execute(T1 a1, T2 a2, T3 a3)
        {
            if(executed) { return; }
            executed = true;
            execute(a1, a2, a3);
        }
        
        public void Reset()
        {
            executed = false;
        }
    }
}