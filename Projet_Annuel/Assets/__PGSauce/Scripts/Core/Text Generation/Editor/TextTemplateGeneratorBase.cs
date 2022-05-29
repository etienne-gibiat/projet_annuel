using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PGSauce.Core
{
    public class TextTemplateGeneratorBase
    {
        protected delegate string TagGenerator();

        protected Dictionary<string, TagGenerator> TagGenerators;

        private string GetTagValue(string tag)
        {
            return TagGenerators[tag].Invoke();
        }

        public string ReplaceTemplateWithData(string template)
        {
            template = TagGenerators.Keys.Aggregate(template, (current, tag) => current.Replace(tag, GetTagValue(tag)));

            return template;
        }
    }
}
