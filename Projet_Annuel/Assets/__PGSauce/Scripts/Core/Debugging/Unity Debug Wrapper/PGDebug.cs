using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PGSauce.Core.PGDebugging
{
    /// <summary>
    /// The main class replacing Unity.Debug
    /// </summary>
	public static class PGDebug
    {
        private const int NumberOfSymbolsEachSide = 7;
        private const char Symbol = '-';
		private static PGDebugDirector pgDebugDirector;

		static PGDebug()
        {
			pgDebugDirector = new PGDebugDirector();
        }

        /// <summary>
        /// Must be called before .Message(). Sets the current message Exception.
        /// </summary>
        public static PGDebugDirector SetException(System.Exception exception)
        {
            pgDebugDirector.SetException(exception);
            return pgDebugDirector;
        }

        /// <summary>
        /// Must be called before .Message(). If false the message won't be logged.
        /// </summary>
        public static PGDebugDirector SetCondition(bool condition)
        {
            pgDebugDirector.SetCondition(condition);
            return pgDebugDirector;
        }

        /// <summary>
        /// Must be called before .Message(). Sets the current message context object. When we click on the message, the object will
        /// be highlighted in the hierarchy.
        /// </summary>
        public static PGDebugDirector SetContext(Object context)
        {
            pgDebugDirector.SetContext(context);
            return pgDebugDirector;
        }

        /// <summary>
        /// The main method : call PGDebug.Message("my text") to add a message to the log. Then call .Log() to log all the messages.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static PGDebugDirector Message(string message)
        {
            pgDebugDirector.Message(message);
            return pgDebugDirector;
        }

        /// <summary>
        /// Must be called before .Message(). Sets the current message color.
        /// </summary>
        public static PGDebugDirector SetColor(Color c)
        {
            pgDebugDirector.SetColor(c);
            return pgDebugDirector;
        }

        /// <summary>
        /// Must be called before .Message(). Sets the current message bold.
        /// </summary>
        public static PGDebugDirector SetBold()
        {
            pgDebugDirector.SetBold();
            return pgDebugDirector;
        }

        /// <summary>
        /// Must be called before .Message(). Sets the current message italic.
        /// </summary>
        public static PGDebugDirector SetItalic()
        {
            pgDebugDirector.SetItalic();
            return pgDebugDirector;
        }

        /// <summary>
        /// Must be called before .Message(). Sets the current message size.
        /// Default is 14.
        /// </summary>
        public static PGDebugDirector SetSize(int size)
        {
            pgDebugDirector.SetSize(size);
            return pgDebugDirector;
        }

        /// <summary>
        /// Draws a line between start and end
        /// </summary>
        public static void DrawLine(Vector3 start, Vector3 end, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
        {
            pgDebugDirector.DrawLine(start, end, color, duration, depthTest);
        }

        /// <summary>
        /// Draws a line from start to start + direction
        /// </summary>
        public static void DrawRay(Vector3 start, Vector3 dir, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
        {
            pgDebugDirector.DrawRay(start, dir, color, duration, depthTest);
        }

        /// <summary>
        /// Pauses the editor
        /// </summary>
        public static void Break()
        {
            Pause();
        }

        /// <summary>
        /// Pauses the editor
        /// </summary>
        public static void Pause()
        {
            pgDebugDirector.Pause();
        }
        
        /// <summary>
        /// Logs the message to the console
        /// </summary>
        public static void Log()
        {
            pgDebugDirector.Log();
        }

        /// <summary>
        /// Logs the message in the warning channel
        /// </summary>
        public static void LogWarning()
        {
            pgDebugDirector.LogWarning();
        }

        /// <summary>
        /// Logs the message in the error channel
        /// </summary>
        public static void LogError()
        {
            pgDebugDirector.LogError();
        }

        /// <summary>
        /// Replaces Debug.Assert. The condition is set with SetCondition and must be set before calling this method.
        /// </summary>
        public static void Assert()
        {
            pgDebugDirector.Assert();
        }

        /// <summary>
        /// Clears errors from the developer console.
        /// </summary>
        public static void Clear()
        {
            pgDebugDirector.Clear();
        }

        /// <summary>
        /// Logs the exception
        /// </summary>
        public static void LogException()
        {
            pgDebugDirector.LogException();
        }

        /// <summary>
        /// Logs the list to the console
        /// </summary>
        public static void Log<T>(IEnumerable<T> objects)
        {
            pgDebugDirector.Log(objects);
        }

        /// <summary>
        /// Returns ------- MY HEADER -------
        /// </summary>
        /// <param name="header">MY HEADER</param>
        public static string Header(string header)
        {
            var sb = new StringBuilder();
            AppendSymbols(sb);

            sb.Append(" ");
            sb.Append(header);
            sb.Append(" ");
            
            AppendSymbols(sb);

            return sb.ToString();
        }
        
        private static void AppendSymbols(StringBuilder sb, int count = NumberOfSymbolsEachSide)
        {
            for (var i = 0; i < NumberOfSymbolsEachSide; i++)
            {
                sb.Append(Symbol);
            }
        }
    }
}
