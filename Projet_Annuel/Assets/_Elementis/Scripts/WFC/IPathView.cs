namespace _Elementis.Scripts.WFC
{
    public interface IPathView
    {
        PathConstraintUtils.SimpleGraph Graph { get; }

        bool[] CouldBePath { get; }

        bool[] MustBePath { get; }

        bool[] CouldBeRelevant { get; }

        bool[] MustBeRelevant { get; }

        void Update();

        void SelectPath(int index);

        void BanPath(int index);

        void BanRelevant(int index);
    }
    
    public static class PathViewExtensions
    {
        public static void Init(this IPathView pathView)
        {
            pathView.Update();
            for (int index = 0; index < pathView.Graph.NodeCount; ++index)
            {
                if (pathView.MustBeRelevant[index])
                    pathView.SelectPath(index);
            }
        }
    }
}