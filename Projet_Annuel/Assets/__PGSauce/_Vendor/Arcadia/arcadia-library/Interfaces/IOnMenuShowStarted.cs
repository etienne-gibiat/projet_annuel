namespace GameTroopers.UI
{
    /// <summary>
    /// Interface to implement if you wish to receive <see cref="OnMenuShowStarted"/> callbacks
    /// </summary>
    public interface IOnMenuShowStarted
    {
        /// <summary>
        /// This callback will be executed when a menu is about to start playing
        /// its show animation, so it is still not visible at this point.
        /// </summary>
        void OnMenuShowStarted();
    }
}