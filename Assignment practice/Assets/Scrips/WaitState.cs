using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : StateMachine
{
    public WaitState(Enemy character) : base(character)
    {

    }

    public override void Tick()
    {
        Debug.Log("enemy in wait state");
    }


    public override void PrintState()
    {
        Debug.Log("Wait State");
    }


    public override string GetState()
    {
        return "WaitState";
    }
}
