using PGSauce.Core;

namespace PGSauce.Core.GlobalVariables
{
	public interface IGlobalVariableTemplate : ITextTemplateBase
	{
		string SUBNAMESPACE();
		string FORMATTEDTYPE();
		string MENUNAME();
		string TYPE();

	}
}
