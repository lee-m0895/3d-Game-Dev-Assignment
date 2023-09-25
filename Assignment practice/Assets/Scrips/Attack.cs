using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack 
{
    System.Random rnd = new System.Random();
    private int damage, manacost;
    private string damageType, name, desc;
    private List<string> validate = new List<string>() { "Thunder", "Fire", "Water", "Wind", "Dark", "Light", "Poison", "Physical" };
    private float chanceToBurn, chanceToPoison, chanceToStun;


    public Attack(int damage, string damageType, string name, float chanceToBurn, float chanceToPoison, float chanceToStun, string desc, int manaCost)
    {
        this.manacost = manaCost;
        this.damage = damage;
        this.chanceToBurn = chanceToBurn;
        this.chanceToPoison = chanceToPoison;
        this.chanceToStun = chanceToStun;
        this.name = name;
        this.desc = desc;
        foreach (string value in validate)
        {
            if (value == damageType)
            {
                this.damageType = damageType;
            }
            else
            {
                this.damageType = null;
            }
        }
        
    }


    public Attack()
    {
        
    }

    public string GetName()
    {
        return this.name;
    }

    public int GetDamage()
    {
        return this.damage;
    }

    



    public int GetManaCost()
    {
        return this.manacost;
    }


    public string GetDesc()
    {
        return this.name + "-" +this.desc + " Cost-" + this.manacost.ToString();
    }


    public string checkStatus()
    {

        double randomFloat = rnd.NextDouble();

        if (randomFloat == 0)
        {
            randomFloat += 0.1;
        }

        if (this.chanceToBurn >= randomFloat)
        {
            return "burned";
        }
        else if (this.chanceToPoison >= randomFloat)
        {
            return "poisoned";
        }
        else if (this.chanceToStun >= randomFloat)
        {
            return "stunned";
        }
        else
        {
            return "null";
        }
    }






}
