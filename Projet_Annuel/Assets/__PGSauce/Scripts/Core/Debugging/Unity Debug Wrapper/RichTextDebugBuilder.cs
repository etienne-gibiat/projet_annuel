using System.Text;
using UnityEngine;

namespace PGSauce.Core.PGDebugging
{
    public class RichTextDebugBuilder : IDebugBuilder
    {
        private StringBuilder stringBuilder;

        public RichTextDebugBuilder()
        {
            stringBuilder = new StringBuilder();
        }

        public IDebugBuilder BeginBold()
        {
            stringBuilder.Append("<b>");
            return this;
        }

        public IDebugBuilder BeginColor(Color c)
        {
            stringBuilder.Append($"<color=#{ColorUtility.ToHtmlStringRGBA(c)}>");
            return this;
        }

        public IDebugBuilder BeginItalic()
        {
            stringBuilder.Append("<i>");
            return this;
        }

        public IDebugBuilder BeginSize(int size)
        {
            stringBuilder.Append($"<size={size}>");
            return this;
        }

        public string Build()
        {
            string result = stringBuilder.ToString();
            stringBuilder.Clear();
            return result;
        }

        public IDebugBuilder EndBold()
        {
            stringBuilder.Append("</b>");
            return this;
        }

        public IDebugBuilder EndColor()
        {
            stringBuilder.Append("</color>");
            return this;
        }

        public IDebugBuilder EndItalic()
        {
            stringBuilder.Append("</i>");
            return this;
        }

        public IDebugBuilder EndSize()
        {
            stringBuilder.Append("</size>");
            return this;
        }

        public IDebugBuilder Message(string message)
        {
            stringBuilder.Append(message);
            return this;
        }

        public IDebugBuilder AppendSpace(string space)
        {
            stringBuilder.Append(space);
            return this;
        }
    }
}
