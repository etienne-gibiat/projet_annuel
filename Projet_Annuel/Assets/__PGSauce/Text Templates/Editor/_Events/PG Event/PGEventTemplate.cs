using UnityEngine;
using System.Collections.Generic;
using PGSauce.Core;

namespace PGSauce.Core.PGEvents
{
	public class PGEventTemplate : TextTemplateGeneratorBase
	{
	    public PGEventTemplate(IPGEventTemplate templateInterface)
        {
            TagGenerators = new Dictionary<string, TagGenerator>();
            
			TagGenerators.Add("#SUBNAMESPACE#", templateInterface.SUBNAMESPACE);
			TagGenerators.Add("#FORMATTEDSPACEDTYPES#", templateInterface.FORMATTEDSPACEDTYPES);
			TagGenerators.Add("#MENUNAME#", templateInterface.MENUNAME);
			TagGenerators.Add("#FORMATTEDTYPES#", templateInterface.FORMATTEDTYPES);
			TagGenerators.Add("#NUMBERARG#", templateInterface.NUMBERARG);
			TagGenerators.Add("#TYPES#", templateInterface.TYPES);

        }
	}
}
