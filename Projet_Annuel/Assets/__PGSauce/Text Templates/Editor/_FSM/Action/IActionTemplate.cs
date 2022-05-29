using PGSauce.Core;

namespace PGSauce.Core.FSM.WithSo
{
	public interface IActionTemplate : ITextTemplateBase
	{
		string SUBNAMESPACE();
		string MENUACTION();
		string ACTIONNAME();
		string NAME();
		string STATECONTROLLERNAME();

	}
}
