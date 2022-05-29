using UnityEngine;
using System.Collections.Generic;
using PGSauce.Core;

namespace PGSauce.Core.PGEvents
{
	public class PGEventListenerTemplate : TextTemplateGeneratorBase
	{
	    public PGEventListenerTemplate(IPGEventListenerTemplate templateInterface)
        {
            TagGenerators = new Dictionary<string, TagGenerator>();
            
			TagGenerators.Add("#SUBNAMESPACE#", templateInterface.SUBNAMESPACE);
			TagGenerators.Add("#FORMATTEDTYPES#", templateInterface.FORMATTEDTYPES);
			TagGenerators.Add("#NUMBERARG#", templateInterface.NUMBERARG);
			TagGenerators.Add("#TYPES#", templateInterface.TYPES);

        }
	}
}
