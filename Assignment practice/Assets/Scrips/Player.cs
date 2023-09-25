using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    //track whether user has used a item this turn
    public bool itemUsed;


    //state machine to track users turn state
    private PlayerStateMachine state;

    //list of players attacks
    protected List<string> attacks = new List<string>() { "melee", "smite", "fireball", "poisonSlash" };

    // factory class for instantiating enemys into scene
    private Factory factory = new Factory();

    // Player user interface variables, these hold panels buttons etc
    public Dropdown healthDrop, manaDrop;
    public HealthBar healthbar;
    public Button inventoryButton;
    public Button Run;
    public GameObject death;
    public Button passTurn;
    public Button attack_button;
    public Dropdown enemySelector;
    public Button Attack1;
    public Button Attack2;
    public Button Attack3;
    public Button Attack4;
    public Button Attack5;
    public Button Attack6;
    public GameObject wonScreen;
    public GameObject attacksUI;
    public GameObject items;
    public Button cont;
    public GameObject inputs;
    public Button healthItem, manaItem;


    //set up lists for referencing enemys and attacks
    private GameObject attackList;

    //index for looping through enemys
    private int enemyIndex = 0;

    // database of attacks
    private AttackDB attackdb = new AttackDB();

    //list of attacks and list of enemys
    private List<GameObject> attackButtons = new List<GameObject>();
    public List<GameObject> EnemyList = new List<GameObject>();
    private List<string> healthItems;
    private List<string> manaItems;


    //run at start of scene
    protected override void Start()
    {

        Debug.Log("Start of battle scene DebugLog");
        
        //get the inventory from the passer
        Inventory = ValuePasser.Inventory;
      

        //set cursor to visible
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


        //hide the won screen
        wonScreen.gameObject.SetActive(false);

        //instantiate enemys
        factory.createEnemys();


        //set the players initial state
        SetState(new PlayerAttackState(this));

        //set the max hp of healthbar
        healthbar.SetMaxHealth(maxHealth);

        //add all enemys in scene to list
        EnemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        //give all enemys a unique id
        createID();
        //set up the gui
        setupGUI();

        //populate the dropdown with enemys
        populateSelector();
        //set a enemy to selected
        SelectEnemy();


        //set the hp to what is stored in the value passer
        health = ValuePasser.playerHealth;
        mp = ValuePasser.playerMP;


    }




    protected override void Update()
    {
        // set up the inventory menu
        SetUpInventory();
        //update the healthbar
        healthbar.SetHealth(health);
        //check if the player has won the battle
        CheckWin();
        //select a enemy
        SelectEnemy();
        //check if player is dead
        if (health <= 0)
        {
            //show death screen
            death.SetActive(true);
            //boot to main menu
            StartCoroutine(toMenu());
        }
        Debug.Log("there are this many enemys " + EnemyList.Count);
        Debug.Log("the inventory count is " + Inventory.Count);
    }

    //set the players state
    public void SetState(PlayerStateMachine state)
    {
        state = state;
    }


    // populate the selector dropbox with enemy names
    public void populateSelector()
    {
        //clear the dropdown values
        enemySelector.ClearOptions();
        List<string> m_DropOptions = new List<string>();

        //loop through enenmy list
        foreach (GameObject GameObject in EnemyList)
        {
            //get current enemy script
            Enemy enemy = GameObject.GetComponent<Enemy>();
            Debug.Log(GameObject.name);

            //check if enemy is alive allowing the user to only select alive enemies
            if (enemy.isAlive == true)
            {
                //add the enemy name to the dropdown
                string name = GameObject.name;
                m_DropOptions.Add(name);
            }
        }
        //set the values
        enemySelector.AddOptions(m_DropOptions);
    }



    // this method is used in testing and no longer needed. it is kept here for future debugging
    public void manualAttack()
    {
        string selected = enemySelector.options[enemySelector.value].text;
        foreach (GameObject GameObject in EnemyList)
        {
            if (GameObject.name == selected)
            {
                Character enemy = GameObject.GetComponent<Character>();
                enemy.Damage(1);
                int health = enemy.GetHealth();
                Debug.Log("enemys health is now: " + health);

            }
        }


    }


    //get the attack name from the button that is clicked
    public void attackSelector(string buttonClicked)
    {
        string value = " ";
        switch (buttonClicked)
        {
            case "Attack1":
                value = Attack1.GetComponentInChildren<Text>().text;
                break;
            case "Attack2":
                value = Attack2.GetComponentInChildren<Text>().text;
                break;
            case "Attack3":
                value = Attack3.GetComponentInChildren<Text>().text;
                break;
            case "Attack4":
                value = Attack4.GetComponentInChildren<Text>().text;
                break;
            case "Attack5":
                value = Attack5.GetComponentInChildren<Text>().text;
                break;
            case "Attack6":
                value = Attack6.GetComponentInChildren<Text>().text;
                break;
        }
        //get the target of the attack
        string selected = enemySelector.options[enemySelector.value].text;
        //call the attack function
        attackFunc(value, selected);

    }


    //timed function that starts the enemy turn cycle
    public IEnumerator StartEnemyTurn()
    {
        //loop through each enemy
        foreach (GameObject gameObject in EnemyList)
        {
            Enemy enemy = gameObject.GetComponent<Enemy>();
            //check if the enemy is alive
            if (enemy.isAlive == true)
            {
                //set the current enemy to turn state
                enemy.SetState(new TurnState(enemy));
                //mainCam.enabled = false;
                //take turn for enemy
                enemy.currentState.Tick();
                //wait for turn to end
                yield return new WaitForSeconds(2);
                Debug.Log("DONE WAITING");
            }

        }
        // mainCam.enabled = true;
        //start player turn
        SetState(new PlayerAttackState(this));

    }

    //call the methods to start enemy turn. this is done because
    //IEnumerator cannot be called from button
    public void PassTurn()
    {
        SetState(new PlayerWaitState(this));
        StartCoroutine(StartEnemyTurn());
    }


    //set up the graphical user interface
    public void setupGUI()
    {
        // hide the buttons 
        attackButtons.AddRange(GameObject.FindGameObjectsWithTag("attackButton"));
        Debug.Log("value: " + attackButtons.Count);
        foreach (GameObject GameObject in attackButtons)
        {
            Button btn = GameObject.GetComponent<Button>();
            btn.gameObject.SetActive(false);

        }

        //set up attack button text values
        int attackTotal = attacks.Count;
        Debug.Log("attack total = " + attackTotal);
        for (int i = 0; i < attackTotal; i++)
        {
            Debug.Log("i = " + i);
            GameObject gameobject = attackButtons[i];
            Button btn = gameobject.GetComponent<Button>();
            //show button that has been given a value
            btn.gameObject.SetActive(true);
            btn.GetComponentInChildren<Text>().text = attacks[i];
        };


        populateSelector();

        //event listeners for buttons
        healthItem.onClick.AddListener(delegate { useItem("hp"); });
        manaItem.onClick.AddListener(delegate { useItem("mp"); });
        attack_button.onClick.AddListener(openAttackMenu);
        inventoryButton.onClick.AddListener(OpenInventory);
        enemySelector.onValueChanged.AddListener(delegate { SelectEnemy(); });
        Attack1.onClick.AddListener(delegate { attackSelector("Attack1"); });
        Attack2.onClick.AddListener(delegate { attackSelector("Attack2"); });
        Attack3.onClick.AddListener(delegate { attackSelector("Attack3"); });
        Attack4.onClick.AddListener(delegate { attackSelector("Attack4"); });
        Attack5.onClick.AddListener(delegate { attackSelector("Attack5"); });
        Attack6.onClick.AddListener(delegate { attackSelector("Attack6"); });
        cont.onClick.AddListener(OpenOverWord);
        passTurn.onClick.AddListener(PassTurn);

    }






    public void attackFunc(string value, string selected)
    {
        StartCoroutine(attackEnemy(value, selected));

    }


    public void SelectEnemy()
    {

        string selected = enemySelector.options[enemySelector.value].text;
        foreach (GameObject GameObject in EnemyList)
        {
            Enemy enemy = GameObject.GetComponent<Enemy>();
            if (enemy.name == selected)
            {
                enemy.setSelected(true);
            }
            else
            {
                enemy.setSelected(false);
            }
        }

    }


    public IEnumerator attackEnemy(string value, string selected)
    {
        GUIvisibility(false);
        AttackDB attackdb = new AttackDB();
        Attack attack = attackdb.GetAttack(value);

        bool isNotEmpty = EnemyList.Any();

        if (isNotEmpty == true)
        {
            foreach (GameObject GameObject in EnemyList)
            {
                if (GameObject.name == selected)
                {
                    if (mp >= attack.GetManaCost())
                    {
                        Enemy enemy = GameObject.GetComponent<Enemy>();
                        int dmg = attack.GetDamage();
                        string status = attack.checkStatus();
                        int manacost = attack.GetManaCost();
                        enemy.DamageEnemy(dmg);
                        Debug.Log(status);
                        enemy.SetStatus(status);
                        yield return new WaitForSeconds(2);
                        populateSelector();
                        PassTurn();
                    }

                }
            }

        }
    }


    private IEnumerator toMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");
    }




    public void GUIvisibility(bool isactive)
    {
        attacksUI.SetActive(isactive);
        inputs.SetActive(isactive);

    }



    public void CheckWin()
    {
        int totalAlive = 0;
        foreach (GameObject GameObject in EnemyList)
        {
            Enemy enemy = GameObject.GetComponent<Enemy>();
            if (enemy.isAlive == true)
            {
                totalAlive += 1;
            }
        }
        if (totalAlive == 0)
        {
            wonScreen.SetActive(true);
            ValuePasser.playerHealth = health;
            ValuePasser.playerMP = mp;
        }

        Debug.Log("there are this many alive :" + totalAlive);
    }




    public void OpenOverWord()
    {
        ValuePasser.Inventory = Inventory;
        SceneManager.LoadScene("OverWorld");
    }

    private void createID()
    {
        int count = 0;
        foreach (GameObject gameObject in EnemyList)
        {
            gameObject.name = (gameObject.name + count.ToString());
            count += 1;
        }
    }


    public void hideAllMenu()
    {
        inputs.SetActive(false);
        attackList.SetActive(false);

    }

    public void showMenu()
    {
        inputs.SetActive(true);

    }

    public void openAttackMenu()
    {
        if (items.activeSelf)
        {
            items.SetActive(false);
        }
        attacksUI.SetActive(true);
    }


    public void OpenInventory()
    {
        if (attacksUI.activeSelf)
        {
            attacksUI.SetActive(false);
        }
        items.SetActive(true);
    }



    public void openOverWorld()
    {
        SceneManager.LoadScene("OverWorld");
    }



    public void SetUpInventory()
    {
        healthDrop.ClearOptions();
        manaDrop.ClearOptions();
        healthItems = new List<string>();
        manaItems = new List<string>();

        int healthCount = 0;
        int mpCount = 0;
        Debug.Log("Inventory");
        foreach (Consumables item in Inventory)
        {
            if (item.GetType() == typeof(Health))
            {
                healthItems.Add(item.GetName());
            }
            if (item.GetType() == typeof(Mana))
            {
                manaItems.Add(item.GetName());
            }
        }
        Debug.Log(healthCount);
        Debug.Log(mpCount);

        healthDrop.AddOptions(healthItems);
        manaDrop.AddOptions(manaItems);
    }



    public void useItem(string type)
    {
        string itemName = "";
        if (type == "hp")
        {
            itemName = healthDrop.options[healthDrop.value].text;
        }

        if (type == "mp")
        {
            itemName = manaDrop.options[manaDrop.value].text;
        }

        if (itemUsed == false && itemName != "")
        {
            foreach (Consumables item in Inventory)
            {
            if (itemName == item.GetName())
                {
                 item.useItem(this);
                Inventory.Remove(item);
                SetUpInventory();
                break;
                }
            }
        }
        
        itemUsed = true;
    }



   
}



  

   




