using System.Collections.Generic;
using UnityEditor;

namespace CafofoStudio
{
	[CustomEditor(typeof(VillageAmbienceMixer))]
	public class VillageAmbienceMixerEditor : AmbienceMixerEditor<VillageAmbienceMixer, VillageAmbiencePreset>
	{
		protected override List<string> GetSerializedElementProperties()
		{
			return new List<string>() { "_birds", "_rain", "_waterStream", "_fire", "_crowd", "_blacksmith", "_lumbermill", "_humanActivity", "_farmAnimals" };
		}

		protected override void ApplyPreset(VillageAmbiencePreset preset)
		{
            ApplyPresetConfig("_birds", preset.birdsIntensity, preset.birdsVolumeMultiplier);
            ApplyPresetConfig("_rain", preset.rainIntensity, preset.rainVolumeMultiplier);
            ApplyPresetConfig("_waterStream", preset.waterStreamIntensity, preset.waterStreamVolumeMultiplier);
            ApplyPresetConfig("_fire", preset.fireIntensity, preset.fireVolumeMultiplier);
            ApplyPresetConfig("_crowd", preset.crowdIntensity, preset.crowdVolumeMultiplier);
            ApplyPresetConfig("_blacksmith", preset.blacksmithIntensity, preset.blacksmithVolumeMultiplier);
            ApplyPresetConfig("_lumbermill", preset.lumbermillIntensity, preset.lumbermillVolumeMultiplier);
            ApplyPresetConfig("_humanActivity", preset.humanActivityIntensity, preset.humanActivityVolumeMultiplier);
            ApplyPresetConfig("_farmAnimals", preset.farmAnimalsIntensity, preset.farmAnimalsVolumeMultiplier);
        }

		protected override SoundElement GetSoundElementFromProperty(string propertyName)
		{
			switch (propertyName)
			{
                case "_birds":
                  return myTarget.Birds;
                case "_rain":
                  return myTarget.Rain;
                case "_waterStream":
                  return myTarget.WaterStream;
                case "_fire":
                  return myTarget.Fire;
                case "_crowd":
                  return myTarget.Crowd;
                case "_blacksmith":
                  return myTarget.Blacksmith;
                case "_lumbermill":
                    return myTarget.Lumbermill;
                case "_farmAnimals":
                    return myTarget.FarmAnimals;
                default:
                   return myTarget.HumanActivity;

			}
		}

	}
}