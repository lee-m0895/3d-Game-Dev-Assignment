using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    public HealthBar healthbar;
    protected List<string> attacks = new List<string>() { "melee", "smite", "fireball", "poisonSlash" };
    public StateMachine currentState;
    public Player player;
    public bool isAlive = true;
    private Animation anim;
    public GameObject selected;
    private bool burn, stun, poison;
    private GameObject burnObj, stunObj, poisonObj;
    //private Camera cam;

    protected override void Start()
    {
        healthbar.SetMaxHealth(maxHp);
        //get status effect game Objects, these hold the particle systems
        poisonObj = this.gameObject.transform.GetChild(1).gameObject;
        stunObj = this.gameObject.transform.GetChild(2).gameObject;
        burnObj = this.gameObject.transform.GetChild(3).gameObject;


        //disable all particle systems
        poisonObj.SetActive(false);
        stunObj.SetActive(false);
        burnObj.SetActive(false);

        //get the health bar

        //get the selector icon
        //set selected to false
        selected.SetActive(false);
        anim = this.gameObject.GetComponent<Animation>();
        //cam = this.gameObject.GetComponent<Camera>();


        //get the enemy a reference to the player

        GameObject gm = GameObject.FindGameObjectWithTag("Player");
        player = gm.GetComponent<Player>();


        //set the enemy to the wait state
        SetState(new WaitState(this));
        //cam.enabled = false;


        //set all status effects to false
        burn = false;
        stun = false;
        poison = false;


    }

    protected override void Update()
    {

        Debug.Log("burn: " + burn);
        Debug.Log("stun: " + stun);
        Debug.Log("poison: " + poison);
        healthbar.SetHealth(health);
        currentState.Tick();

        if (burnObj == true )
        {
            poisonObj.SetActive(false);
            stunObj.SetActive(false);
            burnObj.SetActive(true);

            if (!burnObj.GetComponent<ParticleSystem>().isPlaying)
            {
                burnObj.GetComponent<ParticleSystem>().Play();
            }            
        }
        else
        {
            if (burnObj.GetComponent<ParticleSystem>().isPlaying)
            {
                burnObj.GetComponent<ParticleSystem>().Stop();
            }
        }






        if (stunObj == true)
        {
            poisonObj.SetActive(false);
            stunObj.SetActive(true);
            burnObj.SetActive(false);
            stunObj.GetComponent<ParticleSystem>().Play();
            if (!stunObj.GetComponent<ParticleSystem>().isPlaying)
            {
                stunObj.GetComponent<ParticleSystem>().Play();
            }
        }
        else
        {
            if (stunObj.GetComponent<ParticleSystem>().isPlaying)
            {
                stunObj.GetComponent<ParticleSystem>().Stop();
            }
        }





        if (poisonObj == true)
        {
            poisonObj.SetActive(true);
            stunObj.SetActive(false);
            burnObj.SetActive(false);
            stunObj.GetComponent<ParticleSystem>().Play();
            if (!poisonObj.GetComponent<ParticleSystem>().isPlaying)
            {
                poisonObj.GetComponent<ParticleSystem>().Play();
            }
        }
        else
        {
            if (poisonObj.GetComponent<ParticleSystem>().isPlaying)
            {
                poisonObj.GetComponent<ParticleSystem>().Stop();
            }
        }

    }




    public void setUpAnimators()
    {
        
    }
    public void SetState(StateMachine state)
    {
        currentState = state;
    }



    public string GetState()
    {
        string value = currentState.GetState();
        return value;
    }

    public void StatePrint()
    {
        if (currentState != null)
        {
            currentState.PrintState();
        }
        else
        {
            Debug.Log("no state set");
        }

    }


    public void attack(Attack attack)
    {
        if (burn == true)
        { 
                stun = false;
                poison = false;
                DamageEnemy(10);
        }


        if (stun == true)
        { 
                burn = false;
                poison = false;
               
        }

        if (poison == true)
        {
                stun = false;
                burn = false;
                DamageEnemy(10);
        }
                
        
        if (stun != true)
        {
            int dmg = attack.GetDamage();
            string status = attack.checkStatus();
            int manacost = attack.GetManaCost();
            player.Damage(dmg);
            this.anim.Play("Attack01");
        }
        

    }



    public void turn()
    {
        
    }

    public IEnumerator DeathAni()
    {
        this.anim.Play("Death");
        yield return new WaitForSeconds(2);
    }


    public void DamageEnemy(int dmg)
    {
        
        health -= dmg;
        if (this.health <= 0)
        {
            this.isAlive = false;    
            StartCoroutine(DeathAni()); 
            this.gameObject.SetActive(false);


        }
        else
        {
            this.anim.Play("Damage");
        }
    }



    public void setSelected(bool val)
    {
        selected.SetActive(val);
    }

    public void SetStatus(string value)
    {
        switch (value)
        {
            case "burned":
                burn = true;
                break;
            case "poisoned":
                stun = true;
                break;
            case "stunned":
                poison = true;
                break;
        }
    }



}
