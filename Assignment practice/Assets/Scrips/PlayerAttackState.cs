using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerStateMachine
{

    
    public PlayerAttackState(Player character) : base(character)
    {
        character.itemUsed = false;
        character.GUIvisibility(true);
        character.itemUsed = false;
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
