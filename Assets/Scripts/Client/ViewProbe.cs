using Core.Player;

public abstract class ViewProbeHolder
{
    public abstract void TakeProbeAndDoAction();
    public abstract bool CheckCondition();
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

    public override void TakeProbeAndDoAction()
    {
        var probe = _raycaster.CheckHit<T>();

        if (probe != null)
        {
            _action(probe);
        }
    }

    public override bool CheckCondition()
    {
        var probe = _raycaster.CheckHit<T>();

        if (probe != null)
        {
            return _condition == null || _condition(probe, _player);
        }

        return false;
    }
}
