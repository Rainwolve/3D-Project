using UnityEngine;

public class PlayerStateFactory
{
    private PlayerStateManager stateManager;
    
    public PlayerStateFactory(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public PlayerBaseState CreateIdleState()
    {
        return new PlayerIdleState(stateManager, this);
    }

    public PlayerBaseState CreateWalkState()
    {
        return new PlayerWalkState(stateManager, this);
    }

    public PlayerBaseState CreateRunState()
    {
        return new PlayerRunState(stateManager, this);
    }

    public PlayerBaseState CreateDanceState()
    {
        return new PlayerDanceState(stateManager, this);
    }

    public PlayerBaseState CreateGroundedState()
    {
        return new PlayerGroundedState(stateManager, this);
    }
    public PlayerBaseState CreateJumpState()
    {
        return new PlayerJumpState(stateManager, this);
    }
}