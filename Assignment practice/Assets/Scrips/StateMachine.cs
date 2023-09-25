using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine 
{

    protected Enemy character;

    public abstract void Tick();

    public abstract void PrintState();

    public abstract string GetState();

    public StateMachine(Enemy character)
    {
        this.character = character;
       
    }



}
