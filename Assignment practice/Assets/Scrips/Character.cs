using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{

    public int maxHealth;
    public static int maxHp;
    public static int maxMp = 100;
    public int health = maxHp;
    public int mp;
    protected List<Consumables> Inventory = new List<Consumables>(); 



    private int str, dex, luck, intel;

    protected virtual void Start()
    {
        maxHp = maxHealth;
    }


    protected virtual void Update()
    {
        
    }

    // inflict damage to this entity
    public  void Damage(int dmgValue) 
    {
        health -= dmgValue;
        Debug.Log("health is now " + health);
    }





    public List<Consumables> GetInventory()
    {
        return Inventory;
    }


    public void SetInventory(List<Consumables> inventory)
    {
        this.Inventory = inventory;
    }


    public int GetHealth()
    {
        return health;
    }

    public int GetMaxMp()
    {
        return maxMp;
    }
    public int GetMaxHealth()
    {
        return maxHp;
    }



    public void AddToInventory(Consumables item)
    {
        Inventory.Add(item);
        Debug.Log(item.GetName());
    
    }

    public void RemoveFromList(int index)
    {
        if (index < Inventory.Count && index >= 0)
        {
            Inventory.RemoveAt(index);
        }
        
    }


    public void RemoveByName()
    {
        
    }



    public bool checkForKey(string key)
    {
        bool value = false;
            foreach (Items item in Inventory)
            {
            Debug.Log(item.GetName());
                if (item.GetName() == key)
                {
                    value = true;
                }
                else
                {
                    value = false;
                }
            }
        Debug.Log(value);
        return value;
        }
      
        
    }




