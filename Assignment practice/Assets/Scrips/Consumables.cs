using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumables : Items
{
   

    public abstract void useItem(Character character);


    public Consumables(string name, string desc) : base(desc, name)
    { 

    }

}
