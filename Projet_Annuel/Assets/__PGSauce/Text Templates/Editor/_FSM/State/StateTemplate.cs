using UnityEngine;
using System.Collections.Generic;
using PGSauce.Core;

namespace PGSauce.Core.FSM.WithSo
{
	public class StateTemplate : TextTemplateGeneratorBase
	{
	    public StateTemplate(IStateTemplate templateInterface)
        {
            TagGenerators = new Dictionary<string, TagGenerator>();
            
			TagGenerators.Add("#SUBNAMESPACE#", templateInterface.SUBNAMESPACE);
			TagGenerators.Add("#MENUSTATE#", templateInterface.MENUSTATE);
			TagGenerators.Add("#STATENAME#", templateInterface.STATENAME);
			TagGenerators.Add("#STATECONTROLLERNAME#", templateInterface.STATECONTROLLERNAME);

        }
	}
}
