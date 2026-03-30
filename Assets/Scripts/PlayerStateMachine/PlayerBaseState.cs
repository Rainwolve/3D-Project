public abstract class PlayerBaseState
{
    protected PlayerStateManager stateManager;
    protected PlayerStateFactory stateFactory;

    private bool isRootState = false;
    private PlayerBaseState superState;
    private PlayerBaseState subState;

    public PlayerBaseState(PlayerStateManager stateManager, PlayerStateFactory stateFactory)
    {
        this.stateManager = stateManager;
        this.stateFactory = stateFactory;
    }

    abstract public void EnterState();

    abstract public void UpdateState();
    
    abstract public void ExitState();

    public void UpdateStates()
    {
        UpdateState();
        subState?.UpdateState();
    }

    public void ExitStates()
    {
        ExitState();
        subState?.ExitState();
    }
    
    public bool IsRootState
    {
        get { return isRootState; }
        set { isRootState = value; }
    }

    public PlayerBaseState SuperState
    {
        get { return superState; }
        set { superState = value; }
    }

    public PlayerBaseState SubState
    {
        get { return subState; }
        set
        {
            subState = value;
            subState.superState = this;
        }
    }
}