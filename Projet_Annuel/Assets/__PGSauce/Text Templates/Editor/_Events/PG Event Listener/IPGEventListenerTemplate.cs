using PGSauce.Core;

namespace PGSauce.Core.PGEvents
{
	public interface IPGEventListenerTemplate : ITextTemplateBase
	{
		string SUBNAMESPACE();
		string FORMATTEDTYPES();
		string NUMBERARG();
		string TYPES();

	}
}
