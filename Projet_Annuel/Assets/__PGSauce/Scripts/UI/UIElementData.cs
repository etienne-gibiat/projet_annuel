using PGSauce.AudioManagement;
using PGSauce.Core.Strings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.UI
{
    [InlineEditor()]
    [CreateAssetMenu(menuName = MenuPaths.MenuBase + "UI/Animation/UI Element Data", order = 0)]
    public class UIElementData : ScriptableObject
    {
        [SerializeField, FoldoutGroup("Audio")] private PgSfxBase showClip;
        [SerializeField, FoldoutGroup("Audio")] private PgSfxBase hideClip;
        [SerializeField] private Vector3AnimationSection movementSection;
        [SerializeField] private FloatAnimationSection opacitySection;
        [SerializeField] private Vector3AnimationSection rotationSection;
        [SerializeField] private Vector3AnimationSection scaleSection;
        [SerializeField] private FloatAnimationSection sliceSection;
        public Vector3AnimationSection MovementSection => movementSection;

        public Vector3AnimationSection RotationSection => rotationSection;

        public Vector3AnimationSection ScaleSection => scaleSection;

        public FloatAnimationSection OpacitySection => opacitySection;

        public FloatAnimationSection SliceSection => sliceSection;

        public PgSfxBase HideClip => hideClip;

        public PgSfxBase ShowClip => showClip;

        public UIElementData()
        {
            movementSection = new Vector3AnimationSection();
            opacitySection = new FloatAnimationSection();
            sliceSection = new FloatAnimationSection();
            rotationSection = new Vector3AnimationSection();
            scaleSection = new Vector3AnimationSection();
        }

        public float CheckGreatestHidingTime(float hideDuration)
        {
            hideDuration = MovementSection.CheckGreatestHidingTime(hideDuration);
            hideDuration = rotationSection.CheckGreatestHidingTime(hideDuration);
            hideDuration = scaleSection.CheckGreatestHidingTime(hideDuration);
            hideDuration = opacitySection.CheckGreatestHidingTime(hideDuration);
            hideDuration = sliceSection.CheckGreatestHidingTime(hideDuration);

            return hideDuration;
        }
        
        public float CheckGreatestShowingTime(float showDuration)
        {
            showDuration = MovementSection.CheckGreatestShowingTime(showDuration);
            showDuration = rotationSection.CheckGreatestShowingTime(showDuration);
            showDuration = scaleSection.CheckGreatestShowingTime(showDuration);
            showDuration = opacitySection.CheckGreatestShowingTime(showDuration);
            showDuration = sliceSection.CheckGreatestShowingTime(showDuration);

            return showDuration;
        }

        public void Init()
        {
            MovementSection.Init();
            rotationSection.Init();
            scaleSection.Init();
            opacitySection.Init();
            sliceSection.Init();
        }

        public void OnUiDestroyed()
        {
            MovementSection.KillTween();
            RotationSection.KillTween();
            scaleSection.KillTween();
            opacitySection.KillTween();
            sliceSection.KillTween();
        }
    }
}