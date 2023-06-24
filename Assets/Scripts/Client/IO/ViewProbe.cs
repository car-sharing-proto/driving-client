using UnityEngine;

public abstract class ViewProbeHolder
{
    public enum QueryMode
    {
        CHECK,
        INTERACT,
    }

    public abstract bool CheckCondition(QueryMode mode);
}
public class ViewProbe<T> : ViewProbeHolder where T : class
{
    public delegate bool Condition(T obj);
    public delegate void Action(T obj);

    private readonly Condition _condition;
    private readonly Action _action;
    private readonly Raycaster _raycaster;

    public ViewProbe(Transform view, float rayLength, Action action, Condition condition = null)
    {
        this._condition = condition;
        this._action = action;
        this._raycaster = new Raycaster(view, rayLength);
    }

    public override bool CheckCondition(QueryMode mode)
    {
        var probe = _raycaster.CheckHit<T>();

        if (probe == null)
        {
            return false;
        }

        if (_condition != null && !_condition(probe))
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