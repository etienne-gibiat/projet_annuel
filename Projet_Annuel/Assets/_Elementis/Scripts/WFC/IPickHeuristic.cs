namespace _Elementis.Scripts.WFC
{
    internal interface IPickHeuristic
    {
        void PickObservation(out int index, out int pattern);
    }
}