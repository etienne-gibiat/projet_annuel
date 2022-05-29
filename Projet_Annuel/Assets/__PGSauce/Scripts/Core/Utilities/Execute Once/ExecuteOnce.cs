using System;

namespace PGSauce.Core.Utilities
{
    public class ExecuteOnce
    {
        private Action execute;
        private bool executed;

        public ExecuteOnce(Action execute)
        {
            this.execute = execute;
            executed = false;
        }

        public void Execute()
        {
            if(executed) { return; }
            executed = true;
            execute();
        }
        
        public void Reset()
        {
            executed = false;
        }

    }
}
