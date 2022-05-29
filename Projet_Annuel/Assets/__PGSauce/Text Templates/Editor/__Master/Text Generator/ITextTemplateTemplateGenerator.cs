namespace PGSauce.Core
{
    public interface ITextTemplateTemplateGenerator : ITextTemplateBase
    {
        string SUBNAMESPACE();
        string TEMPLATENAME();
        string TAGGENERATORINIT();
    }
}