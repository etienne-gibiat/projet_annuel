namespace GameTroopers.UI
{
    /// <summary>
    /// Interface to implement if you wish to receive <see cref="OnMenuShowEnded"/> callbacks
    /// </summary>
    public interface IOnMenuShowEnded
    {
        /// <summary>
        /// This callback will be executed when the show animation of a menu has finished
        /// and it is not longer visible.
        /// </summary>
        void OnMenuShowEnded();
    }
}