namespace GameTroopers.UI
{
    /// <summary>
    /// Interface to implement if you wish to receive <see cref="OnMenuHideEnded"/> callbacks
    /// </summary>
    public interface IOnMenuHideEnded
    {
        /// <summary>
        /// This callback will be executed when the hide animation of a menu has finished
        /// and it is not longer visible.
        /// </summary>
        void OnMenuHideEnded();
    }
}