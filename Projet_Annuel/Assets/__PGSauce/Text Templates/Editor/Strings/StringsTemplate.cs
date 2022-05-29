using UnityEngine;
using System.Collections.Generic;
using PGSauce.Core;

namespace PGSauce.Core.Strings
{
	public class StringsTemplate : TextTemplateGeneratorBase
	{
	    public StringsTemplate(IStringsTemplate templateInterface)
        {
            TagGenerators = new Dictionary<string, TagGenerator>();
            
			TagGenerators.Add("#GAMENAME#", templateInterface.GAMENAME);

        }
	}
}
