using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnBox : MonoBehaviour
{
    public string scene;
    public GameObject player;
    public List<GameObject> enemys;

    public bool isIn = false;
    // Start is called before the first frame update
    void Start()
    {
        isIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isIn);
        if (isIn == true)
        {
            Invoke("LoadScene", Random.Range(10.0f, 60.0f));
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isIn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isIn = false;
        }
    }



    public void LoadScene()
    {
        
            //Player playerScript  = player.GetComponent<Player>();
            //ValuePasser.playerHealth = playerScript.health;
            ValuePasser.enemyHolder = enemys;
            SceneManager.LoadScene(scene); // loads scene When player enter the trigger collider 
            ValuePasser.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            Character character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            ValuePasser.playerHealth = character.health;
            ValuePasser.playerMP = character.mp;
            Debug.Log(ValuePasser.position);
            ValuePasser.Inventory = character.GetInventory();


    }
}
