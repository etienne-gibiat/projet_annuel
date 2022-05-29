using PGSauce.Core;

namespace PGSauce.Core.FSM.WithSo
{
	public interface ICiYamlTemplate : ITextTemplateBase
	{
		string BUILDNAME();
		string UNITYVERSION();
		string SUBFOLDER_FORMATTED_BASH();
		string SUBFOLDER();
		string OTHERVARIABLES();
		string BUILDTARGETS();
		string DEPLOYS();

	}
}
