namespace _Elementis.Scripts.WFC
{
    internal interface IWaveConstraint
    {
        void Init(WavePropagator wavePropagator);

        void Check(WavePropagator wavePropagator);
    }
}