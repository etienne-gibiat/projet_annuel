using UnityEngine;

namespace PGSauce.Gameplay.MiniGames.Roulette
{
    public class RouletteReward : MiniGameReward<FortuneWheel>
    {
        [SerializeField] private RectTransform content;
        [SerializeField] private RectTransform separator;
        [SerializeField] private bool rotateContent = true;
        [SerializeField] private UnityEventFloat onBackgroundSetFillAmount;

        public RectTransform Separator => separator;

        public void ApplyLayout(float angle, float sliceSize, FortuneWheel wheel)
        {
            var offsetAngle = wheel.OffsetAngle;
            var rotAngle = -angle - offsetAngle;
            var rot = Quaternion.Euler(0, 0, rotAngle);
            var half = Quaternion.Euler(0, 0, -sliceSize / 2f);
            var contentRot = Quaternion.Euler(0, 0, rotateContent ? offsetAngle - sliceSize / 2f : -rotAngle);
            var vOffset = rot * half * Vector3.up * wheel.ElementOffset;

            transform.localRotation = rot;
            transform.localPosition = vOffset;
            
            onBackgroundSetFillAmount.Invoke(sliceSize / 360f);

            if (separator)
            {
                separator.localRotation = rot;
                separator.localPosition = vOffset;
            }

            if (content)
            {
                var dist = content.localPosition.magnitude;
                content.localPosition = half * (Vector3.up * dist);
                content.localRotation = contentRot;
            }
        }

        public void SpinningUpdate()
        {
            if (!rotateContent && content)
            {
                content.rotation = Quaternion.identity;
            }
        }
    }
}