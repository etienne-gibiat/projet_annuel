using System.Collections;
using UnityEngine;

namespace GameTroopers.UI
{
    /// <summary>
    /// Delegate used to store the action that will play the menu animation. 
    /// Returning a null IEnumerator means that the animation has finished.
    /// </summary>
    /// <param name="menu">Menu to be animated</param>
    /// <param name="startPosition">Start position of the animation</param>
    /// <param name="endPosition">End position of the animation</param>
    /// <param name="duration">Duration of the animation</param>
    /// <returns>Returns an IEnumerator.</returns>
    public delegate IEnumerator MenuAnimationDelegate(Menu menu, Vector2 startPosition, Vector2 endPosition, float duration);
}
