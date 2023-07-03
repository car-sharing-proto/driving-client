namespace Core.ViewProber
{
    public abstract class ViewProbeHolder
    {
        public enum QueryMode
        {
            CHECK,
            INTERACT,
        }

        public abstract bool CheckCondition(QueryMode mode);
    }
}