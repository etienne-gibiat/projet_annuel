using System.Collections;
using UnityEngine;
#if ARCADIA_USE_DOTWEEN
using System;
using DG.Tweening;
#endif

namespace GameTroopers.UI
{
    internal static class AnimationHelper
    {
        internal static MenuAnimationDelegate GetDefaultAnimation(Menu.AnimType animType)
        {
#if ARCADIA_USE_DOTWEEN
            switch (animType)
            {
                case Menu.AnimType.Instant:
                    return InstantAnimation;
                case Menu.AnimType.Linear:
                    return LinearAnimation;
                case Menu.AnimType.EaseIn:
                    return EaseInAnimation;
                case Menu.AnimType.EaseOut:
                    return EaseOutAnimation;
                case Menu.AnimType.EaseInBack:
                    return EaseInBackAnimation;
                case Menu.AnimType.EaseOutBack:
                    return EaseOutBackAnimation;
                case Menu.AnimType.Custom:
                default:
                    throw new ArgumentException(String.Format("Animation {0} does not exist.", animType.ToString()));
            }
#else
            if (animType != Menu.AnimType.Instant)
            {
                Debug.LogWarning("Default animations require DOTween library to work correctly.");
            }
            return InstantAnimation;
#endif
        }

        private static IEnumerator InstantAnimation(Menu menu, Vector2 startPosition, Vector2 endPosition, float duration)
        {
            RectTransform transform = menu.GetComponent<RectTransform>();
            transform.anchoredPosition = endPosition;
            yield return null;
        }

#if ARCADIA_USE_DOTWEEN

        private static IEnumerator LinearAnimation(Menu menu, Vector2 startPosition, Vector2 endPosition, float duration)
        { 
            yield return GetDOTweenAnimation(menu, startPosition, endPosition, duration, Ease.Linear).WaitForCompletion();
        }

        private static IEnumerator EaseInAnimation(Menu menu, Vector2 startPosition, Vector2 endPosition, float duration)
        {
            yield return GetDOTweenAnimation(menu, startPosition, endPosition, duration, Ease.InSine).WaitForCompletion();
        }

        private static IEnumerator EaseOutAnimation(Menu menu, Vector2 startPosition, Vector2 endPosition, float duration)
        {
            yield return GetDOTweenAnimation(menu, startPosition, endPosition, duration, Ease.OutSine).WaitForCompletion();
        }

        private static IEnumerator EaseInBackAnimation(Menu menu, Vector2 startPosition, Vector2 endPosition, float duration)
        {
            yield return GetDOTweenAnimation(menu, startPosition, endPosition, duration, Ease.InBack).WaitForCompletion();
        }

        private static IEnumerator EaseOutBackAnimation(Menu menu, Vector2 startPosition, Vector2 endPosition, float duration)
        {
            yield return GetDOTweenAnimation(menu, startPosition, endPosition, duration, Ease.OutBack).WaitForCompletion();
        }

        private static Tween GetDOTweenAnimation(Menu menu, Vector2 startPosition, Vector2 endPosition, float duration, Ease easing)
        {
            var rectTransform = menu.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = startPosition;
            return rectTransform.DOAnchorPos(endPosition, duration).SetEase(easing);
        }
#endif
    }
}
