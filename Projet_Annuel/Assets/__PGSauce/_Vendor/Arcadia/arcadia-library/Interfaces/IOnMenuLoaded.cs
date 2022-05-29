namespace GameTroopers.UI
{
    /// <summary>
    /// Interface to implement if you wish to receive <see cref="OnMenuLoaded"/> callbacks
    /// </summary>
    public interface IOnMenuLoaded
    {
        /// <summary>
        /// This callback will be executed when the menu is loaded into memory. 
        /// <para>
        /// This can happen after a <see cref="MenuManager.LoadGroup"/> call or during the game
        /// initialization if <see cref="UISettings.loadAllOnStart"/> option is selected in the 
        /// settings.
        /// </para>
        /// </summary>
        void OnMenuLoaded();
    }
}