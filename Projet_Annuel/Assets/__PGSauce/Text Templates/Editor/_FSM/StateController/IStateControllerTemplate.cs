using PGSauce.Core;

namespace PGSauce.Core.FSM.WithSo
{
	public interface IStateControllerTemplate : ITextTemplateBase
	{
		string SUBNAMESPACE();
		string STATECONTROLLERNAME();

	}
}
