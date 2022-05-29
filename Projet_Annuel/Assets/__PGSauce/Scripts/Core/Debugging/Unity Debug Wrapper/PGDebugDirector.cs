using System;
using System.Collections.Generic;
using System.Diagnostics;
using PGSauce.Core.Extensions;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
#pragma warning disable 162

namespace PGSauce.Core.PGDebugging
{
	public class PGDebugDirector
	{
		private RichTextDebugBuilder debugBuilder;
        private List<MessageStructure> messages;
        private MessageStructure currentMessage;
        private Object _context;
        private bool _condition;
        private System.Exception _exception;
        private PGDebugSettings _settings;
        
        private PGDebugSettings.PrefixType PrefixType => _settings.defaultPrefixType;
        
        public const int NumberOfMethodsToGoUp = 8;

        private MessageStructure CurrentMessage { get {
                if(currentMessage == null)
                {
#if  UNITY_EDITOR || DEVELOPMENT_BUILD
                    throw new UnityException("You haven't called Message(string) on your debug message !");
#endif 
                    return new MessageStructure("", Color.black, false,false,10);
                }

                return currentMessage;
            }
            set => currentMessage = value; }

        public PGDebugDirector()
        {
#if  UNITY_EDITOR || DEVELOPMENT_BUILD
            debugBuilder = new RichTextDebugBuilder();
            _settings = PGDebugSettings.Instance;
            Reset();
#endif 
        }
        
        public PGDebugDirector SetException(System.Exception exception)
        {
            _exception = exception;
            return this;
        }

        public PGDebugDirector SetCondition(bool condition)
        {
            _condition = condition;
            return this;
        }

        public PGDebugDirector SetContext(Object context)
        {
            _context = context;
            return this;
        }

        public PGDebugDirector Message(string message)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD 
            CurrentMessage = new MessageStructure(message, Color.magenta, false, false, 14);
            messages.Add(CurrentMessage);
#endif
            return this;
        }

        public PGDebugDirector SetColor(Color c)
        {
#if  UNITY_EDITOR || DEVELOPMENT_BUILD
            CurrentMessage.color = c;
            CurrentMessage.hasCustomColor = true;
#endif
            return this;
        }

        public PGDebugDirector SetBold()
        {
            
#if  UNITY_EDITOR || DEVELOPMENT_BUILD
            CurrentMessage.isBold = true;
#endif
            return this;
        }

        public PGDebugDirector SetItalic()
        {
            
#if  UNITY_EDITOR || DEVELOPMENT_BUILD
            CurrentMessage.isItalic = true;
#endif
            return this;
        }

        public PGDebugDirector SetSize(int size)
        {
#if  UNITY_EDITOR || DEVELOPMENT_BUILD
            CurrentMessage.size = size;
            CurrentMessage.hasCustomSize = true;
#endif
            return this;
        }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public void DrawLine(Vector3 start, Vector3 end, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
        {
            if(_condition)
            {
                Debug.DrawLine(start, end, color, duration, depthTest);
            }
            Reset();
        }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public void DrawRay(Vector3 start, Vector3 dir, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
        {
            if (_condition)
            {
                Debug.DrawRay(start, dir, color, duration, depthTest);
            }
            Reset();
        }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public void Pause()
        {
            if (_condition)
            {
                Debug.Break();
            }
            Reset();
        }
        
        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public void Break()
        {
            Pause();
        }
        
        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public void Log<T>(IEnumerable<T> objects)
        {
            if (_condition)
            {
                PGDebug.Log();
                PGDebug.Message(objects.ListToString());
                PGDebug.Log();
            }

            Reset();
        }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public void Log()
        {
            if (_condition)
            {
                Debug.Log(GetFullMessage(_settings.defaultLogColor), _context);
            }

            Reset();
        }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public void LogWarning()
        {
            if (_condition)
            {
                Debug.LogWarning(GetFullMessage(_settings.defaultLogWarningColor), _context);
            }

            Reset();
        }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public void LogError()
        {
            if (_condition)
            {
                Debug.LogError(GetFullMessage(_settings.defaultLogErrorColor), _context);
            }

            Reset();
        }
        
        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public void LogTodo()
        {
            if (_condition)
            {
                Debug.Log(GetFullMessage(_settings.defaultLogColor, true), _context);
            }

            Reset();
        }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR"), Conditional("UNITY_ASSERTIONS")]
        public void Assert()
        {
            Debug.Assert(_condition, GetFullMessage(_settings.defaultLogColor), _context);
            Reset();
        }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public void Clear()
        {
            if (_condition)
            {
                Debug.ClearDeveloperConsole();
            }

            Reset();
        }

        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public void LogException()
        {
            if (_condition)
            {
                Debug.LogException(_exception, _context);
            }

            Reset();
        }

        private string GetFullMessage(Color textColor, bool isTodo = false)
        {
            BuildMessages(textColor, isTodo);
            return debugBuilder.Build();
        }

        private void BuildMessages(Color textColor, bool isTodo)
        {
            BuildPrefix(isTodo);

            foreach (var message in messages)
            {
                CheckMessageCustom(message,textColor);
                BuildMessage(message);
            }
        }

        private void CheckMessageCustom(MessageStructure message, Color textColor)
        {
            if (!message.hasCustomColor)
            {
                message.color = textColor;
            }

            if (!message.hasCustomSize)
            {
                message.size = _settings.textSize;
            }
        }

        private void BuildPrefix(bool isTodo)
        {
            BuildMessage(new MessageStructure(isTodo ? "PG TODO" : "PG DEBUG", isTodo ? PgColors.Pink: PgColors.Blueish, true, false, 16));
            if (PrefixType != PGDebugSettings.PrefixType.NoPrefix)
            {
                var prefix = GetPrefixText();
                BuildMessage(new MessageStructure(prefix, PgColors.Greenish, false, true, 15));
            }
        }

        private string GetPrefixText()
        {
            switch (PrefixType)
            {
                case PGDebugSettings.PrefixType.ClassName:
                    return GetClassName();
                case PGDebugSettings.PrefixType.MethodName:
                    return GetMethodName();
                default:
                    return "";
            }
        }

        private string GetClassName()
        {
            return GetCallerName(true);
        }

        private static string GetCallerName(bool onlyClass)
        {
            var stack = Environment.StackTrace;
            var table = stack.Split('\n');
            var callingMethod = table[NumberOfMethodsToGoUp];
            callingMethod = callingMethod.Trim();
            table = callingMethod.Split(' ');
            var method = table[1];
            var parts = method.Split('.');
            method = parts[parts.Length - 1];
            var className = parts[parts.Length - 2];

            if (onlyClass)
            {
                return className;
            }
            
            return $"{className}.{method}";
        }

        private static string GetMethodName()
        {
            return GetCallerName(false);
        }


        private void Reset()
        {
            CurrentMessage = null;
            messages = new List<MessageStructure>();
            _condition = true;
            _context = null;
            _exception = null;
            debugBuilder.Build();
        }

        private void BuildMessage(MessageStructure message)
        {
            debugBuilder
                                .BeginColor(message.color)
                                .BeginSize(message.size);

            if (message.isItalic)
            {
                debugBuilder.BeginItalic();
            }

            if (message.isBold)
            {
                debugBuilder.BeginBold();
            }

            debugBuilder
                .Message(message.message);

            if (message.isBold)
            {
                debugBuilder.EndBold();
            }

            if (message.isItalic)
            {
                debugBuilder.EndItalic();
            }

            debugBuilder
                .EndSize()
                .EndColor()
                .AppendSpace(" ");
        }

        private class MessageStructure
        {
            public string message;
            public Color color;
            public bool isBold;
            public bool isItalic;
            public int size;
            public bool hasCustomColor;
            public bool hasCustomSize;

            public MessageStructure(string message, Color color, bool isBold, bool isItalic, int size)
            {
                this.message = message;
                this.color = color;
                this.isBold = isBold;
                this.isItalic = isItalic;
                this.size = size;
                hasCustomColor = false;
                hasCustomSize = false;
            }
        }
    }
}
