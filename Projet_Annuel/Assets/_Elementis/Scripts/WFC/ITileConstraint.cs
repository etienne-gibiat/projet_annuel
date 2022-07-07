namespace _Elementis.Scripts.WFC
{
    public interface ITileConstraint
    {
        void Init(TilePropagator propagator);

        void Check(TilePropagator propagator);
    }
    
    internal class TileConstraintAdaptor : IWaveConstraint
    {
        private readonly ITileConstraint underlying;
        private readonly TilePropagator propagator;

        public TileConstraintAdaptor(ITileConstraint underlying, TilePropagator propagator)
        {
            this.underlying = underlying;
            this.propagator = propagator;
        }

        public void Check(WavePropagator wavePropagator) => this.underlying.Check(this.propagator);

        public void Init(WavePropagator wavePropagator) => this.underlying.Init(this.propagator);
    }
}