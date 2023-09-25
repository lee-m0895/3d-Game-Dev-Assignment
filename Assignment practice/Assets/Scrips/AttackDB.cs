
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackDB 
{
    public Hashtable Database;

      public AttackDB()
    {
        //damage, type of damage, name, chance to burn, chance to poison, chance to stun, description, mana cost
        this.Database = new Hashtable()
        {
            { "melee",  new Attack(10, "Physical", "melee", 0.0f, 0.0f, 0.1f, "a basic physical attack", 0) },
            { "smite", new Attack(30, "Thunder", "smite", 0.0f, 0.0f, 0.8f, "a weak attack with high chance to stun", 5 ) } ,
            { "fireball",  new Attack(50, "Fire", "FireBall", 0.5f, 0.0f, 0.0f, "shoot a fireball", 20) },
            { "poisonSlash",  new Attack(15, "Physical", "poisonSlash", 0.0f, 0.9f, 0.0f, "slash with a poison blade", 10) }

        };
    }




    public Attack GetAttack(string id)
    {
        Attack attack;
        if (this.Database.ContainsKey(id))
        {
            attack = (Attack) this.Database[id];
            
        }
        else
        {
            attack = (Attack) this.Database[1];
        }
        return attack;

    }


}
