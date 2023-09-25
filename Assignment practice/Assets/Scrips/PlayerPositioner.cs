using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositioner : MonoBehaviour
{
    public HealthBar healthBar;
    public HealthBar manaBar;

    public GameObject player;
    public Character character;
    // Start is called before the first frame update
    void Start()
    {
        manaBar.SetMaxHealth(character.GetMaxHealth());
        healthBar.SetMaxHealth(character.GetMaxMp());

        Vector3 emptyVec = new Vector3(0.0f, 0.0f, 0.0f);
        if (ValuePasser.position != emptyVec)
        {    
        player.GetComponent<CharacterController>().enabled = false;
        Vector3 position = ValuePasser.position;
        player.transform.position = position;
        Debug.Log("positioned player at :" + ValuePasser.position);
        player.GetComponent<CharacterController>().enabled = true;
        character.health = ValuePasser.playerHealth;
        character.mp = ValuePasser.playerMP;
        character.SetInventory(ValuePasser.Inventory);
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        manaBar.SetHealth(character.mp);
        healthBar.SetHealth(character.health);      
    }
}
