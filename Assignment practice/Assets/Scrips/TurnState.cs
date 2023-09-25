using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : StateMachine
{
    public TurnState(Enemy character) : base(character)
    {
        
    }

    public override void Tick()
    {
        Debug.Log("enemy started turn");
        AttackDB attackdb = new AttackDB();
        Attack attack = attackdb.GetAttack("melee");
        Debug.Log(attack);
        character.attack(attack);
        Debug.Log("enemy used melee");         
        character.SetState(new WaitState(character));
        Debug.Log("enemy passed turn");
    }


    public override void PrintState()
    {
        Debug.Log("Turn State");
    }

    public override string GetState()
    {
        return "TurnState";
    }

}
