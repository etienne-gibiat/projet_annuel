using UnityEngine;
using System.Collections.Generic;
using PGSauce.Core;

namespace PGSauce.Core.Fsm.WithSo
{
	public class AnyTemplate : TextTemplateGeneratorBase
	{
	    public AnyTemplate(IAnyTemplate templateInterface)
        {
            TagGenerators = new Dictionary<string, TagGenerator>();
            
			TagGenerators.Add("#SUBNAMESPACE#", templateInterface.SUBNAMESPACE);
			TagGenerators.Add("#MENUANY#", templateInterface.MENUANY);
			TagGenerators.Add("#ANYNAME#", templateInterface.ANYNAME);
			TagGenerators.Add("#STATECONTROLLERNAME#", templateInterface.STATECONTROLLERNAME);

        }
	}
}
