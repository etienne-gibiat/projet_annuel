using PGSauce.Core;

namespace PGSauce.Core.PGEvents
{
	public interface IUnityEventTemplate : ITextTemplateBase
	{
		string SUBNAMESPACE();
		string FORMATTEDTYPES();
		string TYPES();

	}
}
