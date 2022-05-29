using Jobberwocky.GeometryAlgorithms.Source.Core;

namespace Jobberwocky.GeometryAlgorithms.Source.Parameters
{
    public abstract class Parameters : IParameters
    {
        public CoordinateSystem CoordinateSystem { get; set; }

        public Parameters()
        {
            CoordinateSystem = CoordinateSystem.XYZ;
        }
    }
}
