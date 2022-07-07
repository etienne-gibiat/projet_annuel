namespace _Elementis.Scripts.WFC
{
    internal interface IPatternModelConstraint
    {
        void DoBan(int index, int pattern);

        void UndoBan(int index, int pattern);

        void DoSelect(int index, int pattern);

        void Propagate();

        void Clear();
    }
}