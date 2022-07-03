using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CafofoStudio
{
	[CreateAssetMenu(fileName = "VillageAmbiencePreset", menuName = "CafofoStudio/Create Custom Preset Asset/Village", order = 1)]
	public class VillageAmbiencePreset : AmbientPreset
	{

        [Range(0, 1)] public float birdsIntensity = 0f;
        [Range(0, 1)] public float birdsVolumeMultiplier = 1f;

        [Range(0, 1)] public float rainIntensity = 0f;
        [Range(0, 1)] public float rainVolumeMultiplier = 1f;

        [Range(0, 1)] public float waterStreamIntensity = 0f;
        [Range(0, 1)] public float waterStreamVolumeMultiplier = 1f;

        [Range(0, 1)] public float fireIntensity = 0f;
        [Range(0, 1)] public float fireVolumeMultiplier = 1f;

        [Range(0, 1)] public float crowdIntensity = 0f;
        [Range(0, 1)] public float crowdVolumeMultiplier = 1f;

        [Range(0, 1)] public float blacksmithIntensity = 0f;
        [Range(0, 1)] public float blacksmithVolumeMultiplier = 1f;

        [Range(0, 1)] public float lumbermillIntensity = 0f;
        [Range(0, 1)] public float lumbermillVolumeMultiplier = 1f;

        [Range(0, 1)] public float humanActivityIntensity = 0f;
        [Range(0, 1)] public float humanActivityVolumeMultiplier = 1f;

        [Range(0, 1)] public float farmAnimalsIntensity = 0f;
        [Range(0, 1)] public float farmAnimalsVolumeMultiplier = 1f;

    }
}
    