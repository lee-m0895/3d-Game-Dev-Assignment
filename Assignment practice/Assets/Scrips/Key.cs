using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Consumables
{
    public Key(string name, string desc) : base(desc, name)
    {
        this.name = name;
        this.desc = desc;
    }

    public override void useItem(Character character)
    {

    }
}
