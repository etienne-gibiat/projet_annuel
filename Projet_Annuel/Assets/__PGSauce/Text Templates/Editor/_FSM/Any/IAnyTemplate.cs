using PGSauce.Core;

namespace PGSauce.Core.Fsm.WithSo
{
	public interface IAnyTemplate : ITextTemplateBase
	{
		string SUBNAMESPACE();
		string MENUANY();
		string ANYNAME();
		string STATECONTROLLERNAME();

	}
}
