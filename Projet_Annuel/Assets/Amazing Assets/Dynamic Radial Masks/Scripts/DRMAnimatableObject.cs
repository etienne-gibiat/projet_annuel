using UnityEngine;


namespace AmazingAssets.DynamicRadialMasks
{
    [System.Serializable]
    public class DRMAnimatableObject
    {
        public Vector3 position;

        public DRMAnimatableProperty radius = new DRMAnimatableProperty();
        public float currentRadius;

        public DRMAnimatableProperty intensity = new DRMAnimatableProperty();
        public float currentIntensity;

        public DRMAnimatableProperty noise = new DRMAnimatableProperty();
        public float currentNoiseStrength;

        public DRMAnimatableProperty edgeSize = new DRMAnimatableProperty();
        public float currentEdgeSize;

        public DRMAnimatableProperty ringCount = new DRMAnimatableProperty();
        public int currentRingCount;

        public DRMAnimatableProperty phaseSpeed = new DRMAnimatableProperty();
        public float currentPhase;

        public DRMAnimatableProperty frequency = new DRMAnimatableProperty();
        public float currentFrequency;

        public DRMAnimatableProperty smooth = new DRMAnimatableProperty();
        public float currentSmooth;



        public Vector2 lifeLength = Vector2.one;
        float currentLifeLength;
        float currentAge;


        public DRMAnimatableObject()
        {
            Reset();
        }

        public DRMAnimatableObject(DRMAnimatableObject obj, Vector3 position)
        {
            this.position = position;

            radius = new DRMAnimatableProperty(obj.radius);
            intensity = new DRMAnimatableProperty(obj.intensity);
            noise = new DRMAnimatableProperty(obj.noise);
            edgeSize = new DRMAnimatableProperty(obj.edgeSize);
            ringCount = new DRMAnimatableProperty(obj.ringCount);
            phaseSpeed = new DRMAnimatableProperty(obj.phaseSpeed);
            frequency = new DRMAnimatableProperty(obj.frequency);
            smooth = new DRMAnimatableProperty(obj.smooth);

            lifeLength = obj.lifeLength;
            currentLifeLength = Random.Range(lifeLength.x, lifeLength.y);
            currentAge = 0;
        }

        public void Reset()
        {
            radius = new DRMAnimatableProperty();
            intensity = new DRMAnimatableProperty();
            noise = new DRMAnimatableProperty(DRMAnimatableProperty.EVOLUTION_TYPE.Constant, Vector2.zero, Vector2.zero, null);
            edgeSize = new DRMAnimatableProperty();
            ringCount = new DRMAnimatableProperty();
            phaseSpeed = new DRMAnimatableProperty();
            frequency = new DRMAnimatableProperty();
            smooth = new DRMAnimatableProperty();

            lifeLength = Vector2.one;
        }

        public bool Animate(float deltaTime)
        {
            currentAge += deltaTime;

            float ageRatio = Mathf.Clamp01(currentAge / currentLifeLength);


            currentRadius = radius.GetEvolutionValue(ageRatio);
            currentIntensity = intensity.GetEvolutionValue(ageRatio);
            currentNoiseStrength = noise.GetEvolutionValue(ageRatio);
            currentEdgeSize = edgeSize.GetEvolutionValue(ageRatio);
            currentPhase += deltaTime * phaseSpeed.GetEvolutionValue(ageRatio);
            currentRingCount = (int)ringCount.GetEvolutionValue(ageRatio);
            currentFrequency = frequency.GetEvolutionValue(ageRatio);
            currentSmooth = smooth.GetEvolutionValue(ageRatio);


            return (ageRatio < 1) ? true : false;   //Can be animation continued in the next frame?
        }
    }
}