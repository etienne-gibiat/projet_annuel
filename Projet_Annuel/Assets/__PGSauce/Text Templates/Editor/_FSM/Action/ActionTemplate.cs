using UnityEngine;
using System.Collections.Generic;
using PGSauce.Core;

namespace PGSauce.Core.FSM.WithSo
{
	public class ActionTemplate : TextTemplateGeneratorBase
	{
	    public ActionTemplate(IActionTemplate templateInterface)
        {
            TagGenerators = new Dictionary<string, TagGenerator>();
            
			TagGenerators.Add("#SUBNAMESPACE#", templateInterface.SUBNAMESPACE);
			TagGenerators.Add("#MENUACTION#", templateInterface.MENUACTION);
			TagGenerators.Add("#ACTIONNAME#", templateInterface.ACTIONNAME);
			TagGenerators.Add("#NAME#", templateInterface.NAME);
			TagGenerators.Add("#STATECONTROLLERNAME#", templateInterface.STATECONTROLLERNAME);

        }
	}
}
