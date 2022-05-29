namespace GameTroopers.UI
{
    /// <summary>
    /// Interface to implement if you wish to receive <see cref="OnMenuHideStarted"/> callbacks
    /// </summary>
    public interface IOnMenuHideStarted
    {
        /// <summary>
        /// This callback will be executed when a menu is about to start playing
        /// its hide animation, so it is still visible at this point.
        /// </summary>
        void OnMenuHideStarted();
    }
}