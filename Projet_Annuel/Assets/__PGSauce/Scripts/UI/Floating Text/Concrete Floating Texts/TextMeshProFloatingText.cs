using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace PGSauce.UI
{
    public class TextMeshProFloatingText : FloatingTextBase
    {
        [Tooltip("the TextMesh used to display the value")]
        public TMP_Text targetTextMesh;
        protected override void SetText(string value, Color color)
        {
            targetTextMesh.text = value;
            targetTextMesh.color = color;
        }

    }
}
