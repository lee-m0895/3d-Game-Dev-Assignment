using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : Consumables
{
    private int mpValue;

    public Mana(string name, string desc, int mpValue) : base(desc, name)
    {
        this.mpValue = mpValue;
        this.name = name;
        this.desc = desc;
    }

    public override void useItem(Character character)
    {

        character.mp += mpValue;
        if (character.mp > character.GetMaxHealth())
        {
            character.mp = character.GetMaxMp();
        }

    }


}
