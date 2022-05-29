using System.Collections.Generic;

namespace PGSauce.Core
{
    public class TextTemplateGeneratorTemplateGenerator : TextTemplateGeneratorBase
    {
        public TextTemplateGeneratorTemplateGenerator(ITextTemplateTemplateGenerator templateInterface)
        {
            TagGenerators = new Dictionary<string, TagGenerator>();
            
            TagGenerators.Add("#SUBNAMESPACE#", templateInterface.SUBNAMESPACE);
            TagGenerators.Add("#TEMPLATENAME#", templateInterface.TEMPLATENAME);
            TagGenerators.Add("#TAGGENERATORINIT#", templateInterface.TAGGENERATORINIT);
        }
    }
}