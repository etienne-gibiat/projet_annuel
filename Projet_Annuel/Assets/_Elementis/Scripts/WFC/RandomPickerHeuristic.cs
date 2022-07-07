using System;

namespace _Elementis.Scripts.WFC
{
    internal class RandomPickerHeuristic : IPickHeuristic
    {
        private IRandomPicker randomPicker;
        private Func<double> randomDouble;

        public RandomPickerHeuristic(IRandomPicker randomPicker, Func<double> randomDouble)
        {
            this.randomPicker = randomPicker;
            this.randomDouble = randomDouble;
        }

        public void PickObservation(out int index, out int pattern)
        {
            index = this.randomPicker.GetRandomIndex(this.randomDouble);
            if (index == -1)
                pattern = -1;
            else
                pattern = this.randomPicker.GetRandomPossiblePatternAt(index, this.randomDouble);
        }
    }
}