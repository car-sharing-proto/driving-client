namespace Core.ViewProber
{
    public class ViewProbe<T> : ViewProbeHolder where T : class
    {
        public delegate bool Condition(T obj);
        public delegate void Action(T obj);

        private readonly Condition _condition;
        private readonly Action _action;
        private readonly Raycaster _raycaster;

        public ViewProbe(Raycaster raycaster,
            Action action, Condition condition = null)
        {
            this._condition = condition;
            this._action = action;
            this._raycaster = raycaster;
        }

        public override bool CheckCondition(QueryMode mode)
        {
            var probe = _raycaster.CheckHit<T>();

            if (probe is null)
            {
                return false;
            }

            if (_condition is not null && !_condition(probe))
            {
                return false;
            }

            if (mode == QueryMode.INTERACT)
            {
                _action(probe);
            }

            return true;
        }
    }
}