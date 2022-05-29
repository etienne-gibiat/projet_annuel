using UnityEngine;

namespace PGSauce.Core.PGDebugging
{
	public interface IDebugBuilder
	{
		IDebugBuilder Message(string message);
		IDebugBuilder BeginColor(Color c);
		IDebugBuilder EndColor();
		IDebugBuilder BeginBold();
		IDebugBuilder EndBold();
		IDebugBuilder BeginItalic();
		IDebugBuilder EndItalic();
		IDebugBuilder BeginSize(int size);
		IDebugBuilder EndSize();
		IDebugBuilder AppendSpace(string space);

		string Build();
	}
}
