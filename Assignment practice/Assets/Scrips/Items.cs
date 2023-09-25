using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items 
{
    protected string desc;
    protected string name;

    public Items(string desc, string name)
    {
        this.desc = desc;
        this.name = name;
    }

    public string GetName()
    {
        return this.name;
    }


    public string GetDesc()
    {
        return this.desc;
    }
}
