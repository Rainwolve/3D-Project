
public abstract class PlayerBaseState
{
    protected PlayerStateManager stateManager;
    protected PlayerStateFactory stateFactory;

    public PlayerBaseState(PlayerStateManager stateManager, PlayerStateFactory stateFactory)
    {
        this.stateManager = stateManager;
        this.stateFactory = stateFactory;
    }
    
    abstract public void EnterState();
    
    abstract public void UpdateState();
}

