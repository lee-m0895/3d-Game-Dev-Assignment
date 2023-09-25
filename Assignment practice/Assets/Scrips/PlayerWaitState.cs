using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaitState : PlayerStateMachine
{
    public PlayerWaitState(Player character) : base(character)
    {
        character.GUIvisibility(false);
    }

    public override void Tick()
    {

    }


    public override void PrintState()
    {
        Debug.Log("Wait State");
    }


    public override void EndState()
    {

    }
}
