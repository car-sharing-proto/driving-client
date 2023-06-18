using Core.Player;

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
    public delegate bool Condition(T obj, PlayerMovement player);
    public delegate void Action(T obj);

    private readonly Condition _condition;
    private readonly Action _action;
    private readonly Raycaster _raycaster;
    private readonly PlayerMovement _player;

    public ViewProbe(PlayerMovement player, float rayLength, Action action, Condition condition = null)
    {
        this._condition = condition;
        this._action = action;
        this._player = player;
        this._raycaster = new Raycaster(player.HeadTransform, rayLength);
    }

    public override bool CheckCondition(QueryMode mode)
    {
        var probe = _raycaster.CheckHit<T>();

        if (probe == null)
        {
            return false;
        }

        if (_condition != null && !_condition(probe, _player))
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