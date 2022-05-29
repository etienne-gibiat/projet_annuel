using UnityEngine;
using System.Collections.Generic;
using PGSauce.Core;

namespace PGSauce.Core.FSM.WithSo
{
	public class DecisionTemplate : TextTemplateGeneratorBase
	{
	    public DecisionTemplate(IDecisionTemplate templateInterface)
        {
            TagGenerators = new Dictionary<string, TagGenerator>();
            
			TagGenerators.Add("#SUBNAMESPACE#", templateInterface.SUBNAMESPACE);
			TagGenerators.Add("#MENUDECISION#", templateInterface.MENUDECISION);
			TagGenerators.Add("#DECISIONNAME#", templateInterface.DECISIONNAME);
			TagGenerators.Add("#STATECONTROLLERNAME#", templateInterface.STATECONTROLLERNAME);
			TagGenerators.Add("#DEFAULTVALUE#", templateInterface.DEFAULTVALUE);

        }
	}
}
