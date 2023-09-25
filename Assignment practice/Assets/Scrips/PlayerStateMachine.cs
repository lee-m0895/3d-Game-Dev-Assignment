using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateMachine 
{

    protected Player character;

    public abstract void Tick();

    public abstract void PrintState();

    public abstract void EndState();

    public PlayerStateMachine(Player character)
    {
        this.character = character;

    }
}
