using UnityEngine;


namespace AmazingAssets.DynamicRadialMasks
{
    [System.Serializable]
    public class DRMAnimatableProperty
    {
        public enum EVOLUTION_TYPE { Constant, ConstantRange, Lerp, LerpRange, Curve }

        public EVOLUTION_TYPE evolutionType;
        public Vector2 startValue;      //x - min, y - max
        public Vector2 endValue;        //x - min, y - max
        public AnimationCurve curve;    //Time range always is in range [0, 1]



        float evolutionValueStart;
        float evolutionValueEnd;


        public DRMAnimatableProperty()
        {
            this.evolutionType = EVOLUTION_TYPE.Constant;
            this.startValue = Vector2.one;
            this.endValue = Vector2.one * 2;
            this.curve = AnimationCurve.Linear(0, 1, 0, 1);

            Initialize();
        }

        public DRMAnimatableProperty(DRMAnimatableProperty item)
        {
            this.evolutionType = item.evolutionType;
            this.startValue = item.startValue;
            this.endValue = item.endValue;
            this.curve = item.curve;

            Initialize();
        }

        public DRMAnimatableProperty(EVOLUTION_TYPE evolution, Vector2 start, Vector2 end, AnimationCurve curve)
        {
            this.evolutionType = evolution;
            this.startValue = start;
            this.endValue = end;
            this.curve = curve;

            Initialize();
        }

        public void Scale(float value)
        {
            startValue *= value;
            endValue *= value;

            if (curve != null)
            {
                for (int i = 0; i < curve.length; i++)
                {
                    curve.keys[i].value *= value;
                }
            }
        }

        void Initialize()
        {
            switch (evolutionType)
            {
                case EVOLUTION_TYPE.Constant:
                    evolutionValueStart = evolutionValueEnd = startValue.x;
                    break;

                case EVOLUTION_TYPE.ConstantRange:
                    evolutionValueStart = evolutionValueEnd = Random.Range(startValue.x, startValue.y);
                    break;


                case EVOLUTION_TYPE.Lerp:
                    evolutionValueStart = startValue.x;
                    evolutionValueEnd = endValue.x;
                    break;

                case EVOLUTION_TYPE.LerpRange:
                    evolutionValueStart = Random.Range(startValue.x, startValue.y);
                    evolutionValueEnd = Random.Range(endValue.x, endValue.y);
                    break;
            }
        }


        public float GetEvolutionValue(float delta) //Range always is in the range of [0, 1]
        {
            switch (evolutionType)
            {
                case EVOLUTION_TYPE.Constant:
                case EVOLUTION_TYPE.ConstantRange:
                    return evolutionValueStart;


                case EVOLUTION_TYPE.Lerp:
                case EVOLUTION_TYPE.LerpRange:
                    return Mathf.Lerp(evolutionValueStart, evolutionValueEnd, delta);


                case EVOLUTION_TYPE.Curve:
                    return curve == null ? 0 : curve.Evaluate(delta);

                default:
                    return 0;
            }
        }
    }
}