namespace PGSauce.UI
{
    public interface UIElementBase
    {
        /// <summary>
        /// The duration it takes to finish the hiding animation.
        /// </summary>
        float GetAllHidingTime();
        /// <summary>
        /// The duration it takes to finish the showing animation.
        /// </summary>
        float GetAllShowingTime();
        /// <summary>
        /// Change the visibility of this menu by playing the desired animation.
        /// </summary>
        /// <param name="visible">Should this menu be visible or not?</param>
        /// <param name="noSounds">If true, sounds won't play</param>
        void ChangeVisibility(bool visible, bool noSounds = false);
    }
}