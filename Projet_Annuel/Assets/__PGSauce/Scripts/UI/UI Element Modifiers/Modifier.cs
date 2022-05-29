namespace PGSauce.UI
{
    public abstract class Modifier<TSection>
    {
        public abstract void Modify(AnimationSection<TSection> section);
    }
}