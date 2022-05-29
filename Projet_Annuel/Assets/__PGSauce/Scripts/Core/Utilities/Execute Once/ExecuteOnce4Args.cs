using System;

namespace PGSauce.Core.Utilities
{
    public class ExecuteOnce4Args<T1, T2, T3, T4>
    {
        private Action<T1, T2, T3, T4> execute;
        private bool executed;

        public ExecuteOnce4Args(Action<T1, T2, T3, T4> execute)
        {
            this.execute = execute;
            executed = false;
        }

        public void Execute(T1 a1, T2 a2, T3 a3, T4 a4)
        {
            if(executed) { return; }
            executed = true;
            execute(a1, a2, a3, a4);
        }
        
        public void Reset()
        {
            executed = false;
        }
    }
}