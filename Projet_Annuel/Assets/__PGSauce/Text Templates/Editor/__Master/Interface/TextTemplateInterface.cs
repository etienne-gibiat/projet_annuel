using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core;

namespace PGSauce.Core
{
    public class TextTemplateInterface : ITextTemplateInterface
    {
        private string _subNameSpace, _templateName, _methods;

        public TextTemplateInterface(string subNameSpace, string templateName, string methods)
        {
            _subNameSpace = subNameSpace;
            _templateName = templateName;
            _methods = methods;
        }

        public string SUBNAMESPACE()
        {
            return _subNameSpace;
        }
        
        public string TEMPLATENAME()
        {
            return _templateName;
        }
        
        public string METHODS()
        {
            return _methods;
        }
    }
}
