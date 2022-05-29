using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using PGSauce.Core.Strings;

namespace PGSauce.Animation
{
    /// <summary>
    /// Used to complement Animancer DirectionalAnimationSet and DirectionalAnimationSet8
    /// The 1st is used for sprites which need animations for up/down/left/right
    /// The 2nd one adds the diagonal
    /// This set is used for sprites which need left/right only
    /// <see cref="DirectionalAnimationSet"/>
    /// <see cref="DirectionalAnimationSet8"/>
    /// </summary>
    [HelpURL(DocsPaths.DirectionalAnimationSet2)]
    [CreateAssetMenu(menuName = MenuPaths.MenuBase + "Animations/Directional Animation 1D")]
    public class DirectionalAnimationSet2 : ScriptableObject
    {
        [SerializeField] private AnimationClip left;
        [SerializeField] private AnimationClip right;
        [SerializeField] private float speed = 1;

        /// <summary>
        /// Speed of the animation, defaults to 1
        /// </summary>
        public float Speed => speed;

        /// <summary>
        /// Returns the clip according to the direction of the sprite
        /// </summary>
        /// <param name="direction">The direction. If positive : right, else left</param>
        /// <returns></returns>
        public AnimationClip GetClip(float direction)
        {
            return direction > 0 ? right : left;
        }
    }
}
