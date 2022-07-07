namespace _Elementis.Scripts.WFC
{
    internal interface ITracker
    {
        void Reset();

        void DoBan(int index, int pattern);

        void UndoBan(int index, int pattern);
    }
}