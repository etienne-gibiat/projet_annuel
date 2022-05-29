using Dreamteck.Splines;
using UnityEngine;

namespace PGSauce.Splines
{
    [CreateAssetMenu(fileName = "Spline UI Data", menuName = "Big Catto/Vendor Extensions/Dreamteck/Spline UI Data", order = 0)]
    public class SplineUIDreamTeckData : ScriptableObject
    {
        [SerializeField] private Vector2 referenceResolution = new Vector2(2340, 1080);
        [SerializeField] private Vector2 referenceScale = Vector2.one;

        public Vector3 GetScale(SplineComputer splineComputer)
        {
            var canvas = splineComputer.GetComponentInParent<Canvas>();
            var rect = canvas.GetComponent<RectTransform>();
            var width = rect.sizeDelta.x;
            var height = rect.sizeDelta.y;

            var ratioWidth = width / referenceResolution.x;
            var ratioHeight = height / referenceResolution.y;

            var x = referenceScale.x * ratioWidth;
            var y = referenceScale.y * ratioHeight;

            return new Vector3(x, y, 1);
        }
    }
}