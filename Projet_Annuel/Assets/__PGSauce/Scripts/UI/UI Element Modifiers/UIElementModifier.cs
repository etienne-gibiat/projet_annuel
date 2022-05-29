namespace PGSauce.UI
{
    public class UIElementModifier<TAnimation>
    {
        private readonly Modifier<TAnimation> _modifier;

        public UIElementModifier(Modifier<TAnimation> modifier)
        {
            _modifier = modifier;
        }

        public void Modify(AnimationSection<TAnimation> section)
        {
            _modifier.Modify(section);
        }
    }
}