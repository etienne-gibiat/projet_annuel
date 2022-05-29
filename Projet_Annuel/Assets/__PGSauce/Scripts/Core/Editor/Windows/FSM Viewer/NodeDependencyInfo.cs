namespace PGSauce.Core.PGEditor
{
    public struct NodeDependencyInfo
    {
        public NodeDependencyInfo (DependencyType depType)
        {
            DepType = depType;
        }

        public DependencyType DepType;
    }
}