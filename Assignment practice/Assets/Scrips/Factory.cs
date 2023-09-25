using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    List<GameObject> enemys = ValuePasser.enemyHolder;
    public GameObject test = ValuePasser.enemyTestHolder;
    //public GameObject playerObj = GameObject.FindGameObjectsWithTag("Player");
    //public Player player = playerObj.GetComponent<Player>();


    // Start is called before the first frame update
    void Start()
    {
      //  player.health = ValuePasser.playerHealth;
       // player.mp = ValuePasser.playerMP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createEnemys()
    {
        int enemyTeamSize = Random.Range(1, 4);
        int listSize = enemys.Count;
        for (int i = 0; i < enemyTeamSize; i++)
        {
            int index = Random.Range(0, listSize);
            switch (i)
            {
                case 0:
                    Vector3 pos = new Vector3(257.22f, 0.0f, 12.0f);
                   
                    Instantiate(enemys[index], pos, Quaternion.Euler(new Vector3(0, -90, 0)));
                    break;
                case 1:
                     pos = new Vector3(257.22f, 0.0f, 7.04f);
                    Instantiate(enemys[index], pos, Quaternion.Euler(new Vector3(0, -90, 0)));
                    break;
                case 2:
                     pos = new Vector3(257.22f, 0.0f, 17.75f);
                    Instantiate(enemys[index], pos, Quaternion.Euler(new Vector3(0, -90, 0)));
                    break;
                case 3:
                     pos = new Vector3(257.22f, 0.0f, 1.78f);
                    Instantiate(enemys[index], pos, Quaternion.Euler(new Vector3(0, -90, 0)));
                    break;
            }
        }

    }



}
