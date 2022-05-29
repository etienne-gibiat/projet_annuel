namespace GameTroopers.UI
{
    /// <summary>
    /// Interface to implement if you wish to receive <see cref="OnUnfocused"/> callbacks
    /// </summary>
    public interface IOnMenuFocused
    {
        /// <summary>
        /// This callback will be executed when a menu gains the input
        /// and is ready to use, always after the OnMenuHideEnded.
        /// </summary>
        void OnMenuFocused();
    }
}