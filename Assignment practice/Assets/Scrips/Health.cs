using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Consumables
{
    private int healingVal;

    public Health(string name, string desc, int healingVal ): base(desc, name)
    {
        this.healingVal = healingVal;
        this.name = name;
        this.desc = desc;
    }
    public override void useItem(Character character)
    {
        
        character.health += healingVal;
        if (character.health > character.GetMaxHealth())
        {
            character.health = character.GetMaxHealth();
        }

    }

}
