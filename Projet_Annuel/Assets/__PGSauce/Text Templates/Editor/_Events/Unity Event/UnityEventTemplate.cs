using UnityEngine;
using System.Collections.Generic;
using PGSauce.Core;

namespace PGSauce.Core.PGEvents
{
	public class UnityEventTemplate : TextTemplateGeneratorBase
	{
	    public UnityEventTemplate(IUnityEventTemplate templateInterface)
        {
            TagGenerators = new Dictionary<string, TagGenerator>();
            
			TagGenerators.Add("#SUBNAMESPACE#", templateInterface.SUBNAMESPACE);
			TagGenerators.Add("#FORMATTEDTYPES#", templateInterface.FORMATTEDTYPES);
			TagGenerators.Add("#TYPES#", templateInterface.TYPES);

        }
	}
}
