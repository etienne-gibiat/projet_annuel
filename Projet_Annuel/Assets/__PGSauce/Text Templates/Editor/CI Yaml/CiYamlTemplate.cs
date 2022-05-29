using UnityEngine;
using System.Collections.Generic;
using PGSauce.Core;

namespace PGSauce.Core.FSM.WithSo
{
	public class CiYamlTemplate : TextTemplateGeneratorBase
	{
	    public CiYamlTemplate(ICiYamlTemplate templateInterface)
        {
            TagGenerators = new Dictionary<string, TagGenerator>();
            
			TagGenerators.Add("#BUILDNAME#", templateInterface.BUILDNAME);
			TagGenerators.Add("#UNITYVERSION#", templateInterface.UNITYVERSION);
			TagGenerators.Add("#SUBFOLDER_FORMATTED_BASH#", templateInterface.SUBFOLDER_FORMATTED_BASH);
			TagGenerators.Add("#SUBFOLDER#", templateInterface.SUBFOLDER);
			TagGenerators.Add("#OTHERVARIABLES#", templateInterface.OTHERVARIABLES);
			TagGenerators.Add("#BUILDTARGETS#", templateInterface.BUILDTARGETS);
			TagGenerators.Add("#DEPLOYS#", templateInterface.DEPLOYS);

        }
	}
}
