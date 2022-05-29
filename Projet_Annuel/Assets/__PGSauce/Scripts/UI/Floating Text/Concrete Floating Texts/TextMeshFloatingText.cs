using UnityEngine;

namespace PGSauce.UI
{
    public class TextMeshFloatingText : FloatingTextBase
    {
        [Tooltip("the TextMesh used to display the value")]
        public TextMesh targetTextMesh;

        protected override void SetText(string value, Color color)
        {
            targetTextMesh.color = color;
            targetTextMesh.text = value;
        }
    }
}