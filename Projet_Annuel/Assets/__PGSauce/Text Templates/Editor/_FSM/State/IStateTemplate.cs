using PGSauce.Core;

namespace PGSauce.Core.FSM.WithSo
{
	public interface IStateTemplate : ITextTemplateBase
	{
		string SUBNAMESPACE();
		string MENUSTATE();
		string STATENAME();
		string STATECONTROLLERNAME();

	}
}
