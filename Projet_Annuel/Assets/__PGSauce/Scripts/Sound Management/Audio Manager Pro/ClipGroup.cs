using UnityEngine;


namespace PGSauce.AudioManagement.External
{
    [CreateAssetMenu(fileName = "ClipGroup", menuName = "AudioManager/ClipGroup")]
    public class ClipGroup : ScriptableObject
    {
        public AudioClip[] Clips;
    }
}
