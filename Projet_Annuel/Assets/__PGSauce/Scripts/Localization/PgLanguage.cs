using System;

namespace PGSauce.Localization
{
    [Serializable]
    public struct PgLanguage
    {
        public string name;
        public string code;

        public PgLanguage(string name, string code)
        {
            this.name = name;
            this.code = code;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(PgLanguage other)
        {
            return name == other.name && code == other.code;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((name != null ? name.GetHashCode() : 0) * 397) ^ (code != null ? code.GetHashCode() : 0);
            }
        }
    }
}