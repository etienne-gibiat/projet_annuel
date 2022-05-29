using PGSauce.Core;

namespace PGSauce.Core.PGEvents
{
	public interface IPGEventTemplate : ITextTemplateBase
	{
		string SUBNAMESPACE();
		string FORMATTEDSPACEDTYPES();
		string MENUNAME();
		string FORMATTEDTYPES();
		string NUMBERARG();
		string TYPES();

	}
}
