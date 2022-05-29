namespace GameTroopers.UI
{
    /// <summary>
    /// Interface to implement if you wish to receive <see cref="OnFocused"/> callbacks
    /// </summary>
    public interface IOnMenuUnfocused
    {
        /// <summary>
        /// This callback will be executed when a menu lose the input, 
        /// always before the OnMenuShowStarted.
        /// </summary>
        void OnMenuUnfocused();
    }
}