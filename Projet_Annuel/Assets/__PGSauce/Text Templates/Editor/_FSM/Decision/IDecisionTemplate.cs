using PGSauce.Core;

namespace PGSauce.Core.FSM.WithSo
{
	public interface IDecisionTemplate : ITextTemplateBase
	{
		string SUBNAMESPACE();
		string MENUDECISION();
		string DECISIONNAME();
		string STATECONTROLLERNAME();
		string DEFAULTVALUE();

	}
}
