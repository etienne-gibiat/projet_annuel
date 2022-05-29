using System;

namespace PGSauce.Core.Utilities
{
    public class ExecuteOnce2Args<T1, T2>
    {
        private Action<T1, T2> execute;
        private bool executed;

        public ExecuteOnce2Args(Action<T1, T2> execute)
        {
            this.execute = execute;
            executed = false;
        }

        public void Execute(T1 a1, T2 a2)
        {
            if(executed) { return; }
            executed = true;
            execute(a1, a2);
        }
        
        public void Reset()
        {
            executed = false;
        }
    }
}