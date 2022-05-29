namespace PGSauce.Core.PGEditor
{
    public enum DependencyType
    {
        None,
        PublicInstance,
        PrivateInstance,
        PublicStatic,
        PrivateStatic,
        Dirty,
        Circular
    }
}