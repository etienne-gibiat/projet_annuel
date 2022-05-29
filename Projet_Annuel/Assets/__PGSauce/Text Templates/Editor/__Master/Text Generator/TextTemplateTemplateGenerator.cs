namespace PGSauce.Core
{
    public class TextTemplateTemplateGenerator : ITextTemplateTemplateGenerator
    {
        private string _subNameSpace, _templateName, _tagGeneratorInit;

        public TextTemplateTemplateGenerator(string subNameSpace, string templateName, string tagGeneratorInit)
        {
            _subNameSpace = subNameSpace;
            _templateName = templateName;
            _tagGeneratorInit = tagGeneratorInit;
        }
        
        public string SUBNAMESPACE()
        {
            return _subNameSpace;
        }

        public string TEMPLATENAME()
        {
            return _templateName;
        }

        public string TAGGENERATORINIT()
        {
            return _tagGeneratorInit;
        }
    }
}