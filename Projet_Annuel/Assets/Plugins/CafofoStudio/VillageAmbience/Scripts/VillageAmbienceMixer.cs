using System.Collections.Generic;
using UnityEngine;

namespace CafofoStudio
{

	public class VillageAmbienceMixer : AmbienceMixer<VillageAmbiencePreset>
	{

        [SerializeField] private SoundElement _birds;
        public SoundElement Birds
        {
            get { return _birds; }
            private set { _birds = Birds; }
        }

        [SerializeField] private SoundElement _rain;
        public SoundElement Rain
        {
            get { return _rain; }
            private set { _rain = Rain; }
        }

        [SerializeField] private SoundElement _waterStream;
        public SoundElement WaterStream
        {
            get { return _waterStream; }
            private set { _waterStream = WaterStream; }
        }

        [SerializeField] private SoundElement _fire;
        public SoundElement Fire
        {
            get { return _fire; }
            private set { _fire = Fire; }
        }

        [SerializeField] private SoundElement _crowd;
        public SoundElement Crowd
        {
            get { return _crowd; }
            private set { _crowd = Crowd; }
        }

        [SerializeField] private SoundElement _blacksmith;
        public SoundElement Blacksmith
        {
            get { return _blacksmith; }
            private set { _blacksmith = Blacksmith; }
        }

        [SerializeField] private SoundElement _lumbermill;
        public SoundElement Lumbermill
        {
            get { return _lumbermill; }
            private set { _lumbermill = Lumbermill; }
        }

        [SerializeField] private SoundElement _humanActivity;
        public SoundElement HumanActivity
        {
            get { return _humanActivity; }
            private set { _humanActivity = HumanActivity; }
        }

        [SerializeField] private SoundElement _farmAnimals;
        public SoundElement FarmAnimals
        {
            get { return _farmAnimals; }
            private set { _farmAnimals = FarmAnimals; }
        }


        protected override List<SoundElement> elements
		{
			get
			{
				return new List<SoundElement>() {_birds, _rain, _waterStream, _fire, _crowd, _blacksmith, _lumbermill, _humanActivity, _farmAnimals};
			}
		}

		override public void ApplyPreset(VillageAmbiencePreset selectedPreset)
		{

            _birds.SetIntensity(selectedPreset.birdsIntensity);
            _birds.SetVolumeMultiplier(selectedPreset.birdsVolumeMultiplier);

            _rain.SetIntensity(selectedPreset.rainIntensity);
            _rain.SetVolumeMultiplier(selectedPreset.rainVolumeMultiplier);

            _waterStream.SetIntensity(selectedPreset.waterStreamIntensity);
            _waterStream.SetVolumeMultiplier(selectedPreset.waterStreamVolumeMultiplier);

            _fire.SetIntensity(selectedPreset.fireIntensity);
            _fire.SetVolumeMultiplier(selectedPreset.fireVolumeMultiplier);

            _crowd.SetIntensity(selectedPreset.crowdIntensity);
            _crowd.SetVolumeMultiplier(selectedPreset.crowdVolumeMultiplier);

            _blacksmith.SetIntensity(selectedPreset.blacksmithIntensity);
            _blacksmith.SetVolumeMultiplier(selectedPreset.blacksmithVolumeMultiplier);

            _lumbermill.SetIntensity(selectedPreset.lumbermillIntensity);
            _lumbermill.SetVolumeMultiplier(selectedPreset.lumbermillVolumeMultiplier);

            _humanActivity.SetIntensity(selectedPreset.humanActivityIntensity);
            _humanActivity.SetVolumeMultiplier(selectedPreset.humanActivityVolumeMultiplier);

            _farmAnimals.SetIntensity(selectedPreset.farmAnimalsIntensity);
            _farmAnimals.SetVolumeMultiplier(selectedPreset.farmAnimalsVolumeMultiplier);

        }
	}
}
