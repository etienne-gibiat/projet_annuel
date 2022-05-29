using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core;

namespace PGSauce.Core
{
    public class TextTemplateGenerator : TextTemplateGeneratorBase
    {
        public TextTemplateGenerator(ITextTemplateInterface templateInterface)
        {
            TagGenerators = new Dictionary<string, TagGenerator>();
            
            TagGenerators.Add("#SUBNAMESPACE#", templateInterface.SUBNAMESPACE);
            TagGenerators.Add("#TEMPLATENAME#", templateInterface.TEMPLATENAME);
            TagGenerators.Add("#METHODS#", templateInterface.METHODS);
        }
    }
}
